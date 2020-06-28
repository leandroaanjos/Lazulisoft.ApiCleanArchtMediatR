using AutoMapper;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Commands;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Dtos;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Data;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Handlers
{
    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, CharacterDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCharacterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CharacterDto> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = _mapper.Map<Character>(request);

            _unitOfWork.BeginTransaction();
            try
            {
                character.CreatedOn = DateTime.Now;

                _unitOfWork.CharacterRepository.Add(character);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.RollbackTransaction();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CharacterDto>(character);
        }
    }
}