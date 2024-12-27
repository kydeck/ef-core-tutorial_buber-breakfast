using BuberBreakfast.Models;
using BuberBreakfast.Persistence;
using BuberBreakfast.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private readonly BuberBreakfastDbContext _dbContext;

    public BreakfastService(BuberBreakfastDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _dbContext.Add(breakfast);
        _dbContext.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        // Ensure breakfast exists in DB before attempting to delete it
        var breakfast = _dbContext.Breakfasts.Find(id);

        if (breakfast is null)
        {
            return Errors.Breakfast.NotFound;  
        }

        _dbContext.Remove(breakfast);

        _dbContext.SaveChanges();

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        // Ensure breakfast exists in DB before attempting to get it
        var breakfast = _dbContext.Breakfasts.Find(id);

        if (breakfast is Breakfast)
        {
            return breakfast;
        }
        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        // EF tracks all objects by default, so passing in a "new" breakfast with the ID of an existing breakfast makes it sad.
        // Updating method to be ".Any()" to not run into issues with trying to work on a duplicate tracked entity, but instead work on the only tracked entity in the DB
        var isNewlyCreated = !_dbContext.Breakfasts.Any(b => b.Id == breakfast.Id);

        if (isNewlyCreated)
        {
            _dbContext.Breakfasts.Add(breakfast);
        }
        else
        {
            _dbContext.Breakfasts.Update(breakfast);
        }

        _dbContext.SaveChanges();

        return new UpsertedBreakfast(isNewlyCreated);
    }
}
