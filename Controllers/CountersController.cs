using Microsoft.AspNetCore.Mvc;


namespace FlashLeit_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CountersController : ControllerBase
{
    // GET: api/<CountersController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<CountersController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<CountersController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<CountersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CountersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
