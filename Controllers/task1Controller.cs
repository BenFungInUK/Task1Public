using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using task1.Models;

namespace task1.Controllers
{
    [ApiController]
    public class task1Controller : ControllerBase
    {
        ToDoListItemModel[] items = new ToDoListItemModel[]
        {
            new ToDoListItemModel { text = "Sample1", dateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")},
            new ToDoListItemModel { text = "Sample2", dateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}
        };

        [HttpGet]
        [Route("api/items")]
        public IEnumerable<Models.ToDoListItemModel> GetAllToDoListItem()
        {
            return items;
        }
    }
}
