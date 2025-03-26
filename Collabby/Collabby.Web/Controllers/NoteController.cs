using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Collabby.Domain.Entities;
using Collabby.infrastructure.Context;
using Collabby.Web.Models;

namespace Collabby.Web.Controllers
{
    public class NoteController : Controller
    {
        

        // GET: Note
        public IActionResult Index()
        {
            return View();
        }

        // GET: Note/Details/5
        public IActionResult Details(int id)
        {
            return View(new NoteViewModel{NoteId = id });
        }

        // GET: Note/Create
        public IActionResult Create()
        {
            return View(new NoteViewModel());
        }

        // GET: Note/Edit/5
        public IActionResult Edit(int id)
        {
            return View(new NoteViewModel { NoteId = id });
        }

        // GET: Note/Delete/5
        public IActionResult Delete(int id)
        {
            return View(new NoteViewModel { NoteId = id });
        }
    }
}
