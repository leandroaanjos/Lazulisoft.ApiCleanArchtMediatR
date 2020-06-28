using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Dtos;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Enums;
using MediatR;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Commands
{
    public class CreateCharacterCommand : IRequest<CharacterDto>
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public string Homeworld { get; set; }

        public string Species { get; set; }

        public Gender Gender { get; set; }

        public string Occupation { get; set; }
    }
}
