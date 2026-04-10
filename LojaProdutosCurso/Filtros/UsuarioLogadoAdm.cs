using LojaProdutosCurso.Enums;
using LojaProdutosCurso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LojaProdutosCurso.Filtros
{
    public class UsuarioLogadoAdm : ActionFilterAttribute
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
                if(usuarioModel != null)
                {
                    if (usuarioModel.Cargo == CargoEnum.Cliente)
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            {"controller", "home"},
                            {"action","Index"}
                        });
                    }
                }
                else
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
