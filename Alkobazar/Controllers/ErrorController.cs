using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkobazar.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult Unauthorized()
        {
            //Response.StatusCode = 401; 
            // Do not set this or else you get a redirect loop
            return View();
            //where View is the friendly .cshtml page
        }
    }
}