using AutoMapper;
using SASF.Domain.Entities;
using SASF.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SASF.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {   
            CreateMap<FuncionarioViewModel, Funcionario>();
            CreateMap<PlanoViewModel,Plano>();
            CreateMap<FichaViewModel, Ficha>();
            CreateMap<DependenteViewModel, Dependente>();
            CreateMap<TituloViewModel, Titulo>();

        }
    }
}