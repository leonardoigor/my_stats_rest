using Microsoft.AspNetCore.Mvc;

namespace MyStats_Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        // GET: api/Home
        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }

        // GET: api/Home/Details/5
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id)
        {
            return Ok();

        }

        // POST: api/Home/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] IFormCollection collection)
        {

            return Ok();

        }

        // GET: api/Home/Edit/5
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            return Ok();
        }

        // POST: api/Home/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] IFormCollection collection)
        {

            return Ok();

        }



        // POST: api/Home/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] IFormCollection collection)
        {

            return Ok();

        }
    }
}
