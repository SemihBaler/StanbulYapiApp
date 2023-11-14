using AutoMapper;

using ApplicationCore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTO_s.AccountDTO;

namespace Infrastructure.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<LogInDTO,AppUser>().ReverseMap();
        }
    }
}
