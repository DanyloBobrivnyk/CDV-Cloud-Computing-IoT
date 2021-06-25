using AutoMapper;
using Lab1_bobrivnyk.Rest.Context;
using Lab1_bobrivnyk.Rest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_bobrivnyk.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private AzureDbContext _context;

        public PeopleController()
        {
            _context = new AzureDbContext();
        }

        [HttpGet]
        public IActionResult GetPeople()
        {
            return Ok(_context.People);
        }
    }
}
