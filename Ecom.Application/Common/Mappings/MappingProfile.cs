using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Application.Helper.DTOs;
using Ecom.Domain.Entities.Identity;

namespace Ecom.Application.Common.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}