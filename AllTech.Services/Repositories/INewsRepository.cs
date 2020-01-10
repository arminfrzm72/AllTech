using AllTech.DomainClasses.News;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllTech.Services.Repositories
{
    public interface INewsRepository
    {
        IEnumerable<News> GetAllNews();
        IEnumerable<News> GetTopNews(int take=4);
        IEnumerable<News> GetNewsInSlider();
        IEnumerable<News> GetLatesNews();
        IEnumerable<News> GetNewsByGroupId(int groupId);
        IEnumerable<News> Search(string q);
        News GetNewsById(int newsId);
        void InsertNews(News news);
        void UpdateNews(News news);
        void DeleteNews(News news);
        void DeleteNews(int newsId);
        bool NewsExists(int newsId);
        void Save();

    }
}
