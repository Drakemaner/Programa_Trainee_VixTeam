using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Programa_Trainee_VixTeam.Models;

namespace Programa_Trainee_VixTeam.Data
{
    public class Programa_Trainee_VixTeamContext : DbContext
    {
        public Programa_Trainee_VixTeamContext (DbContextOptions<Programa_Trainee_VixTeamContext> options)
            : base(options)
        {
        }

        public DbSet<Programa_Trainee_VixTeam.Models.PessoaModel> PessoaModel { get; set; }
    }
}
