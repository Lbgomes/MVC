using System.Collections.Generic;
using RoletopMVC.Models;

namespace RoletopMVC.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        public List<Agendamentos> agendamentos {get; set;}
        public uint AgendamentosAprovados {get; set;}
        
        public uint AgendamentosReprovados {get; set;}
        public uint AgendamentosPendentes {get; set;}
    
    public DashboardViewModel()
    {
        this.agendamentos = new List<Agendamentos>();
    }
    }
}