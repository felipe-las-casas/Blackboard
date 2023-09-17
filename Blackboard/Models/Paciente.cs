using System.ComponentModel;

namespace MVCPacienteHistorico.Models;

public class Paciente
{
    public long PacienteId { get; set; }
    public string? Nome { get; set; }
    [DisplayName("Situação")]
    public string? Situacao { get; set; }
    public virtual ICollection<Historico>? Historicos { get; set; }
    
}
