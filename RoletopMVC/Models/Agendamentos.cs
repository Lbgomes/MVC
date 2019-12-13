using System;
using RoletopMVC.Enums;

namespace RoletopMVC.Models {
    public class Agendamentos : Produto {
        public ulong Id { get; set; }
        public Cliente cliente { get; set; }
        public string NomeEvento { get; set; }

        public string Informacoes { get; set; }

        public int CpfCnpj { get; set; }

        public string Email { get; set; }

        public string ServicosAdicionais { get; set; }
        public string FormasPagamento { get; set; }

        public string PubPriv { get; set; }

        public DateTime DataDoEvento { get; set; }

        public double PrecoTotal { get; set; }
        public uint Status { get; set; }

        public Agendamentos () {
            this.cliente = new Cliente ();
            this.Id = 0;
            this.Status = (uint) StatusPedido.PENDENTE; //Pendente

        }

    }
}