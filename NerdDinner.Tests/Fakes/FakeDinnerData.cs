using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NerdDinner.Models;
using System.Web.Mvc;
using RSVP=NerdDinner.Entities.RSVP;

namespace NerdDinner.Tests.Fakes
{
    class FakeDinnerData
    {
        public static List<NerdDinner.Entities.Dinners> CreateTestDinners()
        {

            List<NerdDinner.Entities.Dinners> dinners = new List<NerdDinner.Entities.Dinners>();

            for (int i = 0; i < 101; i++)
            {

                NerdDinner.Entities.Dinners sampleDinner = new NerdDinner.Entities.Dinners()
                {
                    DinnerID = i,
                    Title = "Sample Dinner",
                    HostedBy = "SomeUser",
                    Address = "Some Address",
                    Country = "USA",
                    ContactPhone = "425-555-1212",
                    Description = "Some description",
                    EventDate = DateTime.Now.AddDays(i),
                    Latitude = 99,
                    Longitude = -99
                };


                IList<NerdDinner.Entities.RSVP> rsvps = new List<NerdDinner.Entities.RSVP>();
                rsvps.Add(new NerdDinner.Entities.RSVP() { AttendeeName = "SomeUser" });
                rsvps[0].dinner = sampleDinner;
                sampleDinner.RSVPs = rsvps;

                dinners.Add(sampleDinner);
            }

            return dinners;
        }

        public static NerdDinner.Entities.Dinners CreateDinner()
        {
            NerdDinner.Entities.Dinners dinner = new NerdDinner.Entities.Dinners();
            dinner.Title = "New Test Dinner";
            dinner.EventDate = DateTime.Now.AddDays(7);
            dinner.Address = "5 Main Street";
            dinner.Description = "Desc";
            dinner.ContactPhone = "503-555-1212";
            dinner.HostedBy = "scottgu";
            dinner.Latitude = 45;
            dinner.Longitude = 45;
            dinner.Country = "USA";
            return dinner;
        }

        public static FormCollection CreateDinnerFormCollection()
        {
            var form = new FormCollection();

            form.Add("Description", "Description");
            form.Add("Title", "New Test Dinner");
            form.Add("EventDate", "2010-02-14");
            form.Add("Address", "5 Main Street");
            form.Add("ContactPhone", "503-555-1212");
            return form;
        }

    }
}
