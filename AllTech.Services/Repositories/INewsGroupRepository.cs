using AllTech.DomainClasses.NewsGroup;
using AllTech.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllTech.Services.Repositories
{
    public interface INewsGroupRepository
    {
        List<NewsGroup> GetAllNewsGroup();
        NewsGroup GetNewsGroupById(int groupId);
        void InserNewsGroup(NewsGroup newsGroup);
        void UpdateNewGroup(NewsGroup newsGroup);
        void DeleteNewsGroup(NewsGroup newsGroup);
        void DeleteNewsGroup(int groupId);
        bool NewsGroupExists(int pageGroupId);
        List<ShowGroupsViewModel> GetListGroups();
        void Save();
        
    }
}
