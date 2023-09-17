namespace MVCPacienteHistorico.Models
{
    public class Historico
    {
        public long HistoricoId { get; set; }
        public string? Nome { get; set; }
        public long PacienteId { get; set;}
        public Paciente? Paciente { get; set; }
    }
}
