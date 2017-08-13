using TeamManager.Logic.DTOs;
using TeamManager.Model.Entities;
using TeamManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Web
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Team, TeamVM>();
                config.CreateMap<TeamVM, Team>();

                //todo: consider possible transitivity via TeamVM
                config.CreateMap<Team, SelectableTeamVM>();
                config.CreateMap<SelectableTeamVM, Team>();

                config.CreateMap<Course, CourseVM>()
                      .ForMember(dest => dest.SelectableTeams,
                                 opt => opt.MapFrom(src => src.Teams));
                config.CreateMap<CourseVM, Course>()
                      .ForMember(dest => dest.Teams,
                                 opt => opt.MapFrom(src => src.SelectableTeams.Where(sg => sg.Selected)));

                config.CreateMap<Player, PlayerVM>();
                config.CreateMap<PlayerVM, Player>()
                      .ForMember(dest => dest.TeamId,
                                 opt => opt.MapFrom(src => src.Team != null & src.Team.Id != 0 ?
                                                    (int?)src.Team.Id : null));

                config.CreateMap<Team, TeamDTO>();
                config.CreateMap<Course, CourseDTO>();
                config.CreateMap<Player, PlayerDTO>();

                config.CreateMap<TeamDTO, TeamVM>();
                config.CreateMap<CourseDTO, CourseVM>();
                config.CreateMap<PlayerDTO, PlayerVM>();
            });
        }
    }
}
