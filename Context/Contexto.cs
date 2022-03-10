using Control_Impressora.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Context
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Impressora> Impressora { get; set; }
        public DbSet<FileOnDatabaseModel> FileOnDatabaseModels { get; set; }
        public DbSet<ImpressoraDate> impressoraDate { get; set; }
     
    }
}
