using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.DataAccess.Model;

namespace SimpleAPI.DataAccess.Service;

public class UserService
{
    public UserService(ApplicationContext context)
    {
        _context = context;
    }

    private readonly ApplicationContext _context;

    public async Task<IEnumerable<User>> GetAsync()
    {
         var transaction = _context.Database.BeginTransaction();

        var users = _context.Users;
        await transaction.CommitAsync();

        return _context.Users;
    }

    public async Task<User?> GetAsync(int id)
    {
        using var transaction = _context.Database.BeginTransaction();

        var user = await _context.Users.FindAsync(id);
        await transaction.CommitAsync();

        return user;
    }

    public async Task<User> CreateAsync(User user)
    {
        using var transaction = _context.Database.BeginTransaction();

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return user;
    }

    public async Task DeleteAsync()
    {
        using var transaction = _context.Database.BeginTransaction();

        await _context.Users.ExecuteDeleteAsync();
        await transaction.CommitAsync();
    }
}
