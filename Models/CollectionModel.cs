using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace flashleit_class_library.Models;
public class CollectionModel
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PublicKey { get; set; }

    [MaxLength(50)]
    public required string Title { get; set; }
    [MaxLength(250)]
    public string Description { get; set; }
    public List<CardModel>? FlashCards { get; set; } = new();
    public int AmountOfCorrectAnswers { get; set; }
    public int AmountOfInCorrectAnswers { get; set; }
    public int AmountOfCompletedRuns { get; set; }
    public bool IsPublic { get; set; }
    public int CardCount { get; set; }

    public CollectionModel()
    {
        
    }

}
