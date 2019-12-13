using System.Collections.Generic;
using RoletopMVC.Models;

namespace RoletopMVC.ViewModel
{
    public class AgendamentoViewModel : BaseViewModel
    {
        public List<string> FormasDePagamento = new List<string>();
        public List<Agendamentos> agendamentos {get; set;}

        public Dictionary<string, double> Servicos = new Dictionary<string, double>();

        public string NomeUsuario {get;set;}
        public Cliente Cliente {get; set;}

        public AgendamentoViewModel()
        {
            this.Cliente = new Cliente();
        }

        
    }
}