using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [NotMapped]
        public virtual string HipAjuda { get; set; }
    }

    public class CreateQuestao {
        public int id { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        [Display(Name="Pergunta *")]
        public string Pergunta { get; set; }

        [Required]
        [Display(Name = "Dificuldade Quantitativa *")]
        public int DifQuantitativa { get; set; }

        [Required]
        [Display(Name = "Opção de Resposta 1*")]
        public string Resposta1 { get; set; }
        
        [Required]
        [Display(Name = "Opção de Resposta 2*")]
        public string Resposta2 { get; set; }

        [Required]
        [Display(Name = "Opção de Resposta 3*")]
        public string Resposta3 { get; set; }

        [Required]
        [Display(Name = "Opção de Resposta 4*")]
        public string Resposta4 { get; set; }
      
        [Display(Name = "Opção de Resposta 5*")]
        public string Resposta5 { get; set; }

        [Display(Name = "Opção de Resposta 6*")]
        public string Resposta6 { get; set; }

        [Required]
        [Display(Name = "Resposta 1*")]
        public string RespCorreta1 { get; set; }

        [Display(Name = "Resposta 2*")]
        public string RespCorreta2 { get; set; }

        [Required]
        [Display(Name = "Ajuda 1 [Pista]*")]
        public string Ajuda1 { get; set; }

        [Required]
        [Display(Name = "Ajuda 2 [50:50]*")]
        public string Ajuda2 { get; set; }

        [Required]
        [Display(Name = "Ajuda 3 [URL]*")]
        public string Ajuda3 { get; set; }

        [Display(Name = "Media *")]
        public string Imagem { get; set; }
    }

    public class ValidaQuestao {
        public int res { get; set; }
        public int pontos { get; set; }
        public int idArea { get; set; }

        [DefaultValue(0)]
        public int gameOver { get; set; }
    }
}