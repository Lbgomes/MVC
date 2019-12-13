using System;

namespace RoletopMVC.Models
{
    public class Cliente {

        public string Nome {get; set;}
        public string Idade {get; set;}
        public string CpfCnpj {get; set;}
        public string Email {get; set;}
        public string Telefone {get; set;}

        public string Senha {get; set;}
        public uint TipoUsuario {get; set;}

    
    public Cliente()
        {

        }

        public Cliente(string Nome, string Nomeevento, string Idade, string Cpfcnpj, string Email, string Telefone, string Senha)
        {
            this.Nome = Nome;
            this.Idade = Idade;
            this.CpfCnpj = Cpfcnpj;
            this.Email = Email;
            this.Telefone = Telefone;
            this.Senha = Senha;
        }
    }
}