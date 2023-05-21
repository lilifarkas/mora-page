using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class UserService: IUserService
{
    private readonly MedicalContext _context;

    public UserService(MedicalContext context)
    {
        _context = context;
    }
    public async Task<User> Add(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return await _context.Users
            .Where(p => p.ID == user.ID)
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException();
    }

    public async Task<User> GetById(long id)
    {
        return await _context.Users.FirstOrDefaultAsync(t => t.ID == id);
    }
    
    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(t => t.Email == email); 
    }

    public async Task<User> GetByName(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(t => t.Name == name); 
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task Update(User entity, long id)
    {
        var user = await _context.Users.FirstAsync(t => t.ID == id);
    
        if (user != null)
        {
            //_context.Entry(user).CurrentValues.SetValues(entity);
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.Phone = entity.Phone;
            user.Password = entity.Password;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("User not found."); // Or handle the case when the user is not found.
        }
    }

    public async Task Delete(long id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {id} not found.");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}