using Application.Common.Mappings;
using Domain.Entities;
using System;

namespace Application.Customers.Queries.GetCustomersWithPagination
{

    public class CustomerBriefDto : IMapFrom<Customer>
    {
        public long Id { get; set; }

        //public int ListId { get; set; }

        //public string Firstname { get; set; }

        //public bool Done { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}