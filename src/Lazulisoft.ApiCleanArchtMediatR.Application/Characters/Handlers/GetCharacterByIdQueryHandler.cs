using AutoMapper;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Dtos;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Queries;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Handlers
{
    public class GetCharacterByIdQueryHandler : IRequestHandler<GetCharacterByIdQuery, CharacterDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCharacterByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CharacterDto> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CharacterRepository.GetById(request.Id);
            return _mapper.Map<CharacterDto>(result);
        }
    }
}