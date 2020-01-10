using AllTech.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllTech.Web.ViewComponents
{
    public class ShowTopNewsComponent:ViewComponent
    {
        private readonly INewsRepository _newsRepository;
        public ShowTopNewsComponent(INewsRepository newsRepository )
        {
            _newsRepository = newsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowTopNewsComponent", 
                _newsRepository.GetTopNews()));
        }
    }
}
