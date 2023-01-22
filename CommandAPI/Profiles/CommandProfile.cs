using AutoMapper;

using CommandAPI.Dtos;

namespace CommandAPI.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, CommandReadDto>().ReverseMap();
        }
    }
}
