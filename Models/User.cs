namespace KutuphaneAPI.Models;

public class User : BaseEntity {
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public string FullName => $"{Name} {LastName}"; // Burayı geri ekledik
    public required string Email { get; set; }
    public int Age { get; set; }
    public required string Address { get; set; }
}