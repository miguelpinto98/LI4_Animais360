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

    }
}