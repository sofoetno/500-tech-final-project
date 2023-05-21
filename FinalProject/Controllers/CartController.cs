using FinalProject.Dto;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly CartRepository _cartRepository;
    private readonly CartItemRepository _cartItemRepository;
    private readonly ProductRepository _productRepository;
    private readonly UserAccountRepository _userAccountRepository;

    public CartController(CartRepository cartRepository, CartItemRepository cartItemRepository, ProductRepository productRepository, UserAccountRepository userAccountRepository)
    {
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
        _userAccountRepository = userAccountRepository;
    }
    
    /// <summary>
    /// Get list of carts.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _cartRepository.GetAll());
    }

    /// <summary>
    /// Get cart by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _cartRepository.Get(id));
    }
    
    /// <summary>
    /// Create single cart.
    /// </summary>
    /// <param name="cartDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CartCreateOrUpdateDto cartDto)
    {
        return Ok(await this.CreateOne(cartDto));
    }
    
    /// <summary>
    /// Create multiple carts at once.
    /// </summary>
    /// <param name="cartDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] CartCreateOrUpdateDto[] cartDtos)
    {
        var createdItems = new List<Cart>();
        
        foreach (var cartDto in cartDtos)
        {
            createdItems.Add(await this.CreateOne(cartDto));
        }

        return Ok(createdItems);
    }

    private async Task<Cart> CreateOne(CartCreateOrUpdateDto cartDto)
    {
        var cart = new Cart()
        {
            UserAccountId = cartDto.UserAccountId,
        };

        return await _cartRepository.Add(cart);
    }

    /// <summary>
    /// Update cart.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cartDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] CartCreateOrUpdateDto cartDto)
    {
        var cart = await _cartRepository.Get(id);

        if (cart == null)
        {
            return NotFound("No cart with provided id");
        }

        cart.UserAccountId = (int)cartDto.UserAccountId;

        return Ok(await _cartRepository.Update(cart));
    }
    
    /// <summary>
    /// Delete cart by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _cartRepository.Delete(id));
    }
    
    /// <summary>
    /// Add product to user's current cart. If there is no cart yet for this user or there are carts but only sold/closed ones, new cart will be created.
    /// There is no mechanism for API users to put product in particular cart by id. API determines automatically in a proper way in what cart requested product should be placed.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="productCount"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPut("[action]")]
    public async Task<ActionResult<Cart>> AddProduct(int productId, int productCount, int userId)
    {
        var product = await _productRepository.Get(productId);
        
        if (product == null)
        {
            return NotFound("Product was not found");
        }

        var user = await _userAccountRepository.Get(userId);

        if (user == null)
        {
            return NotFound("User account not found");
        }

        var cart = _cartRepository.GetCurrentCart();

        if (cart == null)
        {
            cart = new Cart
            {
                UserAccount = user
            };
            
            var cartItem = new CartItem
            {
                Product = product,
                Cart = cart
            };

            cart.Items.Add(cartItem);

            return Ok(await _cartRepository.Add(cart));
        }
        else
        {
            var cartItem = _cartItemRepository.GetCartItemByCartIdAndProductId(cart.Id, productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Product = product,
                    Cart = cart,
                    Count = productCount,
                };
            }
            else
            {
                cartItem.Count = productCount;
            }
            
            cart.Items.Add(cartItem);

            return Ok(await _cartRepository.Update(cart));
        }
    }
}