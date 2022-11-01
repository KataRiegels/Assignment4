using AutoMapper;
using DataLayer;
using DataLayer.Model;


namespace WebServer.Models.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListModel>();

            CreateMap<Product, ProductModel>();
        }
    }
}