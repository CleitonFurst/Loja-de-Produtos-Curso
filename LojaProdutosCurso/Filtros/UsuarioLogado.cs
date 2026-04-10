using LojaProdutosCurso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LojaProdutosCurso.Filtros
{
    public class UsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessao = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessao))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "login"},
                    {"action","Login"}

                });
            }
            else
            {
                UsuarioModel usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(sessao);

                if(usuarioModel == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"controller", "login"},
                        {"action","Login"}
                    });
                }
            }


            base.OnActionExecuting(context);
        }
    }
}
