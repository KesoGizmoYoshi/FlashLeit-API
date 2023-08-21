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
    public required string Question { get; set; }
    public required string CorrectAnswer { get; set; }
    public List<string> Answers { get; set; } = new();

    [ForeignKey("Collection")]
    public int CollectionId { get; set; }
    public CollectionModel? Collection { get; set; }

    public CardModel()
    {
        
    }
}
