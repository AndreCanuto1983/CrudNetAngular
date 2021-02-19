using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IRepository<T>
    {
        Task<IResponse<string>> Insert(T model);
        Task<IResponse<string>> Update(T model);
        Task<IResponse<string>> Delete(long id);
        Task<IResponse<ProductModel>> Get(long id);
        Task<IResponseList<ProductModel>> GetList();
    }
}
