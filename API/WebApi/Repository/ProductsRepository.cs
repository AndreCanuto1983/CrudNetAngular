using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class ProductsRepository : IRepository<ProductModel>
    {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        IResponse<string> _response;
        IResponse<ProductModel> _responseObj;
        IResponseList<ProductModel> _responseList;

        public ProductsRepository(
            ApplicationDbContext context,
            IResponse<string> response,
            IResponse<ProductModel> responseObj,
            IResponseList<ProductModel> responseList
            )
        {
            _context = context;
            _response = response;
            _responseObj = responseObj;
            _responseList = responseList;
        }

        #endregion

        public async Task<IResponse<string>> Insert(ProductModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Entry(model).State = EntityState.Added;

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    _response.success = true;
                    _response.response = "Produto salvo com sucesso!";

                    return _response;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomErrorException("Erro ao tentar salvar o produto", ex);
                }
            }
        }

        public async Task<IResponse<string>> Update(ProductModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var result = await _context.Products.AsNoTracking().Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                    if (result == null)
                    {
                        _response.success = false;
                        _response.response = "Para realizar a atualização do registro, informe um id válido";
                        return _response;
                    }

                    _context.Entry(model).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    _response.success = true;
                    _response.response = "Produto alterado com sucesso!";

                    return _response;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomErrorException("Erro ao tentar salvar o produto", ex);
                }
            }
        }

        public async Task<IResponse<string>> Delete(long id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    ProductModel product = await _context.Products.AsNoTracking().Where(n => n.Id == id).FirstOrDefaultAsync();

                    if (product == null)
                    {
                        _response.success = false;
                        _response.response = "O id informado não foi encontrado";
                        return _response;
                    }

                    _context.Entry(product).State = EntityState.Deleted;

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    _response.success = true;
                    _response.response = "Produto excluído com sucesso!";

                    return _response;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomErrorException("Erro ao tentar excluir o produto", ex);
                }
            }
        }

        public async Task<IResponse<ProductModel>> Get(long id)
        {
            try
            {
                ProductModel products = await _context.Products.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();

                _responseObj.success = true;
                _responseObj.response = products;

                return _responseObj;
            }
            catch (Exception ex)
            {
                throw new CustomErrorException("Erro ao tentar obter o produto", ex);
            }
        }

        public async Task<IResponseList<ProductModel>> GetList()
        {
            try
            {
                IEnumerable<ProductModel> products = await _context.Products.AsNoTracking().ToListAsync();
                _responseList.success = true;
                _responseList.response = products;

                return _responseList;
            }
            catch (Exception ex)
            {
                throw new CustomErrorException("Erro ao tentar obter a lista de produtos", ex);
            }
        }
    }
}
