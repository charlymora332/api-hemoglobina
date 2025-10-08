using AutoMapper;
using RetoHemoglobina.Application.DTOs; // DTOs
using RetoHemoglobina.Domain.Models; // Entidades del dominio

namespace RetoHemoglobina.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // De DTO → Modelo
            CreateMap<PacienteRequest, Paciente>().ReverseMap();
        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RetoHemoglobina.Application.Mappings
//{
//    internal class AutoMapperProfile
//    {
//    }
//}


