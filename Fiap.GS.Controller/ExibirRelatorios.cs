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
            }
        }
    }
}
