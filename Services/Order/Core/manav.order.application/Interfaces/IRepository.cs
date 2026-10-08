using System.Linq.Expressions;

namespace manav.order.applicaiton.Interfaces;

public interface IRepository<T> where T: class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
}

//COMMAND QUERIES RESPONSE SERVİCE'de read ve write işlmlerini ayırmak için kullanılır
//Read->Result
//Paramtre->Queries
//Parametreler(Ekleme Silme Güncelleme)->Commands
//Crud->Handlers