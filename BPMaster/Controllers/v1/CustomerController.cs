using Application.Settings;
using BPMaster.Domains.Dtos;
using BPMaster.Services;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Services;

namespace BPMaster.Controllers.v1
{
    public class CustomerController(IServiceProvider service) : BaseV1Controller<CustomerService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Customer
        /// </summary>
        [HttpGet("Customerall")]
        public async Task<IActionResult> GetBuilding()
        {
            var Customer = await _service.GetAllCustomer();
            return Success(Customer);
        }
        /// <summary>
        /// this is api get by id Customer
        /// </summary>
        [HttpGet("getCustomerbyid")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            return Success(await _service.GetByIDCustomer(id));
        }
        /// <summary>
        /// this is api create a new Customer
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto dto)
        {
            return CreatedSuccess(await _service.CreateCustomerAsync(dto));
        }
        /// <summary>
        /// this is api update Customer
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerDto dto)
        {
            return Success(await _service.UpdateCustomerAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Customer
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _service.DeleteCustomerAsync(id);
            return Success("delete Success");
        }
    }
}

