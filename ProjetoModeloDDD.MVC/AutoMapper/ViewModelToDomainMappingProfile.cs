using AutoMapper;
using SASF.Domain.Entities;
using SASF.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SASF.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public ViewModelToDomainMappingProfile()
        {   
            CreateMap<Funcionario, FuncionarioViewModel>();
            CreateMap<Plano, PlanoViewModel>();
            CreateMap<Ficha,FichaViewModel>();
            CreateMap<Dependente,DependenteViewModel>();
            CreateMap<Titulo, TituloViewModel>();
        }
        //protected override void Configure()
        //{

        //}
    }
}