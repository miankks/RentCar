using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiraffRentBoat.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Fyll i personnummer")]
        [Display(Name = "Personnummer")]
        public int Personnummer { get; set; }

        [Required(ErrorMessage = "Fyll i startdatum")]
        [Display(Name = "Starttid")]
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage ="Fyll i sludatum")]
        [Display(Name = "Sluttid")]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Aktiv")]
        public bool Active { get; set; }

        public int BoatId { get; set; }


        //Navigation properties
        public virtual Boat Boat { get; set; }
        
    }
}