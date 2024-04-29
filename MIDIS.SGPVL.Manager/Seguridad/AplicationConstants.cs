using Microsoft.AspNetCore.Http;
using MIDIS.SGPVL.ManagerDto.Seguridad;
using Newtonsoft.Json;

namespace MIDIS.SGPVL.Manager.Settings
{
    public class AplicationConstants : IAplicationConstants
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AplicationConstants(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UsuarioSesionBE UsuarioSesionBE
        {
            get
            {
                var data = _httpContextAccessor.HttpContext.Session.GetString("UsuarioSesionBE");
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<UsuarioSesionBE>(data);
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetString("UsuarioSesionBE",
                    JsonConvert.SerializeObject(value));
            }
        }

        public List<OpcionSesionBE> OpcionSesionBE
        {
            get
            {
                var data = _httpContextAccessor.HttpContext.Session.GetString("OpcionSesionBE");
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<List<OpcionSesionBE>>(data);
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetString("OpcionSesionBE", JsonConvert.SerializeObject(value));
            }
        }

        public string ImagenSesionUsuario
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetString("ImagenSesionUsuario");
            }

            set
            {
                _httpContextAccessor.HttpContext.Session.SetString("ImagenSesionUsuario", value);
            }
        }

        

        public string IpClienteCliente
        {
            get
            {
                return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        public DateTime FechaHoy
        {
            get
            {
                return TimeZoneInfo.ConvertTime(
                              DateTime.Now,
                              TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            }
        }

        public string RutaServidor
        {
            get
            {
                return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            }
        }
    }
}
