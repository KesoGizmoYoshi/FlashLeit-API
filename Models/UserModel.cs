
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    [MaxLength(50)]
    public required string AvatarUrl { get; set; }
    public List<CollectionModel>? Collections { get; set; } = new();
    public UserStatsModel? UserStats { get; set; } = new();

    public UserModel()
    {
        
    }
}

