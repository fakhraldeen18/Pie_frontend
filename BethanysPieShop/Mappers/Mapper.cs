using AutoMapper;
using BackendPieProject.Dtos;
using BethanysPieShop.Dtos;
using BethanysPieShop.Models;

namespace BackendPieProject.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Pie,PieReadDTO>();
			CreateMap<PieReadDTO,Pie>();

		}
    }
}
