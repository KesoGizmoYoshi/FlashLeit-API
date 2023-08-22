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
    public int CardId { get; set; }
    public int CollectionId { get; set; }
    [MaxLength(150)]
    public required string Question { get; set; }
    [MaxLength(250)]
    public required string CorrectAnswer { get; set; }
    [MaxLength(150)]
    public string? IncorrectAnswerOne { get; set; }
    [MaxLength(150)]
    public string? IncorrectAnswerTwo { get; set; }
    [MaxLength(150)]
    public string? IncorrectAnswerThree { get; set; }

    public CardModel()
    {
        
    }
}
