using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;

namespace SensenHosp.ViewComponents
{
    //THIS IS VIEWCOMPONENT ATTRIVUTE
    [ViewComponent(Name = "AlertPostsViewComponent")]
    public class AlertPostsViewComponent : ViewComponent
    {
        //ACCESS THE DATABASE FOR MY ALERTPOST DATABASE TABLE TO FETCH INFORMATION
        private readonly ApplicationDbContext _context;

        public AlertPostsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        //Define an InvokeAsync method that returns a 
        //Task<IViewComponentResult> or a synchronous Invoke 
        //method that returns an IViewComponentResult.

        //Typically initializes a model and passes it to a 
        //view by calling the ViewComponent View method.
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //THIS WILL RETURN A LIST OF ACTIVE ALERTPOST TO BE DISPLAYED BELOQW NAVIGATION MENU AND WITH BOOTSTRAP CAROUSEL
            return View(await _context.AlertPosts.Where(m => m.AlertStatus == "Active").ToListAsync());
        }
    }
}
