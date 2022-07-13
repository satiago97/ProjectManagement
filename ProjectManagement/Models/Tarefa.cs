using System;
using System.Collections.Generic;

namespace ProjectManagement.Models
{
    public partial class Tarefa
    {
        public int IdTarefa { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime DataHoraIni { get; set; }
        public DateTime DataHoraFim { get; set; }
        public double? PrecoHora { get; set; }
        public int? IdEstado { get; set; }

        public virtual Estado? IdEstadoNavigation { get; set; }
    }
}
