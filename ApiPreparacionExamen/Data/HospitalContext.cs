using ApiPreparacionExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPreparacionExamen.Data
{
    public class HospitalContext:DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        { }
        public DbSet<Hospital> Hospitales { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
    }
}
