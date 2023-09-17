using Microsoft.EntityFrameworkCore;
using MVCPacienteHistorico.Models;

namespace Blackboard.Data
{
    public class BlackboardContext : DbContext
    {
        public BlackboardContext(DbContextOptions<BlackboardContext> options) : base(options){ }

        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }


    }
}
