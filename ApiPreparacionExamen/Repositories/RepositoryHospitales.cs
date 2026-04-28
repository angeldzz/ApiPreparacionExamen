using ApiPreparacionExamen.Data;
using ApiPreparacionExamen.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiPreparacionExamen.Repositories
{
    public class RepositoryHospitales
    {
        private HospitalContext context;
        public RepositoryHospitales(HospitalContext context)
        {
            this.context = context;
        }
        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await this.context.Empleados.ToListAsync();
        }
        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            return await this.context.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == idEmpleado);
        }
        public async Task<List<Empleado>> GetCompisAsync(int idDepartamento)
        {
            return await this.context.Empleados
                .Where(x => x.IdDepartamento == idDepartamento)
                .ToListAsync();
        }
        public async Task<Empleado> LogInEmpleado(string apellido, int idEmpleado)
        {
            return await this.context.Empleados
                .Where(x => x.Apellido == apellido && x.IdEmpleado == idEmpleado)
                .FirstOrDefaultAsync();
        }
        public async Task<List<string>> GetOficiosAsync()
        {
            var consulta = (from datos in this.context.Empleados select datos.Oficio).Distinct();
            return await consulta.ToListAsync();
        }
        public async Task<List<Empleado>>
            GetEmpleadosByOficioAsync(List<string> oficios)
        {
            var consulta = from datos in this.context.Empleados
                           where oficios.Contains(datos.Oficio)
                           select datos;
            return await consulta.ToListAsync();
        }
        public async Task IncrementarSalariosAsync(int incremento, List<string> oficios)
        {
            List<Empleado> empleados = await GetEmpleadosByOficioAsync(oficios);
            foreach (Empleado emp in empleados)
            {
                emp.Salario += incremento;
            }
            await this.context.SaveChangesAsync();
        }
        //GET ALL
        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            return await this.context.Hospitales.ToListAsync();
        }
        //FIND
        public async Task<Hospital> FindHospitalAsync(int hospitalcod)
        {
            return await this.context.Hospitales
                .FirstOrDefaultAsync(x => x.HOSPITAL_COD == hospitalcod);
        }
        //CREATE
        public async Task CreateHospitalAsync(
            int hospitalcod, string nombre, string direccion, string telefono, int numcamas, string? imagen)
        {
            Hospital hospital = new Hospital
            {
                HOSPITAL_COD = hospitalcod,
                NOMBRE = nombre,
                DIRECCION = direccion,
                TELEFONO = telefono,
                NUM_CAMA = numcamas,
                IMAGEN = imagen
            };
            await this.context.Hospitales.AddAsync(hospital);
            await this.context.SaveChangesAsync();
        }
        //UPDATE
        public async Task UpdateHospitalAsync(
            int hospitalcod, string nombre, string direccion, string telefono, int numcamas)
        {
            Hospital hospital = await this.FindHospitalAsync(hospitalcod);
            hospital.NOMBRE = nombre;
            hospital.DIRECCION = direccion;
            hospital.TELEFONO = telefono;
            hospital.NUM_CAMA = numcamas;
            await this.context.Hospitales.AddAsync(hospital);
            await this.context.SaveChangesAsync();
        }
        //DELETE
        public async Task DeleteDepertamentoAsync(int hospitalcod)
        {
            Hospital hospital = await this.FindHospitalAsync(hospitalcod);
            this.context.Hospitales.Remove(hospital);
            await this.context.SaveChangesAsync();
        }
        //Doctores
        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            return await this.context.Doctores.ToListAsync();
        }
        public async Task<Doctor> FindDoctorAsync(int doctorno)
        {
            return await this.context.Doctores.FirstOrDefaultAsync(x => x.DOCTOR_NO == doctorno);
        }
        public async Task<List<Doctor>> FindDoctoresHospitalAsync(int hospitalcod)
        {
            return await this.context.Doctores
                .Where(x => x.HOSPITAL_COD == hospitalcod).ToListAsync();
        }
        public async Task CreateDoctorAsync
            (int doctorid, int hospitalcod, string apellido,string especialidad,int salario )
        {
            Doctor doctor = new Doctor
            {
                DOCTOR_NO = doctorid,
                HOSPITAL_COD = hospitalcod,
                APELLIDO = apellido,
                ESPECIALIDAD = especialidad,
                SALARIO = salario
            };
            await this.context.Doctores.AddAsync(doctor);
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateDoctorAsync
            (int doctorid, int hospitalcod, string apellido, string especialidad, int salario)
        {
            Doctor doctor = await this.FindDoctorAsync(doctorid);
            doctor.HOSPITAL_COD = hospitalcod;
            doctor.APELLIDO = apellido;
            doctor.ESPECIALIDAD = especialidad;
            doctor.SALARIO = salario;
            await this.context.Doctores.AddAsync(doctor);
            await this.context.SaveChangesAsync();
        }
        public async Task DeleteDoctorAsync(int doctorid)
        {
            Doctor doctor = await this.FindDoctorAsync(doctorid);
            this.context.Doctores.Remove(doctor);
            await this.context.SaveChangesAsync();
        }
    }
}
