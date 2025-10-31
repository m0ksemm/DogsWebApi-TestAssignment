using DogsWebApi.Core.DTO;
using DogsWebApi.Core.ServiceContracts;
using DogsWebApi.Core.ServiceContracts.QueryParams;
using Microsoft.AspNetCore.Mvc;

namespace DogsWebApi.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogsController : Controller
    {
        private readonly IDogService _dogService;
        public DogsController(IDogService dogService) { _dogService = dogService; }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? attribute, [FromQuery] string? order,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var q = new DogQueryParams
            {
                Attribute = attribute,
                Order = order,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _dogService.GetAllAsync(q);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DogAddRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _dogService.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { name = created.Name }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
