using System;
using System.Collections.Generic;

namespace ProjectManagement.Models
{
    public partial class Projeto
    {
        public string Nome { get; set; }
        public int IdProjeto { get; set; }
        public string NomeCliente { get; set; } = null!;
        public string Preco { get; set; } = null!;
    }
}
