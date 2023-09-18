
using FlashLeit_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace flashleit_class_library.Models;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Email { get; set; }
    [MaxLength(50)]
    public string AccountName { get; set; }
    public string UserName { get; set; }
    public string SelectedAvatarUrl { get; set; }
    public List<AvatarModel> UserAvatars { get; set; } = new();
    public List<CollectionModel>? Collections { get; set; } = new();
    public List<AchievementModel> Achievements { get; set; } = new();
    public UserModel()
    {
        
    }
}

