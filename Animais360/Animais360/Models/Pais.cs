using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Animais360.Models
{
    [Table("Paises")]
    public class Pais {
        public int PaisID {get; set;}
        public string PaisNome { get; set; }
        public string Descricao { get; set; }
    }
}
