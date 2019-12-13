using System.IO;
using Microsoft.AspNetCore.Mvc;
using RoletopMVC.Models;

namespace RoletopMVC.Repositories
{
    public class ClienteRepository : RepositoryBase
    {
        private const string PATH = "Database/Cliente.CSV";

        public ClienteRepository()
        {
            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        public bool Inserir(Cliente cliente)
        {
            var linha = new string[] {PrepararRegistroCSV(cliente)};
            File.AppendAllLines(PATH, linha);

            return true;
        }

        public Cliente ObterPor (string email)
        {
            var linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                if (ExtrairValorDoCampo("Email", item).Equals(email))

                {
                Cliente c = new Cliente();   
                c.TipoUsuario = uint.Parse(ExtrairValorDoCampo("tipo_usuario", item));
                c.Nome = ExtrairValorDoCampo("Nome", item);
                c.Idade = ExtrairValorDoCampo("Idade", item);
                c.CpfCnpj = ExtrairValorDoCampo("CpfCnpj", item);
                c.Email = ExtrairValorDoCampo("Email", item);
                c.Telefone = ExtrairValorDoCampo("Telefone", item);

                c.Senha = ExtrairValorDoCampo("Senha", item);
                
                return c;
                }
            }
            return null;
        }

        private string PrepararRegistroCSV(Cliente cliente)
        {
            return $"tipo_usuario={cliente.TipoUsuario};Nome={cliente.Nome};Idade={cliente.Idade};CpfCnpj={cliente.CpfCnpj};Email={cliente.Email};Telefone={cliente.Telefone};Senha={cliente.Senha}";
        }
    }
}