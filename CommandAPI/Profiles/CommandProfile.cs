using AutoMapper;

using CommandAPI.Dtos;

namespace CommandAPI.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, CommandReadDto>().ReverseMap();
            CreateMap<Command, CommandCreateDto>().ReverseMap();
            CreateMap<Command, CommandUpdateDto>().ReverseMap();
        }
    }
}
