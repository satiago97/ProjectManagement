using System;
using System.Collections.Generic;

namespace ProjectManagement.Models
{
    public partial class Horario
    {
        public Horario()
        {
            Utilizadors = new HashSet<Utilizador>();
        }

        public int IdHorario { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public virtual ICollection<Utilizador> Utilizadors { get; set; }
    }
}
