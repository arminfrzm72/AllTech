using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AllTech.DataLayer.Context;
using AllTech.DomainClasses.NewsGroup;
using AllTech.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using AllTech.Services.Attributes;

namespace AllTech.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(1)]
    public class NewsGroupsController : Controller
    {
        private readonly INewsGroupRepository _newsGroupRepository;
        public NewsGroupsController(INewsGroupRepository newsGroupRepository)
        {
            _newsGroupRepository = newsGroupRepository;
        }

        // GET: Admin/NewsGroups
        public IActionResult Index()
        {
            return View(_newsGroupRepository.GetAllNewsGroup());
        }

        // GET: Admin/NewsGroups/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsGroup = _newsGroupRepository.GetNewsGroupById(id.Value);
            if (newsGroup == null)
            {
                return NotFound();
            }

            return View(newsGroup);
        }

        // GET: Admin/NewsGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupID,GroupTitle")] NewsGroup newsGroup)
        {
            if (ModelState.IsValid)
            {
                _newsGroupRepository.InserNewsGroup(newsGroup);
                _newsGroupRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(newsGroup);
        }

        // GET: Admin/NewsGroups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsGroup = _newsGroupRepository.GetNewsGroupById(id.Value);
            if (newsGroup == null)
            {
                return NotFound();
            }
            return View(newsGroup);
        }

        // POST: Admin/NewsGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("GroupID,GroupTitle")] NewsGroup newsGroup)
        {
            if (id != newsGroup.GroupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _newsGroupRepository.UpdateNewGroup(newsGroup);
                    _newsGroupRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsGroupExists(newsGroup.GroupID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsGroup);
        }

        // GET: Admin/NewsGroups/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsGroup = _newsGroupRepository.GetNewsGroupById(id.Value);
            if (newsGroup == null)
            {
                return NotFound();
            }

            return View(newsGroup);
        }

        // POST: Admin/NewsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _newsGroupRepository.DeleteNewsGroup(id);
            _newsGroupRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsGroupExists(int id)
        {
            return _newsGroupRepository.NewsGroupExists(id);
        }
    }
}
