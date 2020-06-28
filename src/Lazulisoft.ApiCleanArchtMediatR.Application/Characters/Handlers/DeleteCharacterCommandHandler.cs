using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Commands;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Data;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Handlers
{
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCharacterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var character = await _unitOfWork.CharacterRepository.GetById(request.Id);
                _unitOfWork.CharacterRepository.Delete(character);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.RollbackTransaction();
            }
            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}