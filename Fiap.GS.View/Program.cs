using Fiap.GS.Controller;
using Fiap.GS.Model;
using System;


Console.WriteLine("""
===-===-===-===-===-===-===-===-===-===-===-===
          BEM-VINDO A ENERGYSHIELD
===-===-===-===-===-===-===-===-===-===-===-===
    
""");

Console.WriteLine("Realize o Login para começar");

bool autenticado = false;
int tentativasRestantes = 3;



while (!autenticado && tentativasRestantes > 0)
{
    Console.Write("Login: ");
    string login = Console.ReadLine();

    Console.Write("Senha: ");
    string senha = Console.ReadLine();

    autenticado = Login.CheckLogin(login, senha);

    if (!autenticado)
    {
        tentativasRestantes--;
        if (tentativasRestantes > 0)
        {
            Console.WriteLine($"\nLogin ou senha incorretos. Você tem mais {tentativasRestantes} tentativa(s).\n");
        }
        else
        {
            Console.WriteLine("\nNúmero máximo de tentativas excedido. Encerrando aplicação.");
            Environment.Exit(0);
        }
    }
}


if (autenticado)
{
    Console.WriteLine("""

        Login realizado com sucesso!

        Você está conectado ao sistema de gerenciamento da ENERGY SHIELD.

        """);

    while (autenticado)
    {
        Console.WriteLine("""

            Escolha qual função você deseja realizar

            1 - Registrar Ocorrência;
            2 - Classificar falha;
            3 - Agendar Manutenção;
            4 - Exibir Relatórios;
            5 - Exibir Histórico de Ocorrências;
            6 - Sair;

            Utilize o menu a seguir para navegar pelas funcionalidades.

            """);

        string esc = Console.ReadLine();

        switch (esc)
        {
            case "1":
                Console.WriteLine("\n=== Registrar Ocorrência ===");

                Console.Write("Sintoma observado: ");
                string sintoma = Console.ReadLine();

                // Nome do local - opções fixas
                string[] locais = {
        "Sala de Servidores",
        "Data Center",
        "Setor de Operações",
        "Andar 3 - CPD",
        "Laboratório Técnico",
        "Estação de Trabalho Financeiro"
    };

                Console.WriteLine("\nEscolha o local da ocorrência:");
                for (int i = 0; i < locais.Length; i++)
                    Console.WriteLine($"{i + 1} - {locais[i]}");

                Console.Write("Opção: ");
                if (!int.TryParse(Console.ReadLine(), out int opcaoLocal) || opcaoLocal < 1 || opcaoLocal > locais.Length)
                {
                    Console.WriteLine("Opção inválida. Retornando ao menu...");
                    break;
                }
                string nomeLocal = locais[opcaoLocal - 1];

                string[] departamentos = { "TI", "Financeiro", "Operações", "RH", "Manutenção", "Pediatria" };

                Console.WriteLine("\nEscolha o departamento responsável:");
                for (int i = 0; i < departamentos.Length; i++)
                    Console.WriteLine($"{i + 1} - {departamentos[i]}");

                Console.Write("Opção: ");
                if (!int.TryParse(Console.ReadLine(), out int opcaoDepto) || opcaoDepto < 1 || opcaoDepto > departamentos.Length)
                {
                    Console.WriteLine("Opção inválida. Retornando ao menu...");
                    break;
                }
                string departamento = departamentos[opcaoDepto - 1];

                Console.Write("Duração estimada (em minutos): ");
                if (!double.TryParse(Console.ReadLine(), out double duracaoMin))
                {
                    Console.WriteLine("Valor inválido para duração. Use números.");
                    break;
                }

                var ocorrencia = new Ocorrencia
                {
                    SintomaObservado = sintoma,
                    NomeLocal = nomeLocal,
                    Departamento = departamento,
                    DuracaoEstimada = TimeSpan.FromMinutes(duracaoMin)
                };

                try
                {
                    RegistrarOcorrencia.Cadastrar(ocorrencia);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nErro ao registrar ocorrência: " + ex.Message);
                }

                break;

            case "2":
                Console.WriteLine("\n=== Classificar Falha ===");

                Console.Write("Informe o ID da ocorrência: ");
                if (!int.TryParse(Console.ReadLine(), out int idOcorrencia) || !ClassificarFalha.VerificarIdExiste(idOcorrencia))
                {
                    Console.WriteLine("ID inválido ou inexistente.");
                    break;
                }

                Console.Write("Informe a data e hora da falha (ex: 05/06/2025 14:30): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataHoraFormat) || dataHoraFormat > DateTime.Now)
                {
                    Console.WriteLine("Data inválida. A data/hora da falha não pode estar no futuro.");
                    break;
                }

                Console.WriteLine("\nEscolha o equipamento danificado:");
                Console.WriteLine("1 - Servidor Principal");
                Console.WriteLine("2 - Banco de Dados");
                Console.WriteLine("3 - Roteador de Acesso");
                Console.WriteLine("4 - Switch de Distribuição");
                Console.WriteLine("5 - Cabo de Rede");
                Console.WriteLine("6 - Conector de Energia");
                Console.WriteLine("7 - Estabilizador");
                Console.WriteLine("8 - Nobreak");
                Console.WriteLine("9 - Painel Elétrico");
                Console.WriteLine("10 - Outro");

                Console.Write("Digite o número correspondente: ");
                if (!int.TryParse(Console.ReadLine(), out int opcaoEquipamento) || opcaoEquipamento < 1 || opcaoEquipamento > 10)
                {
                    Console.WriteLine("Opção inválida. Retornando ao menu...");
                    break;
                }

                string equipamentoDanificado = opcaoEquipamento switch
                {
                    1 => "Servidor Principal",
                    2 => "Banco de Dados",
                    3 => "Roteador de Acesso",
                    4 => "Switch de Distribuição",
                    5 => "Cabo de Rede",
                    6 => "Conector de Energia",
                    7 => "Estabilizador",
                    8 => "Nobreak",
                    9 => "Painel Elétrico",
                    _ => "Outro"
                };

                try
                {
                    string severidade = ClassificarFalha.AtualizarFalha(idOcorrencia, dataHoraFormat, equipamentoDanificado);

                    Console.WriteLine("\nFalha classificada com sucesso!");
                    Console.WriteLine($"Severidade: {severidade.ToUpper()}");

                    switch (severidade.ToLower())
                    {
                        case "crítica":
                            Console.WriteLine("\nAção recomendada: Agendar manutenção IMEDIATA.");
                            break;
                        case "alta":
                            Console.WriteLine("\nAção recomendada: Agendar manutenção urgente.");
                            break;
                        case "média":
                            Console.WriteLine("\nAção recomendada: Programar manutenção.");
                            break;
                        case "baixa":
                            Console.WriteLine("\nAção recomendada: Monitoramento contínuo.");
                            break;
                        default:
                            Console.WriteLine("\nAção recomendada: Verificar manualmente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao classificar falha: " + ex.Message);
                }

                break;

            case "3":
                Console.WriteLine("\n=== Agendar Manutenção ===");

                Console.Write("Data e hora agendada (dd/MM/yyyy HH:mm): ");
                string inputData = Console.ReadLine();

                if (!DateTime.TryParseExact(inputData, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dataHoraAgendada))
                {
                    Console.WriteLine("Formato inválido. Use exatamente: dd/MM/yyyy HH:mm");
                    break;
                }

                if (dataHoraAgendada <= DateTime.Now)
                {
                    Console.WriteLine("A data e hora não podem estar no passado.");
                    break;
                }

                Console.Write("Responsável pela manutenção: ");
                string responsavel = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(responsavel))
                {
                    Console.WriteLine("Responsável inválido.");
                    break;
                }

                Console.Write("Tipo de intervenção (preventiva ou corretiva): ");
                string tipo = Console.ReadLine().Trim().ToLower();

                if (tipo != "preventiva" && tipo != "corretiva")
                {
                    Console.WriteLine("Tipo de intervenção inválido. Digite exatamente 'preventiva' ou 'corretiva'.");
                    break;
                }

                var manutencao = new Manutencao
                {
                    DataHoraAgendada = dataHoraAgendada,
                    Responsavel = responsavel,
                    TipoIntervencao = tipo
                };

                try
                {
                    AgendarManutencao.Cadastrar(manutencao);
                    Console.WriteLine("\nManutenção agendada com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nErro ao agendar manutenção: " + ex.Message);
                }

                break;

            case "4":
                ExibirRelatorios.Exibir();
                break;
            case "5":
                ExibirOcorrencias.Exibir();
                break;
            case "6":
                Console.WriteLine("\nSaindo do sistema...");
                autenticado = false;
                break;

            default:
                Console.WriteLine("\nOpção inválida. Tente novamente.");
                break;
        }

        if (autenticado)
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}