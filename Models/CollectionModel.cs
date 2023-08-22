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
    public int CollectionId { get; set; }
    public int UserId { get; set; }
    [MaxLength(50)]
    public required string Title { get; set; }
    public List<CardModel> FlashCards { get; set; } = new();
    public List<UserModel> Users { get; set; } = new();
    public int CounterId { get; set; }

    public CollectionModel()
    {
        
    }

}
