using Microsoft.EntityFrameworkCore;
using Servicos.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicos.Servico
{
    public class ServicoDePonto : DbContext
    {
        private readonly string StringConexao;
        public ServicoDePonto(string stringConexao)
        {
            StringConexao = stringConexao;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StringConexao);
            base.OnConfiguring(optionsBuilder);
        }

        private DbSet<RegistroPonto> RegistrosPontos { get; set; }

        public List<RegistroPonto> Consulte()
        {
            return RegistrosPontos.Select(m => m).ToList();
        }

        public List<RegistroPonto> ConsultePorIntervalo(DateTime inicio, DateTime fim, string usuario)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT Id, Data, Hora, NomeUsuario, Tipo")
               .AppendLine("FROM RegistrosPontos")
               .AppendLine("WHERE")
               .AppendLine($"Data between CONVERT(DATETIME, '{inicio:yyyy-MM-dd HH:mm:ss}', 102)")
               .AppendLine($"AND CONVERT(DATETIME, '{fim:yyyy-MM-dd HH:mm:ss}', 102)")
               .AppendLine($"AND NomeUsuario = '{usuario}'");

            return RegistrosPontos.FromSqlRaw(sql.ToString()).ToListAsync().Result;
        }

        public bool Salvar(RegistroPonto registro)
        {
            try
            {
                var listaRegistroPontoUsuario = RegistrosPontos.Where(f => f.NomeUsuario == registro.NomeUsuario).ToList();
                var ultimoRegistroPontoUsuario = listaRegistroPontoUsuario.LastOrDefault();

                if (ultimoRegistroPontoUsuario == null)
                {
                    registro.Tipo = "Entrada";
                }
                else
                {
                    if (ultimoRegistroPontoUsuario.Tipo.TrimEnd() == "Entrada")
                    {
                        registro.Tipo = "Saída";
                    }
                    else
                    {
                        registro.Tipo = "Entrada";
                    }
                }

                var now = DateTime.UtcNow;
                registro.Data = now;
                registro.Hora = new TimeSpan(now.Hour, now.Minute, now.Second);

                this.Add(registro);
                var obj = SaveChangesAsync().Result;

                return registro.Id > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
