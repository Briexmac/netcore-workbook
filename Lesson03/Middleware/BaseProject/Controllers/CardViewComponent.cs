using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Controllers
{
    public class CardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CardModel cardModel)
        {
            return View<CardModel>(cardModel);
        }
    }
}
