using APInegocy.DTOs;
using APInegocy.Models;

public interface IServiceManager<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task<bool> Delete(int id);

    Task<User?> LoginAsync(LoginDto dto);
}