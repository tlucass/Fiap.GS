# ⚡ EnergyShield

**EnergyShield** é um sistema de gerenciamento de ocorrências relacionadas a falhas de energia e operações de manutenção em ambientes corporativos e hospitalares. Desenvolvido como um aplicativo console em C#, ele visa auxiliar equipes técnicas na identificação, controle e prevenção de problemas que afetam a infraestrutura elétrica e tecnológica de organizações.

---

## 👤 Integrantes
- David Guilherme B. Denunci - rm98603
- Lucas P. de Toledo - rm97913


## 🎯 Finalidade

O sistema foi projetado para atender demandas reais de monitoramento e resposta a falhas, com foco em:

- Registro de ocorrências com informações detalhadas (sintoma, local, departamento, duração).
- Classificação de falhas com base na severidade do impacto e no tipo de equipamento danificado.
- Agendamento de manutenções preventivas ou corretivas com controle de datas e responsáveis.
- Geração de relatórios para apoio à tomada de decisão.
- Histórico completo das ocorrências registradas para consulta e auditoria.

Ideal para profissionais de infraestrutura, operação de TI, analistas de manutenção e gestores técnicos.

---

## 🚀 Como Executar

### Pré-requisitos

- .NET SDK 6.0 ou superior
- Oracle Client instalado
- Banco de dados Oracle com tabelas e permissões configuradas


## 📂 Estrutra
``` 
energyshield/
│
├── Controller/
│   ├── RegistrarOcorrencia.cs
│   ├── ClassificarFalha.cs
│   ├── AgendarManutencao.cs
│   ├── ExibirOcorrencias.cs
│   └── ExibirRelatorios.cs
│
├── DAL/
│   └── Banco.cs
│
├── Model/                     
│   ├── Ocorrencia.cs
│   └── Manutencao.cs
│
└── Program.cs

```

