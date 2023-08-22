
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace flashleit_class_library.Models;
public class CounterModel
{
    [Key]
    public required int CounterId { get; set; }
    [MaxLength(50)]
    public required string Title { get; set; }
    public int CollectionId { get; set; }
    public int UserStatsId { get; set; }
    public int AmountOfCardsAnswered { get; set; }
    public int AmountOfCorrectAnswers { get; set; }
    public int AmountOfIncorrectAnswers { get; set; }
    public int AmountOfPerfectRuns { get; set; }
    public int TimesStarted { get; set; }
    public int TimesFinished { get; set; }

    public CounterModel()
    {
        
    }
}
