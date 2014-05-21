using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Animais360.Models
{
    [Table("AreaProtegidas")]
    public class AreaProtegida
    {
        public int AreaProtegidaID { get; set; }
        public string AreaNome { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Descricao { get; set; }
        public int Permitida { get; set; }
        public Pais Pais { get; set; }
        public virtual ICollection<Questao> Questoes { get; set; }
    }
}