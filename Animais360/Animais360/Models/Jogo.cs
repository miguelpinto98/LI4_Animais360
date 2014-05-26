using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Animais360.Models
{
    [Table("Jogos")]
    public class Jogo {
        public int JogoId { get; set; }
        public int RespCertas { get; set; }
        public int RespErradas { get; set; }
        public string Personagem { get; set; }
        public int Nivel { get; set; }
        public int Pontos { get; set; }
        public int DifQualitativa { get; set; }
        public int Estado { get; set; }
        public int Sucesso {get; set;}
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public User User { get; set; }


        [NotMapped]
        public virtual int ContinenteID { get; set; }
        
        [NotMapped]
        public virtual int DificuldadeID { get; set; }

        //Secalhar vai ser preciso adicionar um atributo, se perder, dizer em que área foi
    }
}