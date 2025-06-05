using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using N5Now.Core.DTOs;
using N5Now.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace N5Now.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _service;
        public PermissionsController(IPermissionService permissionService)
        {
            _service = permissionService;
        }

        // GET: api/<PermissionsController>
        [HttpGet("GetPermissions")]
        public async Task<ActionResult<IEnumerable<PermissionResponseDto>>> Get()
        {
            try
            {
                var result = await _service.GetPermissionsAsync();
                if (result == null)
                {
                    return NotFound();
                }else 
                    return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
            //return new string[] { "value1", "value2" };
        }
         

        // POST api/<PermissionsController>
        [HttpPost("RequestPermissions")]
        public async Task<ActionResult<PermissionResponseDto>> Post([FromBody] PermissionRequestDto dto)
        {
            var result = await _service.RequestPermissionAsync(dto);
            if (result == null)
            {
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
