using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NerdDinner.Entities;
using NerdDinner.Helpers;
using NerdDinner.Models;
using NHibernate;


namespace NerdDinner.Controllers
{
    //
    // ViewModel Classes

    public class DinnerFormViewModel
    {
        // Properties
        public NerdDinner.Entities.Dinners Dinner { get; private set; }
        public SelectList Countries { get; private set; }

        // Constructor
        public DinnerFormViewModel(NerdDinner.Entities.Dinners dinner)
        {
            Dinner = dinner;
            Countries = new SelectList(PhoneValidator.Countries, Dinner.Country);
        }
    }

    //
    // Controller Class

    [HandleError]
    public class DinnersController : Controller
    {
        private IDinnerRepository dinnerRepository;

       
        //
        // Dependency Injection enabled constructors

       public DinnersController(IDinnerRepository repository)
        {
            dinnerRepository = repository;
        }

        //
        // GET: /Dinners/
        //      /Dinners/Page/2

        public ActionResult Index(int? page)
        {
            const int pageSize = 10;

            var upcomingDinners = dinnerRepository.FindUpcomingDinners();
            var paginatedDinners = new PaginatedList<NerdDinner.Entities.Dinners>(upcomingDinners, page ?? 0, pageSize);

            return View(paginatedDinners);
        }

        //
        // GET: /Dinners/Details/5

      
        public ActionResult Details(int id)
        {
            NerdDinner.Entities.Dinners dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            return View(dinner);
        }

        //
        // GET: /Dinners/Edit/5

        [Authorize]
        public ActionResult Edit(int id)
        {
            NerdDinner.Entities.Dinners dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            return View(new DinnerFormViewModel(dinner)); 
        }

        //
        // POST: /Dinners/Edit/5

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            NerdDinner.Entities.Dinners dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            try
            {
                UpdateModel(dinner);

                dinnerRepository.Save(dinner);

                return RedirectToAction("Details", new {id = dinner.DinnerID});
            }
            catch
            {
                ModelState.AddModelErrors(dinner.GetRuleViolations());

                return View(new DinnerFormViewModel(dinner)); 
            }
        }

        //
        // GET: /Dinners/Create

        [Authorize]
        public ActionResult Create()
        {
            NerdDinner.Entities.Dinners dinner = new Dinners()
                                                     {
                                                         EventDate = DateTime.Now.AddDays(7)
                                                     };

            return View(new DinnerFormViewModel(dinner));
        }

        //
        // POST: /Dinners/Create

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Create(NerdDinner.Entities.Dinners dinner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dinner.HostedBy = User.Identity.Name;
                    IList<NerdDinner.Entities.RSVP> rsvps = new List<NerdDinner.Entities.RSVP>();
                    rsvps.Add(new NerdDinner.Entities.RSVP() {AttendeeName = User.Identity.Name});
                    rsvps[0].dinner = dinner;
                    dinner.RSVPs = rsvps;
                    dinnerRepository.Add(dinner);
                    dinnerRepository.Save(dinner);

                    return RedirectToAction("Details", new {id = dinner.DinnerID});
                }
                catch
                {
                    ModelState.AddModelErrors(dinner.GetRuleViolations());
                }
            }

            return View(new DinnerFormViewModel(dinner));
        }

        //
        // HTTP GET: /Dinners/Delete/1

        [Authorize]
        public ActionResult Delete(int id)
        {
            NerdDinner.Entities.Dinners dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            return View(dinner);
        }

        // 
        // HTTP POST: /Dinners/Delete/1

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Delete(int id, string confirmButton)
        {
            NerdDinner.Entities.Dinners dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            dinnerRepository.Delete(dinner);
            

            return View("Deleted");
        }
    }
}