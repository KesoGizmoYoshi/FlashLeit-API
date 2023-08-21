using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashleit_class_library.Models;
public class AchievementModel
{
    [Key]
    public required int Id { get; set; }
    [MaxLength (150)]
    public required string Name { get; set; }
    public bool IsAchieved { get; set; } = false;
}
