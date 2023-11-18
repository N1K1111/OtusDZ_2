using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OtusDZ_2.Models;
using OtusDZ_2.Repository;
using WebApi.Entities;

namespace OtusDZ_2.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly IRepository<CustomerEntity> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(IRepository<CustomerEntity> repository, IMapper mapper)
        {
            _customerRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromRoute] long id)
        {
            var res = await _customerRepository.GetAsync(id);
            if (res == null)
            {
                return NotFound("Клиент с таким ID не найден");
            }

            return Ok(res);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] Customer customer)
        {
            var exist = await _customerRepository.GetAsync(customer.Id);
            if (exist != null)
            {
                return Conflict($"Клиент с ID = {exist.Id}");
            }

            var res = await _customerRepository.AddAsync(_mapper.Map<CustomerEntity>(customer));

            return Ok($"Клиент c ID = {res.Id} успешно добавлен!");
        }
    }
}
