using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoletopMVC.Enums;
using RoletopMVC.Models;
using RoletopMVC.Repositories;
using RoletopMVC.ViewModel;

namespace RoletopMVC.Controllers
{
    public class CadastroController : AbstractController
    {
        ClienteRepository  clienteRepository = new ClienteRepository();
        
        public IActionResult Cadastro()
        {
            return View(new BaseViewModel()
        {
        NomeView = "Cliente",
        UsuarioEmail = ObterUsuarioSession(),
        UsuarioNome = ObterUsuarioNomeSession()
            });
        }
        public IActionResult CadastrarCliente(IFormCollection form)
        {
            ViewData["Action"] = "Seu cadastro";
            try
            {
                Cliente cliente = new Cliente(form["Nome"], form["NomeEvento"], form["Idade"], form["Cpf"], form["Email"], form["Telefone"], form["Senha"]);
                
                cliente.TipoUsuario = (uint) TiposUsuario.CLIENTE;
                clienteRepository.Inserir(cliente);
                
                return View("Sucesso", new BaseViewModel()
        {
        NomeView = "Cadastro",
        UsuarioEmail = ObterUsuarioSession(),
        UsuarioNome = ObterUsuarioNomeSession()
            });
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
                return View("Erro", new BaseViewModel()
        {
        NomeView = "Home",
        UsuarioEmail = ObterUsuarioSession(),
        UsuarioNome = ObterUsuarioNomeSession()
            });
            }
    }
}
}
