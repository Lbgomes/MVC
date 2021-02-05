using System.Collections.Generic;
using System.IO;

namespace MVC.Repositories
{
    public class PagamentoRepository
    {
        private const string PATH = "Database/FormasPagamentos.csv";

        public PagamentoRepository()
        {
            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }
        public List<string> ObterTodos()
        {
            var linhas = File.ReadAllLines(PATH);
            List<string> pagamentos = new List<string>();
            foreach (var linha in linhas)
            {
                pagamentos.Add(linha);
            }
            return pagamentos;
        }
    }
}