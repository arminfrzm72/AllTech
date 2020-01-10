using AllTech.DataLayer.Context;
using AllTech.DomainClasses.News;
using AllTech.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.Services.Services
{
    public class NewsRepository : INewsRepository
    {
        public readonly AllTechDbContext _db;
        public NewsRepository(AllTechDbContext db)
        {
            _db = db;
        }
        public IEnumerable<News> GetAllNews()
        {
           return _db.News.ToList();
        }

        public News GetNewsById(int newsId)
        {
            return _db.News.Find(newsId);
        }

        public void InsertNews(News news)
        {
            _db.News.Add(news);
        }
        public void UpdateNews(News news)
        {
            _db.Entry(news).State = EntityState.Modified;
        }
        public void DeleteNews(News news)
        {
            _db.Entry(news).State = EntityState.Deleted;
        }

        public void DeleteNews(int newsId)
        {
            var news= GetNewsById(newsId);
            DeleteNews(news);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public bool NewsExists(int newsId)
        {
            return _db.News.Any(n => n.NewsID == newsId);
        }

        public IEnumerable<News> GetTopNews(int take=4)
        {
            return _db.News.OrderByDescending(n => n.Visit).Take(take).ToList();
        }

        public IEnumerable<News> GetNewsInSlider()
        {
            return _db.News.Where(n => n.ShowInSlider == true).ToList();
        }

        public IEnumerable<News> GetLatesNews()
        {
            return _db.News.OrderByDescending(n => n.CreateDate).Take(4).ToList();
        }

        public IEnumerable<News> GetNewsByGroupId(int groupId)
        {
            return _db.News.Where(n=>n.GroupID==groupId).ToList();
        }

        public IEnumerable<News> Search(string q)
        {
            var list = _db.News.Where(n => n.NewsTitle.Contains(q) ||
              n.ShortDescription.Contains(q) || n.MainText.Contains(q)
              || n.Tags.Contains(q)).ToList();
            return list.Distinct().ToList();
        }
    }
}
