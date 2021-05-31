﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturent.Data;
using Resturent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturent.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }


        //GET
        public async Task<IActionResult> Index()
        {
            return View(await _db.Category.ToListAsync());
        }

        //Get-For Create
        public IActionResult Create()
        {
            return View();
        }


        //Post- Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return View(category);
        }

        //GET- EDIT
        public async Task<IActionResult> Edit (int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var Category =await _db.Category.FindAsync(Id);

            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }

        //GET- Delete
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var Category = await _db.Category.FindAsync(Id);

            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

    }
}
