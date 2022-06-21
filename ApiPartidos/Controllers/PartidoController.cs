using ApiPartidos.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPartidos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartidoController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        string ConnectionString = "Server=tcp:sqlserverappligamx.database.windows.net,1433;Initial Catalog=sqlappligamx;Persist Security Info=False;User ID=andres;Password=Marcosguapo.123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public PartidoController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<PartidoController>
        [HttpGet]
        public ApiResponse Get()
        {
            return new PartidoModel().GetAll();
        }

        
        // GET api/<PartidoController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            try
            {
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "El partido se obtuvo exitosamente",
                    Result = new PartidoModel().Get(id)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al buscar el partido: {ex.Message}",
                    Result = null
                };
            }
        }

        // POST api/<PartidoController>
        [HttpPost]
        public ApiResponse Post([FromBody] PartidoModel model)
        {
            try
            {
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "El partido se creó exitosamente",
                    Result = new PartidoModel().Post(model)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"a: {ex.Message}",
                    Result = null
                };
            }
        }

        // PUT api/<PartidoController>/5
        [HttpPut("{id}")]
        public ApiResponse Put([FromBody] PartidoModel model)
        {
            try
            {
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "El partido se ha modificado exitosamente",
                    Result = new PartidoModel().Update(model)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al modificar el partido: {ex.Message}",
                    Result = null
                };
            }
        }

        // DELETE api/<PartidoController>/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            try
            {
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "El partido se eliminó exitosamente",
                    Result = new PartidoModel ().Delete(id)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al eliminar el partido: {ex.Message}",
                    Result = null
                };
            }
        }
        
    }
}
