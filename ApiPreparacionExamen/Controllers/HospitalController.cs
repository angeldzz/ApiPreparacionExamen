using ApiPreparacionExamen.Models;
using ApiPreparacionExamen.Repositories;
using ApiPreparacionExamen.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiPreparacionExamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private RepositoryHospitales repo;
        private HelperSubirFoto helperSubirFoto;

        public HospitalController(RepositoryHospitales repo, HelperSubirFoto helperSubirFoto)
        {
            this.repo = repo;
            this.helperSubirFoto = helperSubirFoto;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Hospital>>> GetHospital()
        {
            return await this.repo.GetHospitalesAsync();
        }
        [HttpGet("{cod}")]
        public async Task<ActionResult<Hospital>> FindHospital(int cod)
        {
            return await this.repo.FindHospitalAsync(cod);
        }
        [HttpPost]
        public async Task<ActionResult> Post(Hospital hospital)
        {
            await this.repo.CreateHospitalAsync(hospital.HOSPITAL_COD,hospital.NOMBRE,
                                                hospital.DIRECCION,hospital.TELEFONO,
                                                hospital.NUM_CAMA, hospital.IMAGEN);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put(Hospital hospital)
        {
            await this.repo.UpdateHospitalAsync(hospital.HOSPITAL_COD, hospital.NOMBRE,
                                                hospital.DIRECCION, hospital.TELEFONO,
                                                hospital.NUM_CAMA);
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
