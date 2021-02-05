using Microsoft.AspNetCore.Mvc;
using RoletopMVC.ViewModel;

namespace RoletopMVC.Controllers
{
    public class EventosController : AbstractController
    {
            public IActionResult Eventos()
        {
            return View(new BaseViewModel()
        {
        NomeView = "Eventos",
        UsuarioEmail = ObterUsuarioSession(),
        UsuarioNome = ObterUsuarioNomeSession()
            });
        }
    }
}