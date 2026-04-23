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
