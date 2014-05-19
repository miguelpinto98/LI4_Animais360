using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Animais360.Models
{
    [Table("Classificacoes")]
    public class Classificacao {
        public int ClassificacaoId { get; set; }
        public int Dificuldade { get; set; }
        public int pontos { get; set; }
    }
}