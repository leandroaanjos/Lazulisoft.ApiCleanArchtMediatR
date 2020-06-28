using AutoMapper;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Queries;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Dtos;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Data;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Handlers
{
    public class GetAllCharactersQueryHandler : IRequestHandler<GetAllCharactersQuery, List<CharacterDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCharactersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CharacterDto>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CharacterRepository.GetAll();
            return _mapper.Map<List<CharacterDto>>(result);
        }
    }
}