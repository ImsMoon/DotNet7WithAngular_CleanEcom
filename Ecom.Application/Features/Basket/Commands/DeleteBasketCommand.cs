
using Ecom.Application.Features.Basket.Contracts;
using MediatR;

namespace Ecom.Application.Features.Basket.Commands
{
    public class DeleteBasketCommand:IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteBasketCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, bool>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            if(!string.IsNullOrEmpty(request.Id))
            {
                return await _basketRepository.DeleteBasketAsync(request.Id);
            }
            return false;
        }
    }
}