// using System;
// using System.Collections.Generic;
// using System.IO;
// using RoletopMVC.Models;
// using RoletopMVC.Repositories;

// namespace McBonaldsMVC.Repositories
// {
//     public class PedidoRepository : RepositoryBase  
//     {
//         private const string PATH = "Database/Pedido.csv";

//         public PedidoRepository()
//         {
//             if(!File.Exists(PATH))
//             {
//                 File.Create(PATH).Close();
//             }
//         }

//         public bool Inserir(Eventos eventos)
//         {
//             var quantidadePedidos = File.ReadAllLines(PATH).Length;
//             eventos.Id = (ulong) ++quantidadePedidos;
//             var linha = new string[] {PrepararEventosCSV(eventos) };
//             File.AppendAllLines(PATH, linha);

//             return true;
//         }

//         public List<Eventos> ObterTodosPorCliente(string emailCliente)
//         {
//             var pedidos = ObterTodos();
//             List<Eventos> AgendamentosCliente = new List<Eventos>();
//             foreach (var pedido in pedidos)
//             {
//                 if(pedido.cliente.Email.Equals(emailCliente))
//                 {
//                     AgendamentosCliente.Add(pedido);
//                 }
//             }
//             return AgendamentosCliente;
//         }

//         public List<Eventos> ObterTodos()
//         {
//             var linhas = File.ReadAllLines(PATH);
//             List<Eventos> pedidos = new List<Eventos> ();
//             foreach (var linha in linhas)
//             {
//                 Eventos eventos = new Eventos();
//                 eventos.Id = ulong.Parse(ExtrairValorDoCampo("id", linha));
//                 eventos.Status = uint.Parse(ExtrairValorDoCampo("status_pedido", linha));
//                 eventos.cliente.Nome = ExtrairValorDoCampo("cliente_nome", linha);
//                 eventos.cliente.Endereco = ExtrairValorDoCampo("cliente_endereco", linha);
//                 eventos.cliente.Telefone = ExtrairValorDoCampo("cliente_telefone", linha);
//                 eventos.cliente.Email = ExtrairValorDoCampo("cliente_email", linha);
//                 eventos.Hamburguer.Nome = ExtrairValorDoCampo("hamburguer_nome", linha);
//                 eventos.Hamburguer.Preco = double.Parse(ExtrairValorDoCampo("hamburguer_preco", linha));
//                 eventos.Shake.Nome = ExtrairValorDoCampo("shake_nome", linha);
//                 eventos.Shake.Preco = double.Parse(ExtrairValorDoCampo("shake_preco", linha));
//                 eventos.DataDoPedido = DateTime.Parse(ExtrairValorDoCampo("data_pedido", linha));
//                 eventos.PrecoTotal = double.Parse(ExtrairValorDoCampo("preco_total", linha));

//                 pedidos.Add(eventos);
//             }
//             return pedidos;
//         }

        

//         private string PrepararEventosCSV(Eventos pedido)
//         {
//             Cliente c = pedido.cliente;
//         }
//     }
// }