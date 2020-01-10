using AllTech.DataLayer.Context;
using AllTech.DomainClasses.NewsGroup;
using AllTech.Services.Repositories;
using AllTech.ViewModels.News;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.Services.Services
{
    public class NewsGroupRepository : INewsGroupRepository
    {
        public readonly AllTechDbContext _db;
        public NewsGroupRepository(AllTechDbContext db)
        {
            _db = db;
        }
        

        public List<NewsGroup> GetAllNewsGroup()
        {
            return _db.NewsGroups.ToList();
        }

        public NewsGroup GetNewsGroupById(int groupId)
        {
            return _db.NewsGroups.Find(groupId);
        }

        public void InserNewsGroup(NewsGroup newsGroup)
        {
            _db.NewsGroups.Add(newsGroup);
        }
        public void UpdateNewGroup(NewsGroup newsGroup)
        {
            _db.Entry(newsGroup).State = EntityState.Modified;
        }
        public void DeleteNewsGroup(NewsGroup newsGroup)
        {
            _db.Entry(newsGroup).State = EntityState.Deleted;
        }

        public void DeleteNewsGroup(int groupId)
        {
            var group= GetNewsGroupById(groupId);
            DeleteNewsGroup(group);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public bool NewsGroupExists(int newsGroupId)
        {
            return _db.NewsGroups.Any(n => n.GroupID == newsGroupId);
        }

        public List<ShowGroupsViewModel> GetListGroups()
        {
            return _db.NewsGroups.Select(n => new ShowGroupsViewModel()
            {
                GroupID = n.GroupID,
                GroupTitle = n.GroupTitle,
                NewsCount = n.News.Count
            }).ToList();
        }
    }
}
