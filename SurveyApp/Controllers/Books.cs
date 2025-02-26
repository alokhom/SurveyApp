using Microsoft.AspNetCore.Mvc;
using SurveyApp.DAO;
using System.Collections;

namespace SurveyApp.Controllers
{
    public class Books : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task Index()
        {
            IEnumerable shipcrew = await BooksDAO.getCrew();
            return View(“Index”, shipcrew.ToList());
        }
    }
}

private static BooksDAO dao;

protected BooksDAO bDao
{
    if(dao == null)
    {
        dao = new BooksDAO();
    }
    return dao;
}
