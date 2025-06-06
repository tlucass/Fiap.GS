using Oracle.ManagedDataAccess.Client;

namespace Fiap.GS.DAL
{
    public class Banco
    {
        public static OracleConnection ObterConexao()
        {
            string connectionString = "User Id=rm97913;Password=270304;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));";

            var conexao = new OracleConnection(connectionString);
            conexao.Open();
            return conexao;
        }
    }
}
