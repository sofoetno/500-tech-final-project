namespace FinalProject.Models;

public class UserType
{
    public int Id { get; set; }
    
    /// <summary>
    /// User type name.
    /// </summary>
    public string TypeName { get; set; }
    
    /// <summary>
    /// User description.
    /// </summary>
    public string Description { get; set; }
    public ICollection<UserAccount> UserAccounts { get; } = new List<UserAccount>();

}