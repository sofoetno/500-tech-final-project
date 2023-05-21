namespace FinalProject.Models;

public class UserAccount
{
    public int Id { get; set; }
    public int UserTypeId { get; set; }
    
    /// <summary>
    /// Full name of the user.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// User age.
    /// </summary>
    public int Age { get; set; }
    
    /// <summary>
    /// User gender.
    /// </summary>
    public string Gender { get; set; }
    
    /// <summary>
    /// User address.
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// User contact number.
    /// </summary>
    public int ContactNumber { get; set; }
    
    /// <summary>
    /// User name.
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    /// User password.
    /// </summary>
    public string Password { get; set; }

    public UserType UserType { get; set; }
    public ICollection<Payment> Payments { get; set; }
}