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

                config.CreateMap<Player, PlayerVM>();
                config.CreateMap<PlayerVM, Player>();

                config.CreateMap<Team, TeamDTO>();
                config.CreateMap<Player, PlayerDTO>();

                config.CreateMap<TeamDTO, TeamVM>();
                config.CreateMap<PlayerDTO, PlayerVM>();
            });
        }
    }
}
