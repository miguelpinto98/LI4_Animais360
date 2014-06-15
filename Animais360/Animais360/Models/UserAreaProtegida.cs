using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Animais360.Models
{
    [Table("UserAreaProtegida")]
    public class UserAreaProtegida
    {
        public int UserAreaProtegidaId { get; set; }
        public int NrVezes { get; set; }
        public int RespCertas { get; set; }
        public int RespErradas { get; set; }
        public virtual User User { get; set; }
        public virtual AreaProtegida AreaProtegida { get; set; }
    }

}