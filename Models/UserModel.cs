
using System.ComponentModel.DataAnnotations;

namespace flashleit_class_library.Models;

public class UserModel
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Email { get; set; }
    [MaxLength(50)]
    public required string UserName { get; set; }
    public required List<CollectionModel> Collections { get; set; } = new();
    public required List<UserStatsModel> UserStats { get; set; } = new();

    public UserModel()
    {
        
    }
}

