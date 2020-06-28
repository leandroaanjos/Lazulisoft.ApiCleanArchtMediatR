using AutoMapper;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Commands;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Data;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Handlers
{
    public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCharacterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var character = _mapper.Map<Character>(request);
                character.UpdatedOn = DateTime.Now;

                _unitOfWork.CharacterRepository.Update(character);
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