using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using static ProvaTecnica.Enums.Enums;

namespace ProvaTecnica.Controllers
{
    public class BaseController : Controller
    {
        ConfigurationRoot configuration = (ConfigurationRoot)new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        protected string StringConexao => configuration.GetConnectionString("Provider");
        public void chamarSweetAlert(string mensagem, TipoSweetAlert tipoSweetAlert)
        {
            var iconNotification = "";

            switch (tipoSweetAlert)
            {
                case TipoSweetAlert.Sucesso:
                    iconNotification = "success";
                    break;

                case TipoSweetAlert.Erro:
                    iconNotification = "errors";
                    break;

                case TipoSweetAlert.Aviso:
                    iconNotification = "warning";
                    break;

                case TipoSweetAlert.Informação:
                    iconNotification = "notice";
                    break;
            }

            var msg = "<script language='javascript'>swal('" + tipoSweetAlert.ToString() + "', '" + mensagem + "', '" + iconNotification + "')" + "</script>";

            TempData["sweetalert"] = msg;
        }

        //public void Message(string message, TipoSweetAlert tipoSweetAlert)
        //{
        //    TempData["Notification2"] = message;

        //    switch (tipoSweetAlert)
        //    {
        //        case TipoSweetAlert.Sucesso:
        //            TempData["NotificationCSS"] = "alert-box success";
        //            break;

        //        case TipoSweetAlert.Erro:
        //            TempData["NotificationCSS"] = "alert-box errors";
        //            break;

        //        case TipoSweetAlert.Aviso:
        //            TempData["NotificationCSS"] = "alert-box warning";
        //            break;

        //        case TipoSweetAlert.Informação:
        //            TempData["NotificationCSS"] = "alert-box notice";
        //            break;
        //    }
        //}
    }
}
