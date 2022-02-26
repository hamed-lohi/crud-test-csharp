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
using System;

namespace Application.Customers.Queries.GetCustomersWithPagination
{

    public class GetCustomersWithPaginationQuery : IRequest<PaginatedList<CustomerMinDto>>
    {
        public DateTime? DateOfBirthFrom { get; set; }
        public DateTime? DateOfBirthTo { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;


        public class GetCustomersWithPaginationQueryHandler : IRequestHandler<GetCustomersWithPaginationQuery, PaginatedList<CustomerMinDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCustomersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PaginatedList<CustomerMinDto>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken)
            {
                return await _context.Customers
                    .Where(x => x.DateOfBirth >= (request.DateOfBirthFrom??DateTime.MinValue) && x.DateOfBirth <= (request.DateOfBirthTo??DateTime.MaxValue))
                    .OrderBy(x => x.Firstname)
                    .ProjectTo<CustomerMinDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }

    }
}