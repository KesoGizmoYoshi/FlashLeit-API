using Microsoft.AspNetCore.Mvc;


namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CountersController : ControllerBase
{

    // PUT api/<CountersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

}
