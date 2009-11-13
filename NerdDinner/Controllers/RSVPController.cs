using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;
using NHibernate;
using RSVP=NerdDinner.Entities.RSVP;

namespace NerdDinner.Controllers
{
    public class RSVPController : Controller {

        IDinnerRepository dinnerRepository;

       



        //
        // Dependency Injection enabled constructors

        public RSVPController()
            : this(new DinnerRepository(((ISessionFactory)System.Web.HttpContext.Current.Application["SessionFactory"]).OpenSession()))
        {

            
        
        }

        public RSVPController(IDinnerRepository repository) {
            dinnerRepository = repository;
        }

        //
        // AJAX: /Dinners/Register/1

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(int id) {

            NerdDinner.Entities.Dinners dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsUserRegistered(User.Identity.Name)) {

                NerdDinner.Entities.RSVP rsvp = new NerdDinner.Entities.RSVP();
                rsvp.dinner = dinner;
                rsvp.AttendeeName = User.Identity.Name;
                dinner.RSVPs.Add(rsvp);
                dinnerRepository.Save(dinner);
            }

            return Content("Thanks - we'll see you there!");
        }
    }
}
