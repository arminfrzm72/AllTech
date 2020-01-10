using AllTech.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllTech.Web.ViewComponents
{
    public class ShowGroupsComponent:ViewComponent
    {
        private readonly INewsGroupRepository _newsGroupRepository;
        public ShowGroupsComponent(INewsGroupRepository newsGroupRepository )
        {
            _newsGroupRepository = newsGroupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowGroupsComponent",
                _newsGroupRepository.GetListGroups()));
        }
    }
}
