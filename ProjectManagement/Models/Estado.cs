using System;
using System.Collections.Generic;

namespace ProjectManagement.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Tarefas = new HashSet<Tarefa>();
        }

        public int IdEstado { get; set; }
        public string Estado1 { get; set; } = null!;

        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
