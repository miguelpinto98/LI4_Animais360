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

        [Required]
        [Display(Name = "Nome Área Protegida *")]
        public string AreaNome { get; set; }

        [Required]
        [Display(Name = "Longitude *")]
        public string Longitude { get; set; }

        [Required]
        [Display(Name = "Latitude *")]
        public string Latitude { get; set; }

        [Required]
        [Display(Name = "Descrição *")]
        public string Descricao { get; set; }
        public int Permitida { get; set; }

        public Pais Pais { get; set; }
        public virtual ICollection<Questao> Questoes { get; set; }

        [NotMapped]
        [Display(Name = "País Pertencente *")]
        public int IdPais { get; set; }
        
        [NotMapped]
        [Display(Name = "Se País Não Existir **")]
        public int IdContinente { get; set; }

        [NotMapped]
        public string NomePais { get; set; }
    }
}