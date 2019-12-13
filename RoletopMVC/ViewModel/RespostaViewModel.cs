namespace RoletopMVC.ViewModel
{
    public class RespostaViewModel : BaseViewModel
    {
        public string Mensagem {get;set;}

        public RespostaViewModel()
        {

        }

//Estamos criando essa area e colocando ela no shared de erro para mandar uma mensagem mais especifica do usuario 
        public RespostaViewModel(string Mensagem)
        {
            this.Mensagem = Mensagem;
        }
    }
}