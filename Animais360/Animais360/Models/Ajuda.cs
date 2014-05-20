using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Animais360.Models
{
    [Table("Ajuda")]
    public class Ajuda
    {
        public int AjudaID { get; set; }
        public int Grau { get; set; }
        public string Pista { get; set; }
    }
}
