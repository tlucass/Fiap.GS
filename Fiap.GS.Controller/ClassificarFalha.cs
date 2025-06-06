using Fiap.GS.DAL;
using Fiap.GS.Model;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Fiap.GS.Controller
{
    public class ClassificarFalha
    {
        public static string AtualizarFalha(int idOcorrencia, DateTime dataHoraFalha, string equipamentoDanificado)
        {
            using (var conexao = Banco.ObterConexao())
            {
                string severidade = CalcularSeveridade(dataHoraFalha, equipamentoDanificado);

                string sql = @"UPDATE Ocorrencias 
                               SET data_hora_falha = :dataFalha,
                                   equipamento_danificado = :equipamento,
                                   severidade = :severidade
                               WHERE id = :id";

                using (var comando = new OracleCommand(sql, conexao))
                {
                    comando.Parameters.Add(new OracleParameter("dataFalha", dataHoraFalha));
                    comando.Parameters.Add(new OracleParameter("equipamento", equipamentoDanificado));
                    comando.Parameters.Add(new OracleParameter("severidade", severidade));
                    comando.Parameters.Add(new OracleParameter("id", idOcorrencia));

                    comando.ExecuteNonQuery();
                }

                return severidade;
            }
        }

        private static string CalcularSeveridade(DateTime dataHoraFalha, string equipamento)
        {
            TimeSpan diferenca = DateTime.Now - dataHoraFalha;
            string equipamentoLower = equipamento.ToLower();

            if (equipamentoLower.Contains("servidor") || equipamentoLower.Contains("banco"))
                return "Crítica";

            if (equipamentoLower.Contains("roteador") || equipamentoLower.Contains("switch"))
                return diferenca.TotalHours > 5 ? "Alta" : "Média";

            if (equipamentoLower.Contains("cabo") || equipamentoLower.Contains("conector"))
                return diferenca.TotalHours > 10 ? "Média" : "Baixa";

            return "Baixa";
        }

        public static bool VerificarIdExiste(int id)
        {
            using (var conexao = Banco.ObterConexao())
            {
                string sql = "SELECT COUNT(*) FROM Ocorrencias WHERE id = :id";

                using (var comando = new OracleCommand(sql, conexao))
                {
                    comando.Parameters.Add(new OracleParameter("id", id));
                    int count = Convert.ToInt32(comando.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
