using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiraffRentBoat.Models
{
    public class Boat
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fyll i båtnamn")]
        [Display(Name = "Båtnamn")]
        public string BoatName { get; set; }

        [Required]
        [Display(Name ="Bokningsnummer")]
        public string BookingNumber { get; set; }

        [Required]
        [Display(Name ="Båt Aktiv")]
        public bool BoatActive { get; set; }

        [Required(ErrorMessage = "Fyll i typ")]
        [Display(Name = "Typ")]
        public string BoatType { get; set; }


        //Navigation properties
        public virtual ICollection<Booking> bookings { get; set; }


    }
}