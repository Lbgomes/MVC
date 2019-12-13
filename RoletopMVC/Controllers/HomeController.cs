using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Repositories;
using RoletopMVC.Controllers;
using RoletopMVC.Enums;
using RoletopMVC.Models;
using RoletopMVC.Repositories;
using RoletopMVC.ViewModel;

namespace RoleTopMVC.Controllers {
    public class HomeController : AbstractController {
        ClienteRepository clienteRepository = new ClienteRepository ();
        AgendamentoRepository agendamentoRepository1 = new AgendamentoRepository ();
        ServicosRepository servicosRepository = new ServicosRepository ();
        PagamentoRepository pagamentoRepository = new PagamentoRepository ();

        public IActionResult Index () {
            AgendamentoViewModel avm = new AgendamentoViewModel ();
            avm.FormasDePagamento = pagamentoRepository.ObterTodos ();
            avm.Servicos = servicosRepository.ObterTodos ();

            var usuarioLogado = ObterUsuarioNomeSession ();
            var nomeUsuarioLogado = ObterUsuarioSession ();

            if (!string.IsNullOrEmpty (nomeUsuarioLogado)) {
                avm.NomeUsuario = nomeUsuarioLogado;
            }
            // var clienteLogado = clienteRepository.ObterPor(usuarioLogado);
            // avm.Cliente = clienteLogado;

            avm.NomeView = "Pedido";
            avm.UsuarioEmail = usuarioLogado;
            avm.UsuarioNome = nomeUsuarioLogado;

            return View (avm);
        }

        public IActionResult Registrar (IFormCollection form) {
            Agendamentos agendamento = new Agendamentos ();

            Cliente cliente = new Cliente ();
            cliente.Nome = form["Nome"];
            cliente.Email = form["Email"];
            cliente.CpfCnpj = form["CpfCnpj"];
            cliente.Telefone = form["Telefone"];
            agendamento.NomeEvento = form["NomeEvento"];
            agendamento.Informacoes = form["informacoes"];
            agendamento.ServicosAdicionais = form["ServicosAdicionais"];
            agendamento.FormasPagamento = form["FormaPagamento"];
            agendamento.PubPriv = form["pubpriv"];

            agendamento.cliente = cliente;

            agendamento.DataDoEvento = DateTime.Parse(form["calendario"]);

            double precoDefinitivo = servicosRepository.ObterPrecoTotal (agendamento.ServicosAdicionais);
            agendamento.PrecoTotal = precoDefinitivo;

            if (agendamentoRepository1.Inserir (agendamento)) {
                return View ("Sucesso", new RespostaViewModel () {
                    NomeView = "Pedido",
                        UsuarioEmail = ObterUsuarioSession (),
                        UsuarioNome = ObterUsuarioNomeSession ()
                });
            } else {
                return View ("Erro", new RespostaViewModel () {
                NomeView = "Pedido",
                        UsuarioEmail = ObterUsuarioSession (),
                        UsuarioNome = ObterUsuarioNomeSession ()
            });
            }
        }
        public IActionResult Aprovar (ulong id) {
            var pedido = agendamentoRepository1.ObterPor (id);
            pedido.Status = (uint) StatusPedido.APROVADO;

            if (agendamentoRepository1.Atualizar (pedido)) {
                return RedirectToAction ("Dashboard", "Administrador");
            } else {
                return View ("Erro", new RespostaViewModel ("Não foi possível aprovar esse pedido") {
                    NomeView = "Dashboard",
                        UsuarioEmail = ObterUsuarioSession (),
                        UsuarioNome = ObterUsuarioNomeSession ()
                });
            }
        }
        public IActionResult Pendente (ulong id) {
            var pedido = agendamentoRepository1.ObterPor (id);
            pedido.Status = (uint) StatusPedido.PENDENTE;

            if (agendamentoRepository1.Atualizar (pedido)) {
                return RedirectToAction ("Aprovado", "Administrador");
            } else {
                return View ("Erro", new RespostaViewModel ("Não foi possível aprovar esse pedido") {
                    NomeView = "Dashboard",
                        UsuarioEmail = ObterUsuarioSession (),
                        UsuarioNome = ObterUsuarioNomeSession ()
                });
            }
        }
        public IActionResult Pendentes (ulong id) {
            var pedido = agendamentoRepository1.ObterPor (id);
            pedido.Status = (uint) StatusPedido.PENDENTE;

            if (agendamentoRepository1.Atualizar (pedido)) {
                return RedirectToAction ("Reprovado", "Administrador");
            } else {
                return View ("Erro", new RespostaViewModel ("Não foi possível aprovar esse pedido") {
                    NomeView = "Dashboard",
                        UsuarioEmail = ObterUsuarioSession (),
                        UsuarioNome = ObterUsuarioNomeSession ()
                });
            }
        }

        public IActionResult Reprovar (ulong id) {
            var pedido = agendamentoRepository1.ObterPor (id);
            pedido.Status = (uint) StatusPedido.REPROVADO;

            if (agendamentoRepository1.Atualizar (pedido)) {
                return RedirectToAction ("Dashboard", "Administrador");
            } else {
                return View ("Erro", new RespostaViewModel ("Não foi possível reprovar esse pedido") {
                    NomeView = "Dashboard",
                        UsuarioEmail = ObterUsuarioSession (),
                        UsuarioNome = ObterUsuarioNomeSession ()
                        
                });
            }
        }
    }
}