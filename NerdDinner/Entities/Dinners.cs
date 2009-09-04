using System;
using System.Collections.Generic;
using System.Linq;
using NerdDinner.Helpers;
using NerdDinner.Models;

namespace NerdDinner.Entities
{
    public class Dinners
    {
        public virtual int DinnerID { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime EventDate { get; set; }
        public virtual string Description { get; set; }
        public virtual string HostedBy { get; set; }
        public virtual string ContactPhone { get; set; }
        public virtual string Address { get; set; }
        public virtual string Country { get; set; }
        public virtual float Latitude { get; set; }
        public virtual float Longitude { get; set; }

        public virtual IList<NerdDinner.Entities.RSVP> RSVPs { get; set; }

        public virtual bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        
        public virtual bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public virtual bool IsHostedBy(string userName)
        {
            return HostedBy.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual IEnumerable<RuleViolation> GetRuleViolations()
        {

            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("Title is required", "Title");

            if (String.IsNullOrEmpty(Description))
                yield return new RuleViolation("Description is required", "Description");

            if (String.IsNullOrEmpty(HostedBy))
                yield return new RuleViolation("HostedBy is required", "HostedBy");

            if (String.IsNullOrEmpty(Address))
                yield return new RuleViolation("Address is required", "Address");

            if (String.IsNullOrEmpty(Country))
                yield return new RuleViolation("Country is required", "Address");

            if (String.IsNullOrEmpty(ContactPhone))
                yield return new RuleViolation("Phone# is required", "ContactPhone");

            if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
                yield return new RuleViolation("Phone# does not match country", "ContactPhone");

            yield break;
        }


    }
}
