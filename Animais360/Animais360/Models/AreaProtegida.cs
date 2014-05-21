using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Animais360.Models
{
    [Table("AreaProtegidas")]
    public class AreaProtegida
    {
        public int AreaProtegidaID { get; set; }

        [Display(Name = "Nome Área Protegida *")]
        public string AreaNome { get; set; }
        
        [Display(Name = "Longitude *")]
        public string Longitude { get; set; }
        
        [Display(Name = "Latitude *")]
        public string Latitude { get; set; }

        [Display(Name = "Descrição *")]
        public string Descricao { get; set; }
        public int Permitida { get; set; }
        public Pais Pais { get; set; }
        public virtual ICollection<Questao> Questoes { get; set; }
    }
}