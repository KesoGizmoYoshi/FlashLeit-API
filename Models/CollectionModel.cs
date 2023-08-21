using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashleit_class_library.Models;
public class CollectionModel
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Author")]
    public int UserId { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    public List<CardModel>? FlashCards { get; set; }
    public List<UserModel>? Users { get; set; }

    public CollectionModel()
    {
        
    }

}
