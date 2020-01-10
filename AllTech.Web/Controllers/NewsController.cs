using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllTech.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AllTech.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        [Route("News/{newsId}")]
        public IActionResult ShowNews(int newsId)
        {
            var news = _newsRepository.GetNewsById(newsId);
            if (news!=null)
            {
                news.Visit += 1;
                _newsRepository.UpdateNews(news);
                _newsRepository.Save();
            }
            return View(news);
        }
        [Route("Group/{groupId}/{title}")]
        public IActionResult ShowNewsByGroupId(int groupId,string title)
        {
            ViewBag.GroupTitle = title;
            return View(_newsRepository.GetNewsByGroupId(groupId));
        }
        [Route("Search")]
        public IActionResult Search(string q)
        {
            return View(_newsRepository.Search(q));
        }
    }
}