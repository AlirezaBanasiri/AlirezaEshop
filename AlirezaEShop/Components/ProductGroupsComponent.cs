using AlirezaEShop.Data;
using AlirezaEShop.Data.Repositories;
using AlirezaEShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlirezaEShop.Components
{
    public class ProductGroupsComponent:ViewComponent
    {
        private IGroupRepositories _groupRepository;
        public ProductGroupsComponent(IGroupRepositories groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Components/ProductGroupsComponent.cshtml", _groupRepository.GetGroupForShow());
        }
    }
}
