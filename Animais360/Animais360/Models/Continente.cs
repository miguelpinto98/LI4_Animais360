using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Animais360.Models
{
    [Table("Continentes")]
    public class Continente
    {
        public int ContinenteId { get; set; }
        public string ContinenteName { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Pais> Paises { get; set; }
    }
}