using System;
using System.Collections.Generic;
using System.IO;
using RoletopMVC.Models;

namespace RoletopMVC.Repositories {
    public class AgendamentoRepository : RepositoryBase {
        private const string PATH = "Database/Agendamentos.csv";

        public AgendamentoRepository () {
            if (!File.Exists (PATH)) {
                File.Create (PATH).Close ();
            }
        }

        public bool Inserir (Agendamentos agendamento) {
            var quantidadePedidos = File.ReadAllLines (PATH).Length;
            agendamento.Id = (ulong) ++quantidadePedidos;
            var linha = new string[] { PrepararAgendamentoCSV (agendamento) };
            File.AppendAllLines (PATH, linha);

            return true;
        }

        public List<Agendamentos> ObterTodosPorCliente (string emailCliente) {
            var Agendamentos = ObterTodos ();
            List<Agendamentos> pedidosCliente = new List<Agendamentos> ();
            foreach (var agendamento in Agendamentos) {
                if (agendamento.cliente.Email.Equals (emailCliente)) {
                    pedidosCliente.Add (agendamento);
                }
            }
            return pedidosCliente;
        }
        public List<Agendamentos> ObterTodos () {
            var linhas = File.ReadAllLines (PATH);
            List<Agendamentos> agendamentos = new List<Agendamentos> ();
            foreach (var linha in linhas) {
                Agendamentos agendamento = new Agendamentos ();
                agendamento.Id = ulong.Parse(ExtrairValorDoCampo ("id", linha));
                agendamento.Status = uint.Parse(ExtrairValorDoCampo ("status_pedido", linha));
                agendamento.cliente.Nome = ExtrairValorDoCampo ("cliente_nome", linha);
                agendamento.NomeEvento = ExtrairValorDoCampo ("cliente_nomeevento", linha);
                agendamento.cliente.Email = ExtrairValorDoCampo ("cliente_email", linha);
                agendamento.cliente.Telefone = ExtrairValorDoCampo ("cliente_telefone", linha);
                agendamento.cliente.CpfCnpj = ExtrairValorDoCampo ("cliente_cpfcnpj", linha);
                agendamento.DataDoEvento = DateTime.Parse(ExtrairValorDoCampo ("cliente_dataevento", linha));
                agendamento.ServicosAdicionais = ExtrairValorDoCampo ("cliente_servicosadicionais", linha);   
                agendamento.FormasPagamento = ExtrairValorDoCampo ("cliente_formadepagamento", linha);
                agendamento.PubPriv = ExtrairValorDoCampo ("cliente_pubpriv", linha);
                agendamento.Informacoes = ExtrairValorDoCampo ("cliente_informacoes", linha);
                agendamento.PrecoTotal = double.Parse(ExtrairValorDoCampo("cliente_precototal", linha));
                agendamentos.Add(agendamento);

            }
            return agendamentos;
        }
        public Agendamentos ObterPor (ulong id) {
            var pedidosTotais = ObterTodos ();
            foreach (var pedido in pedidosTotais) {
                if (id.Equals (pedido.Id)) {
                    return pedido;
                }
            }
            return null;

        }

        public bool Atualizar (Agendamentos agendamentos) {
            var AgendamentosTotais = File.ReadAllLines (PATH);
            var pedidoCSV = PrepararAgendamentoCSV (agendamentos);
            var linhaPedido = -1;
            var resultado = false;


            for (int i = 0; i < AgendamentosTotais.Length; i++) {
                var idConvertido = ulong.Parse (ExtrairValorDoCampo ("id", AgendamentosTotais[i]));
                if (agendamentos.Id.Equals(idConvertido))
                {
                    linhaPedido = i;
                    resultado = true;
                    break;
                }
            }

            if(resultado)
            {
                AgendamentosTotais[linhaPedido] = pedidoCSV;
                File.WriteAllLines(PATH, AgendamentosTotais);
            }
            return resultado;
        }

        private string PrepararAgendamentoCSV (Agendamentos agendamentos) {
            Cliente c = agendamentos.cliente;

            
            return $"id={agendamentos.Id};status_pedido={agendamentos.Status};cliente_nome={c.Nome};cliente_nomeevento={agendamentos.NomeEvento};cliente_email={c.Email};cliente_telefone={c.Telefone};cliente_cpfcnpj={c.CpfCnpj};cliente_dataevento={agendamentos.DataDoEvento};cliente_servicosadicionais={agendamentos.ServicosAdicionais};cliente_formadepagamento={agendamentos.FormasPagamento};cliente_pubpriv={agendamentos.PubPriv};cliente_informacoes={agendamentos.Informacoes};cliente_precototal={agendamentos.PrecoTotal}";
        }
    }
}