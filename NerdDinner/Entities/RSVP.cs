namespace NerdDinner.Entities
{
    public class RSVP
    {
        public virtual int RsvpID { get;  set; }
        public virtual string AttendeeName { get;  set; }
        public virtual Dinners dinner { get;  set; }
    }
}
