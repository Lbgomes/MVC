using System.Collections.Generic;
using RoletopMVC.Models;

namespace RoletopMVC.ViewModel
{
    public class HistoricoViewModel : BaseViewModel
    {
        public List<Agendamentos> agendamentos {get;set;}
        public HistoricoViewModel()
        {
            this.agendamentos = new List<Agendamentos>();
        }
    }
}