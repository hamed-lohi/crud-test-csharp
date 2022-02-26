using Application.Customers.Queries.GetCustomersWithPagination;
using System;

namespace Application.Customers.Queries.GetCustomerById
{

    public class CustomerDto : CustomerMinDto
    {
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

    }
}