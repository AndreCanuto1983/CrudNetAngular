using Core.Helpers;
using WebAPI.Models;

namespace WebAPI.ViewModels.Extensions
{
    public static class ProductUpdateExtension
    {
        public static ProductModel Front2Entity(this ProductUpdateViewModel model)
        {
            return new ProductModel()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Image = model.Image != null ? Util.ByteEncode(model.Image) : null
            };
        }
    }
}