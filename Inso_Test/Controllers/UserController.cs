using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Inso_Test.Models;

namespace Inso_Test.Controllers
{
    public class UserController : Controller {
        public ActionResult Index()
        {
            return View("List");
        }
  
        public ActionResult Create()
        {
            return View("Create");
        }

        public ActionResult Save(Inso_Test.Models.user user, HttpPostedFile avatar)
        {
            if (avatar != null && avatar.ContentLength > 0) {
                var fileName = Path.GetFileName(avatar.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                avatar.SaveAs(path);
            }

            InsooEntities entities = new InsooEntities();

            entities.users.Add(user);

            entities.SaveChanges();

            return View("View", user);
        }

        public ActionResult List()
        {
            InsooEntities entities = new InsooEntities();
            return View(from user in entities.users.Take(1000)
                        select user);
        }
    }
}