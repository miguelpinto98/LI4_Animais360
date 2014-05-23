using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Animais360.Models
{
    public class Animais360Context : DbContext {
        public Animais360Context()
            : base("Animais360Context")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Continente> Continentes { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Classificacao> Classificacoes { get; set; }

        public DbSet<Ajuda> Ajudas { get; set; }

        public DbSet<AreaProtegida> AreaProtegidas { get; set; }

        public DbSet<Questao> Questoes { get; set; }
    }
}