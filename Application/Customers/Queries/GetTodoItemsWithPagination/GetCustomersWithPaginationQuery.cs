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

namespace Application.Customers.Queries.GetCustomersWithPagination
{

    public class GetCustomersWithPaginationQuery : IRequest<PaginatedList<CustomerBriefDto>>
    {
        //public int ListId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetCustomersWithPaginationQueryHandler : IRequestHandler<GetCustomersWithPaginationQuery, PaginatedList<CustomerBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CustomerBriefDto>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                //.Where(x => x.Id == request.ListId)
                .OrderBy(x => x.Firstname)
                .ProjectTo<CustomerBriefDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}