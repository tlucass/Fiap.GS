using Fiap.GS.DAL;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.GS.Controller
{
    public class Login
    {
        public static bool CheckLogin(string login, string senha)
        {
            using (var conexao = Banco.ObterConexao())
            {
                string sql = "SELECT COUNT(*) FROM Usuarios WHERE nome_usuario = :login AND senha = :senha";

                using (var comando = new OracleCommand(sql, conexao))
                {
                    comando.Parameters.Add(new OracleParameter("login", login));
                    comando.Parameters.Add(new OracleParameter("senha", senha));

                    int resultado = Convert.ToInt32(comando.ExecuteScalar());

                    return resultado > 0;
                }
            }
        }
    }
}
