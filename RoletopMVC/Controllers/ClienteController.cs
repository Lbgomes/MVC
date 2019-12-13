using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoletopMVC.Enums;
using RoletopMVC.Repositories;
using RoletopMVC.ViewModel;

namespace RoletopMVC.Controllers {
    public class ClienteController : AbstractController {
        private ClienteRepository clienteRepository = new ClienteRepository ();
        private AgendamentoRepository agendamentoRepository = new AgendamentoRepository ();
        [HttpGet]
        public IActionResult Login () {
            return View (new BaseViewModel () {
                NomeView = "Login",
                    UsuarioEmail = ObterUsuarioSession (),
                    UsuarioNome = ObterUsuarioNomeSession ()
            });

        }

        [HttpPost]
        public IActionResult Login (IFormCollection form) {
            ViewData["Action"] = "login"; //Aqui temos um problema, pois este login esta direcionado para carregar minha tela de login, ou seja, quando o usuario apertar na opção de login ele irá voltar para a mesma. como carregar a pagina certa ???
            //O problema acima foi resolvido criando um historico... para acesssar o historico ir até Views-Cliente.
            try {
                System.Console.WriteLine ("========================");
                System.Console.WriteLine (form["Email"]);
                System.Console.WriteLine (form["Senha"]);
                System.Console.WriteLine ("========================");

                var usuario = form["Email"];
                var senha = form["Senha"];

                var cliente = clienteRepository.ObterPor (usuario);

                if (cliente != null) {
                    if (cliente.Senha.Equals (senha)) {
                        switch (cliente.TipoUsuario) {
                            case (uint) TiposUsuario.CLIENTE:
                                HttpContext.Session.SetString (SESSION_CLIENTE_EMAIL, usuario);
                                HttpContext.Session.SetString (SESSION_CLIENTE_NOME, cliente.Nome);
                                HttpContext.Session.SetString (SESSION_CLIENTE_TIPO, cliente.TipoUsuario.ToString ());
                                return RedirectToAction ("Historico", "Cliente"); //Aqui estamos usando redirecttoaction pois, ele irá "matar o viewdata"

                            default:
                                HttpContext.Session.SetString (SESSION_CLIENTE_EMAIL, usuario);
                                HttpContext.Session.SetString (SESSION_CLIENTE_NOME, cliente.Nome);
                                HttpContext.Session.SetString (SESSION_CLIENTE_TIPO, cliente.TipoUsuario.ToString ());
                                return RedirectToAction ("Dashboard", "Administrador"); //Aqui estamos usando redirecttoaction pois, ele irá "matar o viewdata"
                        }
                    } else {
                        return View ("Erro", new RespostaViewModel ("Senha incorreta"));
                    }
                } else {
                    return View ("Erro", new RespostaViewModel ($"Usuário {usuario} não encontrado")); //Ao inves de chamar esta mensagem salvando ela em uma variavel, neste caso como é uma pequena mensagem, basta instanciar o objeto dentro do parametro
                }

            } catch (Exception e) {
                System.Console.WriteLine (e.StackTrace);
                return View ("Erro", new BaseViewModel () {
                    NomeView = "Home",
                        UsuarioEmail = ObterUsuarioSession (),
                        UsuarioNome = ObterUsuarioNomeSession ()
                });
            }
        }
        public IActionResult Historico () {
            var emailCliente = HttpContext.Session.GetString (SESSION_CLIENTE_EMAIL);
            var agendamentosCliente = agendamentoRepository.ObterTodosPorCliente (emailCliente);

            return View (new HistoricoViewModel () {
                agendamentos = agendamentosCliente,
                    NomeView = "Historico",
                    UsuarioEmail = ObterUsuarioSession (),
                    UsuarioNome = ObterUsuarioNomeSession ()
            });
        }
        public IActionResult Logoff () {
            HttpContext.Session.Remove (SESSION_CLIENTE_EMAIL);
            HttpContext.Session.Remove (SESSION_CLIENTE_NOME);
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index", "Home");

        }
    }
}