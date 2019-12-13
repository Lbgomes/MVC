using Microsoft.AspNetCore.Mvc;
using RoletopMVC.ViewModel;

namespace RoletopMVC.Controllers
{
    public class GaleriaController : AbstractController
    {
        public IActionResult galeria()
        {
            return View(new BaseViewModel()
        {
        NomeView = "Galeria",
        UsuarioEmail = ObterUsuarioSession(),
        UsuarioNome = ObterUsuarioNomeSession()
            });
        }
    }
}