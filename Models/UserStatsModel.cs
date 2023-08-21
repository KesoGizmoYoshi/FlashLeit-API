using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashleit_class_library.Models;
public class UserStatsModel
{
    [Key]
    public int Id { get; set; }
    public List<CounterModel> Counters { get; set; }
    public List<AchievementModel> Achievements { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserModel? User { get; set; }
}
