using ApiPreparacionExamen.Models;
using ApiPreparacionExamen.Repositories;
using ApiPreparacionExamen.Helpers;
using Microsoft.AspNetCore.Mvc;

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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Post([FromForm] Hospital hospital)
        {
            if (hospital.Imagen != null)
            {
                byte[] datos = await this.helperSubirFoto.ConvertirImagenABytesAsync(hospital.Imagen);
                hospital.IMAGEN = await this.helperSubirFoto.GuardarArchivoByteAsync(
                    datos,
                    hospital.Imagen.FileName,
                    "wwwroot\\imagenes"
                );
            }

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
