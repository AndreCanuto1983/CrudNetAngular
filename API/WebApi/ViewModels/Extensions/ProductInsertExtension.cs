using Core.Helpers;
using WebAPI.Models;

namespace WebAPI.ViewModels.Extensions
{
    public static class ProductInsertExtension
    {
        public static ProductModel Front2Entity(this ProductInsertViewModel model)
        {
            return new ProductModel()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Image = model.Image != null ? Util.ByteEncode(model.Image) : null
            };
        }

        public static ProductInsertViewModel Entity2Front(this ProductModel entity)
        {
            return new ProductInsertViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Image = entity.Image != null ? Util.ByteDecode(entity.Image) : null
            };
        }
    }
}
