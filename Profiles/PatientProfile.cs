using AutoMapper;
using HomePhysio.Models;
using HomePhysio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
           
            //CreateMap<PatientViewModel,PatientModel>().ForMember(dest=>dest.Name,opt => opt.MapFrom(src=>src.Name1)).ReverseMap();
            CreateMap<RegisterPatientViewModel,PatientModel>().ReverseMap();
            CreateMap<RegisterPhysioViewModel, PhysiotherapistModel>().ReverseMap();

        }
    }
}
