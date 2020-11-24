using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProvaTecnica.Models;
using Servicos.Negocio;
using Servicos.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProvaTecnica.Enums.Enums;

namespace ProvaTecnica.Controllers
{
    public class RegistrosPontosController : BaseController
    {
        // GET: RegistrosPontos/Index
        public ActionResult Index()
        {
            var servico = new ServicoDePonto(StringConexao);
            var usuarios = servico.Consulte().Select(x => x.NomeUsuario);

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var usuario in usuarios.Distinct())
            {
                items.Add(new SelectListItem { Text = usuario, Value = usuario });
            }

            ViewBag.NomeUsuario = items;

            return View();
        }

        // GET: RegistrosPontos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrosPontos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Data,NomeUsuario,Tipo,Hora")] DtoRegistroPonto registroPonto)
        {
            var servico = new ServicoDePonto(StringConexao);
            if (ModelState.IsValid)
            {
                var ponto = new RegistroPonto()
                {
                    NomeUsuario = registroPonto.NomeUsuario
                };

                if (servico.Salvar(ponto))
                {
                    chamarSweetAlert("Registro de ponto realizado com sucesso", TipoSweetAlert.Sucesso);
                }
                else
                {
                    chamarSweetAlert("Registro de ponto não realizado", TipoSweetAlert.Aviso);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: RegistrosPontos/GerarRelatorio
        [HttpPost]
        public ActionResult Report(DateTime DataInicial, DateTime DataFinal, string NomeUsuario)
        {
            var servico = new ServicoDePonto(StringConexao);
            List<RegistroPonto> registrosPontos;
            if (DataInicial.Year > 1 && DataFinal.Year > 1)
            {
                var dataFinal = DateTime.Parse($"{DataFinal.Year}-{DataFinal.Month}-{DataFinal.Day} 23:59:59");
                registrosPontos = servico.ConsultePorIntervalo(DataInicial, DataFinal, NomeUsuario);
            }
            else
            {
                registrosPontos = servico.Consulte().Where(w => w.NomeUsuario == NomeUsuario).ToList();
            }

            if (registrosPontos.Count == 0)
            {
                chamarSweetAlert("Sem dados para gerar relatório", TipoSweetAlert.Aviso);
                return RedirectToAction("Index", "RegistrosPontos");
            }

            var registros = registrosPontos.Select(x =>
            {
                var data = new DateTime(x.Data.Year, x.Data.Month, x.Data.Day, x.Hora.Hours, x.Hora.Minutes, x.Hora.Minutes);
                return new DtoRegistroPonto()
                {
                    Id = x.Id,
                    Registro = data.ToLocalTime(),
                    NomeUsuario = x.NomeUsuario,
                    Tipo = x.Tipo
                };
            });

            return View(registros.ToList());
        }
    }
}
