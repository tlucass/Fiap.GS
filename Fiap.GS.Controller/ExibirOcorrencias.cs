using Fiap.GS.DAL;
using Fiap.GS.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace Fiap.GS.Controller
{
    public class ExibirOcorrencias
    {
        public static List<Ocorrencia> ObterTodas()
        {
            var lista = new List<Ocorrencia>();

            using (var conexao = Banco.ObterConexao())
            {
                string sql = @"SELECT id, sintoma_observado, nome_local, departamento, duracao_estimada, 
                                        data_hora_falha, severidade, equipamento_danificado
                                 FROM Ocorrencias
                                 ORDER BY id";

                using (var comando = new OracleCommand(sql, conexao))
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ocorrencia = new Ocorrencia
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            SintomaObservado = reader["sintoma_observado"].ToString(),
                            NomeLocal = reader["nome_local"].ToString(),
                            Departamento = reader["departamento"].ToString(),
                            DuracaoEstimada = TimeSpan.FromMinutes(Convert.ToDouble(reader["duracao_estimada"])),
                            DataHoraFalha = reader["data_hora_falha"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["data_hora_falha"]),
                            Severidade = reader["severidade"].ToString(),
                            EquipamentoDanificado = reader["equipamento_danificado"].ToString()
                        };
                        lista.Add(ocorrencia);
                    }
                }
            }

            return lista;
        }

        public static void Exibir()
        {
            var ocorrencias = ObterTodas();

            if (ocorrencias.Count == 0)
            {
                Console.WriteLine("\nNenhuma ocorrência registrada até o momento.");
                return;
            }

            Console.WriteLine("\n=== Histórico de Ocorrências ===\n");
            foreach (var o in ocorrencias)
            {
                Console.WriteLine($"ID: {o.Id}");
                Console.WriteLine($"Sintoma: {o.SintomaObservado}");
                Console.WriteLine($"Local: {o.NomeLocal}");
                Console.WriteLine($"Departamento: {o.Departamento}");
                Console.WriteLine($"Duração Estimada: {o.DuracaoEstimada.TotalMinutes} min");
                Console.WriteLine($"Data/Hora da Falha: {(o.DataHoraFalha == DateTime.MinValue ? "Não registrada" : o.DataHoraFalha.ToString("dd/MM/yyyy HH:mm"))}");
                Console.WriteLine($"Severidade: {(!string.IsNullOrEmpty(o.Severidade) ? o.Severidade : "Não classificada")}");
                Console.WriteLine($"Equipamento Danificado: {(string.IsNullOrEmpty(o.EquipamentoDanificado) ? "Não informado" : o.EquipamentoDanificado)}");
                Console.WriteLine("---------------------------------------------");
            }
        }
    }
}
