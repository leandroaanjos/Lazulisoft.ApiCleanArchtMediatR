using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Queries
{
    public class GetAllCharactersQuery : IRequest<List<CharacterDto>>
    {
    }
}
