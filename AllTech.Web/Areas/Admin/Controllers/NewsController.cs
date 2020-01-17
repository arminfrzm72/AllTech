using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AllTech.DataLayer.Context;
using AllTech.DomainClasses.News;
using AllTech.Utilities.Convertor;
using Microsoft.AspNetCore.Http;
using System.IO;
using AllTech.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using AllTech.Services.Attributes;

namespace AllTech.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(1)]
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly INewsGroupRepository _newsGroupRepository;
        public NewsController(INewsRepository newsRepository,INewsGroupRepository newsGroupRepository)
        {
            _newsRepository = newsRepository;
            _newsGroupRepository = newsGroupRepository;
        }

        // GET: Admin/News
        public  IActionResult Index()
        {
            
            return View(_newsRepository.GetAllNews());
        }

        // GET: Admin/News/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news =  _newsRepository.GetNewsById(id.Value);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_newsGroupRepository.GetAllNewsGroup(), "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsID,GroupID,NewsTitle,ShortDescription,MainText,Visit,ImageName,Tags,Source,ShowInSlider,CreateDate")] News news,IFormFile imgUp)
        {
            if (ModelState.IsValid)
            {
                news.Visit = 0;
                news.CreateDate = DateTime.Now;

                if (imgUp!=null)
                {
                    news.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                    var savePath = Path.Combine(
                    Directory.GetCurrentDirectory(),"wwwroot/NewsImages", news.ImageName);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await imgUp.CopyToAsync(stream);
                        await stream.FlushAsync();
                        

                    }
                }

                _newsRepository.InsertNews(news);
                _newsRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_newsGroupRepository.GetAllNewsGroup(), "GroupID", "GroupTitle", news.GroupID);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news =  _newsRepository.GetNewsById(id.Value);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_newsGroupRepository.GetAllNewsGroup(), "GroupID", "GroupTitle", news.GroupID);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsID,GroupID,NewsTitle,ShortDescription,MainText,Visit,ImageName,Tags,Source,ShowInSlider,CreateDate")] News news,IFormFile imgUp)
        {
            if (id != news.NewsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imgUp != null)
                    {
                        if (news.ImageName==null)
                        {
                            news.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                        }                    
                        var savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/NewsImages", news.ImageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await imgUp.CopyToAsync(stream);
                            await stream.FlushAsync();


                        }
                    }
                    _newsRepository.UpdateNews(news);
                    _newsRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsID))
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
            ViewData["GroupID"] = new SelectList(_newsGroupRepository.GetAllNewsGroup(), "GroupID", "GroupTitle", news.GroupID);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _newsRepository.GetNewsById(id.Value);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var news = _newsRepository.GetNewsById(id);
             _newsRepository.DeleteNews(news);
            if (news.ImageName!=null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/NewsImages", news.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _newsRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _newsRepository.NewsExists(id);
        }
    }
}
