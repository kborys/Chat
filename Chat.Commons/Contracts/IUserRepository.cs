using Chat.Commons.Models;

namespace Chat.Commons.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<bool> CheckExistence(string email);
        Task<int> Create(User user);
    }
}