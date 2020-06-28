using AutoMapper;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Commands;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Dtos;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.MappingProfiles
{
    public class CharacterMappingProfile : Profile
    {
        public CharacterMappingProfile()
        {
            CreateMap<CreateCharacterCommand, Character>().ReverseMap();
            CreateMap<UpdateCharacterCommand, Character>().ReverseMap();
            CreateMap<Character, CharacterDto>().ReverseMap();
        }
    }
}
