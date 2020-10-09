using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Messenger_Kings.Models
{
    public class Points
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="Points ID")]
        public string Points_ID { get; set; }

        [Display(Name = "Client Points")]
        public int ClientPoints { get; set; }

        public string Client_ID { get; set; }
        public virtual Client Client { get; set; }
    }
}