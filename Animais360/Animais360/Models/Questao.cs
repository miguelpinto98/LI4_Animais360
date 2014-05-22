using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Animais360.Models
{
    [Table("Questoes")]
    public class Questao
    {
        public int QuestaoID { get; set; }
        public int DifQuantitativa { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public string Hipoteses { get; set; }
        public int Tipo { get; set; }
        public string Imagem { get; set; }
        public virtual AreaProtegida AreaProtegida { get; set; }
        public virtual ICollection<Ajuda> Ajudas { get; set; }
    }

    public class CreateQuestao {
        [Required]
        [Display(Name="Pergunta *")]
        public string Pergunta { get; set; }

        [Required]
        [Display(Name = "Dificuldade Quantitativa *")]
        public int DifQuantitativa { get; set; }
    }
}