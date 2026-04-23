using ApiPreparacionExamen.Models;
using ApiPreparacionExamen.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPreparacionExamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private RepositoryHospitales repo;
        public DoctorController(RepositoryHospitales repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctores()
        {
            return await this.repo.GetDoctoresAsync();
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Doctor>> FindDoctor(int id)
        {
            return await this.repo.FindDoctorAsync(id);
        }
        [HttpGet]
        [Route("[action]/{hospitalcod}")]
        public async Task<ActionResult<List<Doctor>>> FindDoctoresHospital(int hospitalcod)
        {
            return await this.repo.FindDoctoresHospitalAsync(hospitalcod);
        }
        [HttpPost]
        public async Task<ActionResult> Post(Doctor doctor)
        {
            await this.repo.CreateDoctorAsync(doctor.DOCTOR_NO,doctor.HOSPITAL_COD,
                                                doctor.APELLIDO, doctor.ESPECIALIDAD,
                                                doctor.SALARIO);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put(Doctor doctor)
        {
            await this.repo.UpdateDoctorAsync(doctor.DOCTOR_NO, doctor.HOSPITAL_COD,
                                                doctor.APELLIDO, doctor.ESPECIALIDAD,
                                                doctor.SALARIO);
            return Ok();
        }
        [HttpDelete("{cod}")]
        public async Task<ActionResult> Delete(int cod)
        {
            await this.repo.DeleteDepertamentoAsync(cod);
            return Ok();
        }
    }
}
