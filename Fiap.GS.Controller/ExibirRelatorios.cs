using Fiap.GS.DAL;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Fiap.GS.Controller
{
    public class ExibirRelatorios
    {
        public static void Exibir()
        {
            using (var conexao = Banco.ObterConexao())
            {

                Console.WriteLine("=== Frequência de Ocorrências por Local ===");

                string sqlLocal = @"SELECT nome_local, COUNT(*) AS total
                                    FROM Ocorrencias
                                    GROUP BY nome_local
                                    ORDER BY total DESC";

                using (var comando = new OracleCommand(sqlLocal, conexao))
                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        Console.WriteLine($"{leitor["nome_local"]}: {leitor["total"]} ocorrência(s)");
                    }
                }

                Console.WriteLine("\n=== Relatório de Severidade das Falhas ===");

                string sqlSeveridade = @"SELECT severidade, COUNT(*) AS total
                                         FROM Ocorrencias
                                         GROUP BY severidade
                                         ORDER BY total DESC";

                using (var comando = new OracleCommand(sqlSeveridade, conexao))
                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        Console.WriteLine($"{leitor["severidade"]}: {leitor["total"]} ocorrência(s)");
                    }
                }

                Console.WriteLine("\n=== Agenda de Manutenções Futuras ===");

                string sqlFuturas = @"SELECT data_hora_agendada, responsavel, tipo_intervencao
                                      FROM Manutencoes
                                      WHERE data_hora_agendada > SYSDATE
                                      ORDER BY data_hora_agendada";

                using (var comando = new OracleCommand(sqlFuturas, conexao))
                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        DateTime data = Convert.ToDateTime(leitor["data_hora_agendada"]);
                        string responsavel = leitor["responsavel"].ToString();
                        string tipo = leitor["tipo_intervencao"].ToString();

                        Console.WriteLine($"{data:dd/MM/yyyy HH:mm} - {responsavel} ({tipo})");
                    }
                }

                Console.WriteLine("\n=== Total de Manutenções da Semana Atual ===");

                string sqlSemana = @"
                    SELECT COUNT(*) AS total
                    FROM Manutencoes
                    WHERE TO_CHAR(data_hora_agendada, 'IW') = TO_CHAR(SYSDATE, 'IW')
                    AND TO_CHAR(data_hora_agendada, 'YYYY') = TO_CHAR(SYSDATE, 'YYYY')";

                using (var comando = new OracleCommand(sqlSemana, conexao))
                {
                    int totalSemana = Convert.ToInt32(comando.ExecuteScalar());
                    Console.WriteLine($"Manutenções agendadas nesta semana: {totalSemana}");
                }
            }
        }
    }
}
