using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using MediatR;
using Application.Customers.Queries.GetCustomersWithPagination;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomerById
{

    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public long Id { get; set; }


        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCustomerByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                return await _context.Customers
                    .Where(x => x.Id == request.Id)
                    .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
            }
        }

    }
}