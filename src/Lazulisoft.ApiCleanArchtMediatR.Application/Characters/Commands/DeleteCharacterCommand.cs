using MediatR;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Commands
{
    public class DeleteCharacterCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
