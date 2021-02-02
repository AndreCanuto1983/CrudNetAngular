using Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.ViewModels;
using WebAPI.ViewModels.Extensions;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Dependency Injection

        private readonly IRepository<ProductModel> _productsRepository;
        dynamic _response;

        public ProductsController(ApplicationDbContext context, IRepository<ProductModel> productsRepository)
        {
            _productsRepository = productsRepository;
            _response = new ExpandoObject();
        }

        #endregion

        /// <summary>
        /// Register products
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Insert(ProductInsertViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (model.Id != 0) return BadRequest("Não é permitido informar um id diferente de 0 para salvar");

            try
            {
                return Created("", await _productsRepository.Insert(model.Front2Entity()));
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update products
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(ProductUpdateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (model.Id <= 0) return BadRequest("Por favor informe um id maior que 0");

            try
            {
                return Ok(await _productsRepository.Update(model.Front2Entity()));
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id <= 0) return BadRequest("Por favor informe um id maior que 0");

            try
            {
                var result = await _productsRepository.Get(id);                
                var convert = result.response.Entity2Front();                
                _response.success = result.success;
                _response.response = convert;
                return Ok(_response);
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get product list
        /// Eu poderia utilizar também apenas o get para trazer apenas 1 registro e todos registos de acordo com a passagem de parâmetros, mas resolvi fazer dessa maneira para esse teste
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            try
            {
                var result = await _productsRepository.GetList();
                var convert = result.response.Select(p => p.Entity2Front());
                _response.success = result.success;
                _response.response = convert;
                return Ok(_response);
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>  
        /// 
        [AllowAnonymous]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            if (id <= 0) return BadRequest("Por favor informe um id maior que 0");

            try
            {
                var result = await _productsRepository.Delete(id);

                if (result.success == false)
                    return NotFound(result);
                else
                    return Ok(result);                
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //não utilizarei o path pois não será necessário neste momento
    }
}
