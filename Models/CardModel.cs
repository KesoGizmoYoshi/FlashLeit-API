using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashleit_class_library.Models;
public class CardModel
{
    [Key]
    public int Id { get; set; }
    public int CollectionId { get; set; }
    public int UserId { get; set; }
    [MaxLength(250)]
    public required string Question { get; set; }
    [MaxLength(250)]
    public required string Answer { get; set; }
    public int LeitnerIndex { get; set; } = 1;
    public DateTime LastReviewedDate { get; set; } = DateTime.Now;

    public CardModel()
    {
        
    }
}
