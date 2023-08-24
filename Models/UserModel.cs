
using System.ComponentModel.DataAnnotations;

namespace flashleit_class_library.Models;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Email { get; set; }
    [MaxLength(50)]
    public required string AccountName { get; set; }
    public required string UserName { get; set; }
    public required List<CollectionModel> Collections { get; set; } = new();
    public required UserStatsModel UserStats { get; set; }

    public UserModel()
    {
        
    }
}

