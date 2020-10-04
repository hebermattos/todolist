using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace todolist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Todo
            {
                Descricao = "tarefa " + index
            })
            .ToArray();
        }
    }
}
