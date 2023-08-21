using flashleit_class_library.Models;

namespace FlashLeit_API.Repositories.Interfaces;

public interface ICardRepository : IRepository<CardModel>
{

    // Here we implement operations specific to this 
    // particular repository additional to all the 
    // generic / default operations in IRepository.
}
