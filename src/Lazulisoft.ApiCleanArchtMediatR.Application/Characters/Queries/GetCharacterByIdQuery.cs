using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Dtos;
using MediatR;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Queries
{
    public class GetCharacterByIdQuery : IRequest<CharacterDto>
    {
        public int Id { get; set; }
    }
}
