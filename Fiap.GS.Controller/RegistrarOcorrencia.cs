using Fiap.GS.DAL;
using Oracle.ManagedDataAccess.Client;
using Fiap.GS.Model;

namespace Fiap.GS.Controller
{
    public class RegistrarOcorrencia
    {
        public static void Cadastrar(Ocorrencia ocorrencia)
        {
            using (var conexao = Banco.ObterConexao())
            {
                string sql = @"INSERT INTO Ocorrencias 
            (data_hora_falha, sintoma_observado, nome_local, departamento, duracao_estimada, severidade, equipamento_danificado)
            VALUES (:dataHoraFalha, :sintoma, :nomeLocal, :departamento, :duracao, :severidade, :equipamento)
            RETURNING id INTO :id";

                using (var comando = new OracleCommand(sql, conexao))
                {
                    comando.Parameters.Add(new OracleParameter("dataHoraFalha", DBNull.Value));
                    comando.Parameters.Add(new OracleParameter("sintoma", ocorrencia.SintomaObservado));
                    comando.Parameters.Add(new OracleParameter("nomeLocal", ocorrencia.NomeLocal));
                    comando.Parameters.Add(new OracleParameter("departamento", ocorrencia.Departamento));
                    comando.Parameters.Add(new OracleParameter("duracao", ocorrencia.DuracaoEstimada.TotalMinutes));
                    comando.Parameters.Add(new OracleParameter("severidade", DBNull.Value));
                    comando.Parameters.Add(new OracleParameter("equipamento", DBNull.Value));

                    var idParam = new OracleParameter("id", OracleDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    comando.Parameters.Add(idParam);

                    comando.ExecuteNonQuery();

                    var id = (Oracle.ManagedDataAccess.Types.OracleDecimal)idParam.Value;
                    int idGerado = id.ToInt32();
                    Console.WriteLine($"\nOcorrência registrada com sucesso!\nID da ocorrência: {idGerado}");
                }
            }
        }

    }
}
