﻿using FlashLeit_API.Data.Database;
using FlashLeit_API.Repositories.Interfaces;

namespace FlashLeit_API.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ICardRepository Cards { get; set; }
    public IUserRepository Users { get; set; }
    public ICollectionRepository Collections { get; set; }
    public ICounterRepository Counters { get; set; }
    public IUserStatsRepository UserStats { get; set; }
    public IAchievementRepository Achievements { get; set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Cards = new CardRepository(context);
        Users = new UserRepository(context);
        Collections = new CollectionRepository(context);
        Counters = new CounterRepository(context);
        UserStats = new UserStatsRepository(context);
        Achievements = new AchievementRepository(context);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }


}
