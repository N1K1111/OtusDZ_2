using AutoMapper;
using OtusDZ_2.Models;
using WebApi.Entities;
namespace WebApi
{
    public class AppMapping : Profile
    {
        public AppMapping()
        {
            CreateMap<Customer, CustomerEntity>();
        }
    }
}
