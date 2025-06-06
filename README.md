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


## 🛠️ Instruções de Uso

### Acesso ao Sistema  
- Ao iniciar o programa, será solicitado o **login** e a **senha**.  
- Utilize as credenciais padrão:  
  - **Usuário:** `admin`  
  - **Senha:** `admin123`  
- O sistema permite até 3 tentativas antes de encerrar a aplicação.

---

### 1. Registrar Ocorrência  
- Forneça o **sintoma observado**, **local**, **setor hospitalar** e **duração estimada da falha**.  
- Os locais e setores disponíveis são listados para escolha numérica, garantindo padronização.

### 2. Classificar Falha  
- Informe o **ID da ocorrência** registrada.  
- Selecione o **equipamento danificado** a partir de uma lista fornecida.  
- A severidade será determinada automaticamente com base no tipo de equipamento e no tempo desde a falha.

### 3. Agendar Manutenção  
- Informe a **data e hora futura** da manutenção (formato `dd/MM/yyyy HH:mm`).  
- Insira o **nome do responsável** que fará a manutenção e escolha o tipo de intervenção: **preventiva** ou **corretiva**.

### 4. Exibir Relatórios  
- Veja a **frequência de ocorrências por local**.  
- Consulte o **relatório de severidade das falhas**.  
- Acesse também a **agenda de manutenções futuras** e saiba **quantos agendamentos existem para a semana atual**.

### 5. Exibir Histórico de Ocorrências  
- Lista completa das ocorrências registradas no sistema.

### 6. Sair  
- Encerra a sessão e finaliza a aplicação.


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
└── View/                     
    └── Program.cs

```

