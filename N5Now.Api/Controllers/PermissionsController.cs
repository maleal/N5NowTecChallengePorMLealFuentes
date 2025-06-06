using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using N5Now.Core.DTOs;
using N5Now.Core.Entities;
using N5Now.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace N5Now.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _service;
        private readonly ILogger<PermissionsController> _logger;


        public PermissionsController(IPermissionService permissionService, ILogger<PermissionsController> logger)
        {
            _service = permissionService;
            _logger = logger;
        }

        // GET: api/<PermissionsController>
        [HttpGet("GetPermissions")]
        public async Task<ActionResult<IEnumerable<PermissionResponseDto>>> Get()
        {
            
            _logger.LogInformation($"GetPermissions: Get All");

            var result = await _service.GetPermissionsAsync();
            if (result == null)
            {
                _logger.LogError($"GetPermissions: Error");
                return NotFound();
            }else 
                return Ok(result);
            
            //return new string[] { "value1", "value2" };
        }
         

        // POST api/<PermissionsController>
        [HttpPost("RequestPermissions")]
        public async Task<ActionResult<PermissionResponseDto>> Post([FromBody] PermissionRequestDto dto)
        {
            _logger.LogInformation($"RequestPermissions: Registrar para Employee:{dto.EmployeeSurName}");
            var result = await _service.RequestPermissionAsync(dto);
            if (result == null)
            {
                _logger.LogInformation($"RequestPermissions: Error al registrar Employee:{dto.EmployeeSurName}");
                return NotFound();
            }
            return Ok(result);
        }

        // PUT api/<PermissionsController>/5
        [HttpPut("Modify/{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] PermissionRequestDto dto)
        {
            var result = await _service.ModifyPermissionAsync(id, dto);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        //// DELETE api/<PermissionsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        
        // GET api/<PermissionsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
    }
}
