using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleStoreApp.Web.Database;
using PeopleStoreAppDataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleStoreApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly LocalDataStorage db;
        public PeopleController(LocalDataStorage db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetPeople()
        {
            //It's fetching data, and shows in json
            return Ok(db.People);
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody] Person person)//What this frombody means ?
        {
            db.AddPerson(person);
            return Ok();
        }

        [HttpGet("(id)/photo")]
        public IActionResult GetPhoto([FromRoute] int id)
        {
            var p = db.People.First(w => w.Id == id);
            return base.File(Convert.FromBase64String(p.PictureBase64), "image/jpeg");
        }
    }
}
