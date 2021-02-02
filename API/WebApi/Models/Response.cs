using System.Collections.Generic;
using WebAPI.Interfaces;

namespace WebAPI.Models
{
    public class Response : IResponse<string>
    {
        public bool success { get; set; }
        public string response { get; set; }
    }

    public class ResponseObj : IResponse<ProductModel>
    {
        public bool success { get; set; }
        public ProductModel response { get; set; }
    }

    public class ResponseList : IResponseList<ProductModel>
    {
        public bool success { get; set; }
        public IEnumerable<ProductModel> response { get; set; }
    }
}
