namespace Fiap.GS.Model
{
    public class Ocorrencia
    {
        public int Id { get; set; }
        public DateTime DataHoraFalha { get; set; }
        public string SintomaObservado { get; set; }
        public string NomeLocal { get; set; }
        public string Departamento { get; set; } // Para manter junto com local
        public TimeSpan DuracaoEstimada { get; set; }
        public string Severidade { get; set; }
        public string EquipamentoDanificado { get; set; } // Ajuda na priorização

    }
}
