using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Lazulisoft.ApiCleanArchtMediatR.SWApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator
        {
            get
            {
                if (_mediator == null)
                {
                    _mediator = HttpContext.RequestServices.GetService<IMediator>();
                }
                return _mediator;
            }
        }
    }
}