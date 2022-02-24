using Application.Common.Models;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
//using Application.Customers.Commands.UpdateTodoItemDetail;
using Application.Customers.Queries.GetCustomersWithPagination;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ApiControllerBase
    {
        //[HttpGet]
        //public async Task<ActionResult<PaginatedList<CustomerBriefDto>>> GetCustomersWithPagination([FromQuery] GetCustomersWithPaginationQuery query)
        //{
        //    return await Mediator.Send(query);
        //}

        [HttpGet]
        public async Task<List<CustomerBriefDto>> GetCustomersWithPagination([FromQuery] GetCustomersWithPaginationQuery query)
        {
            //return new List<CustomerBriefDto> { new CustomerBriefDto { Id = 0, Firstname = "hhhh" } };
            //var dd = new PaginatedList()
            var temp =  await Mediator.Send(query);
            return temp.Items;
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, UpdateCustomerCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        //[HttpPut("[action]")]
        //public async Task<ActionResult> UpdateItemDetails(int id, UpdateCustomerDetailCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCustomerCommand { Id = id });

            return NoContent();
        }
    }
}