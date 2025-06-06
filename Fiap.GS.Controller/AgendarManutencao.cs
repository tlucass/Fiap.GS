using Fiap.GS.DAL;
using Fiap.GS.Model;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Fiap.GS.Controller
{
    public class AgendarManutencao
    {
        public static void Cadastrar(Manutencao manutencao)
        {
            using (var conexao = Banco.ObterConexao())
            {
                string sql = @"INSERT INTO Manutencoes 
                    (data_hora_agendada, responsavel, tipo_intervencao)
                    VALUES (:dataHoraAgendada, :responsavel, :tipoIntervencao)";

                using (var comando = new OracleCommand(sql, conexao))
                {
                    comando.Parameters.Add(new OracleParameter("dataHoraAgendada", manutencao.DataHoraAgendada));
                    comando.Parameters.Add(new OracleParameter("responsavel", manutencao.Responsavel));
                    comando.Parameters.Add(new OracleParameter("tipoIntervencao", manutencao.TipoIntervencao));

                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void Agendar()
        {
            Console.Clear();
            Console.WriteLine("=== Agendar Manutenção ===");

            Console.Write("Data e hora agendada (dd/MM/yyyy HH:mm): ");
            string inputDataHora = Console.ReadLine();

            if (!DateTime.TryParseExact(inputDataHora, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dataHoraAgendada))
            {
                Console.WriteLine("Formato inválido. Use exatamente: dd/MM/yyyy HH:mm");
                return;
            }

            if (dataHoraAgendada <= DateTime.Now)
            {
                Console.WriteLine("A data e hora não podem estar no passado.");
                return;
            }

            Console.Write("Responsável pela manutenção: ");
            string responsavel = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(responsavel))
            {
                Console.WriteLine("\nResponsável inválido. Retornando ao menu...");
                return;
            }

            Console.Write("\nTipo de intervenção (preventiva ou corretiva): ");
            string tipoIntervencao = Console.ReadLine().Trim().ToLower();

            if (tipoIntervencao != "preventiva" && tipoIntervencao != "corretiva")
            {
                Console.WriteLine("\nTipo de intervenção inválido. Digite exatamente 'preventiva' ou 'corretiva'.");
                return;
            }

            var manutencao = new Manutencao
            {
                DataHoraAgendada = dataHoraAgendada,
                Responsavel = responsavel,
                TipoIntervencao = tipoIntervencao
            };

            Cadastrar(manutencao);
            Console.WriteLine("\nManutenção agendada com sucesso!");
        }
    }
}
