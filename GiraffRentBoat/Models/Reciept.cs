using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiraffRentBoat.Models
{
    public class Reciept
    {
        public int RecieptId { get; set; }

        [Display(Name ="Båt namn")]
        public string BoatNameReciept { get; set; }

        [Display(Name ="Bokning nummer")]
        public string BookingNumber { get; set; }

        [Display(Name ="Startdatum")]
        public DateTime StartDate { get; set; }

        [Display(Name ="Lämnar in tid")]
        public DateTime RecieptDate { get; set; }

        [Display(Name ="Total kostnad")]
        public double TotalCost { get; set; }

        public ICollection<Boat> BoatReiepts { get; set; }
        public ICollection<Booking> BookingReciepts { get; set; }
    }
}