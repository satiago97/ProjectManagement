using System;
using System.Collections.Generic;

namespace ProjectManagement.Models
{
    public partial class Utilizador
    {
        public int IdUtilizador { get; set; }
        public string Nome { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? IdHorario { get; set; }

        public virtual Horario? IdHorarioNavigation { get; set; }
    }
}
