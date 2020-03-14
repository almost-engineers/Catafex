using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Entity;

namespace Persistencia.Listas
{
    public class Lista : IRepositorio
    {
        readonly IList<CATA> Catas;
        readonly IList<CATADOR> catadores;
        readonly IList<PANEL> paneles;
        readonly IList<EVENTO> eventos;
        readonly IList<ATRIBUTOSCAFE> atributosCafe;
        readonly IList<CAFE> cafes;
        readonly IList<CATACION> cataciones;

        public Lista()
        {
            this.Catas = new List<CATA>();
            this.catadores = new List<CATADOR>();
            this.paneles = new List<PANEL>();
            this.eventos = new List<EVENTO>();
            this.atributosCafe = new List<ATRIBUTOSCAFE>();
            this.cafes = new List<CAFE>();
            this.cataciones = new List<CATACION>();
            this.LlenarListas();

        }

        private void LlenarListas()
        {
            const int CANTIDAD_CATAR = 3;
            this.eventos.Add(new EVENTO
            {
                CODEVENTO = "EV-01",
                NOMBRE = "EVENTO 1",
                FECHA = DateTime.Parse("05/12/2019")
            });
            this.paneles.Add(new PANEL
            {
                CODPANEL = "PA-01",
                CODEVENTO = "EV-01",
                TIPOCAFE = "VERDE",
                HORA = TimeSpan.Parse("16:00")
            });
            this.cafes.Add(new CAFE
            {
                CODCAFE = "CA-01",
                TIPOCAFE = "VERDE",
                CODEVENTO = "EV-01",
            });
            this.atributosCafe.Add(new ATRIBUTOSCAFE
            {
                TIPOCAFE = "VERDE",
                DATOS = "FRAGANCIA;AROMA;ACIDEZ;AMARGO;CUERPO;SABOR_RESIDUAL;IMPRESION_GLOBAL"
            });
            this.catadores.Add(new CATADOR
            {
                NOMBRE = "Jhonathan",
                CEDULA = "1234",
                CORREO = "jhonathan@catafex.com",
                CODIGO = "CATADOR001",
                NIVELEXP = "EXPERIMENTADO",
                CONTRASEÑA = "e10adc3949ba59abbe56e057f20f883e",
                ESTADO = "HABILITADO"
            });
            this.cataciones.Add(new CATACION
            {
                CODCATACION = "CAT-01",
                CODPANEL = "PA-01",
                CODCATADOR = "CATADOR001",
                CODCAFE = "CA-01",
                CANTIDAD = CANTIDAD_CATAR
            });
        }
        public bool ActualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste)
        {
            return true;
        }

        public bool ActualizarCatación(string codCatacion, string codCafe, string codPanel, string codCatador, int cantidad)
        {
            throw new NotImplementedException();
        }
        private static bool ValidarDatosCatador2(string nombre, string cedula, string correo, string contraseña)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
            {
                return false;
            }
            return true;
        }

        public bool ActualizarCatador(string nombre, string cedula, string correo, string contraseña)
        {
            if (!ValidarDatosCatador2(nombre, cedula, correo, contraseña))
            {
                return false;
            }
            foreach (CATADOR catador in this.catadores)
            {
                if (catador.CEDULA.Equals(cedula))
                {
                    catador.NOMBRE = nombre;
                    catador.CONTRASEÑA = contraseña;
                    catador.CORREO = correo;
                    return true;
                }
            }
            return false;
        }

        public bool ActualizarEvento(string codigo, string nombre, DateTime fecha)
        {
            return true;
        }

        public bool ActualizarPanel(string codigo, string codEvento, string tipoCafe, TimeSpan hora)
        {
            return true;
        }

        public CATADOR BuscarCedulaCatador(string cedula)
        {
            foreach (CATADOR catador in this.catadores)
            {
                if (catador.CEDULA.Equals(cedula))
                {
                    return catador;
                }
            }
            return null;
        }
        public CATADOR BuscarCatador(string correo, string contraseña)
        {
            CATADOR catador = this.ObtenerCatador(correo);
            if (catador != null)
            {
                if (VerificarMd5Hash(contraseña, catador.CONTRASEÑA) && catador.ESTADO.Equals("HABILITADO") && correo.Equals(catador.CORREO))
                {
                    return catador;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        private CATADOR ObtenerCatador(string correo)
        {

            foreach (CATADOR catador in this.catadores)
            {
                if (catador.CORREO.Equals(correo))
                {
                    return catador;
                }
            }
            return null;
        }
        public REPORTE BuscarReporte(string codReporte)
        {
            return null;
        }

        public ADMINISTRADOR ConsultarAdministrador(string correo)
        {
            return null;
        }

        public ATRIBUTOSCAFE ConsultarAtributosCafe(string tipoCafe)
        {
            return null;
        }

        public CAFE ConsultarCafe(string codCafe)
        {
            foreach (CAFE cafe in this.cafes)
            {
                if (cafe.CODCAFE.Equals(codCafe))
                {
                    return cafe;
                }
            }
            return null;
        }

        public IList<CAFE> ConsultarCafes() => null;


        public IList<CAFE> CconsultarCafes(string tipoCafe) => null;


        public CATA ConsultarCata(string codigo)
        {
            return null;
        }

        public CATACION ConsultarCatacion(string codCatacion)
        {

            foreach (CATACION catacion in this.cataciones)
            {
                if (catacion.CODCATACION.Equals(codCatacion))
                {
                    return catacion;
                }
            }
            return null;
        }

        public IList<CATACION> ConsultarCataciones() => null;


        public IList<CATACION> ConsultarCatacionesAsignadas(string codCatador)
        {
            IList<CATACION> catacionesAsignadas = new List<CATACION>();
            foreach (CATACION catacion in this.cataciones)
            {
                if (catacion.CODCATADOR.Equals(codCatador))
                {
                    catacionesAsignadas.Add(catacion);
                }
            }
            if (catacionesAsignadas.Count > 0)
            {
                return catacionesAsignadas;
            }
            else
            {
                return null;
            }

        }



        public CATADOR ConsultarCatador(string correo)
        {
            this.LlenarListas();
            foreach (CATADOR catador in this.catadores)
            {
                if (catador.CORREO.Equals(correo))
                {
                    return catador;
                }
            }
            return null;

        }

        public EVENTO ConsultarEvento(string codEvento)
        {

            foreach (EVENTO evento in this.eventos)
            {
                if (evento.CODEVENTO.Equals(codEvento))
                {
                    return evento;
                }
            }
            return null;
        }


        public IList<EVENTO> ConsultarEventos() => null;

        public DateTime ConsultarFecha(string codigo)
        {
            return DateTime.Now;
        }

        public PANEL ConsultarPanel(string codPanel)
        {
            foreach (PANEL panel in this.paneles)
            {
                if (panel.CODPANEL.Equals(codPanel))
                {
                    return panel;
                }
            }
            return null;
        }

        public IList<PANEL> ConsultarPaneles() => null;

        public bool ConsultarUsuario(string cedula)
        {
            return false;
        }

        public bool EliminarCafe(string codigo)
        {
            return false;
        }

        public bool EliminarCatador(string cedula)
        {
            foreach (CATADOR catador in this.catadores)
            {
                if (catador.CEDULA.Equals(cedula))
                {
                    this.catadores.Remove(catador);
                    return true;
                }
            }
            return false;
        }
        private static string GetMD5Hash(string contraseña)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] datos = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder sBuilder = new StringBuilder();
                foreach (byte b in datos)
                {
                    //Le da un formato hexadecimal a cada byte de informacion, ademas de transformalo en string
                    sBuilder.Append(b.ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        private static bool VerificarMd5Hash(string contraseña, string hash)
        {

            const int RESPUESTACOMPARER = 0;


            string hashContraseña = GetMD5Hash(contraseña);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashContraseña, hash) == RESPUESTACOMPARER;
        }

        public bool EliminarEvento(string codEvento)
        {
            return false;
        }

        public bool EliminarPanel(string codigo)
        {
            return false;
        }

        public bool InsertarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste)
        {

            return false;
        }

        public void HabilitarCatador(string cedula)
        {
            foreach (CATADOR catador in this.catadores)
            {
                if (catador.CEDULA.Equals(cedula))
                {
                    catador.ESTADO = "HABILITADO";
                    return;
                }
            }
        }
        private static bool ValidarDatosCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(correo) ||
                string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(nivelExp))
            {
                return false;
            }
            return true;
        }
        public bool InsertarCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp)
        {
            try
            {
                Int64.Parse(cedula);
            }
            catch (FormatException)
            {
                return false;
            }
            if (!ValidarDatosCatador(nombre, cedula, codigo, correo, contraseña, nivelExp))
            {
                return false;
            }
            try
            {
                this.catadores.Add(new CATADOR
                {
                    NOMBRE = nombre,
                    CEDULA = cedula,
                    CODIGO = codigo,
                    CORREO = correo,
                    CONTRASEÑA = contraseña,
                    NIVELEXP = nivelExp,
                    ESTADO = "INHABILITADO"
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertarEvento(string nombre, DateTime fecha)
        {
            return false;
        }

        public bool InsertarPanel(string codEvento, string tipoCafe, TimeSpan hora)
        {
            return false;
        }

        public bool InsertarReporte()
        {
            return false;
        }
        private string ObtenerAtributosCafes(string tipoCafe)
        {
            string datos = null;
            foreach (ATRIBUTOSCAFE atributos in this.atributosCafe)
            {
                if (atributos.TIPOCAFE.Equals(tipoCafe))
                {
                    datos = atributos.DATOS;
                }
            }
            return datos;
        }
        public Dictionary<string, string> ObtenerInformacionCatacion(string codCatacion)
        {
            Dictionary<string, string> catas = new Dictionary<string, string>();

            CATACION catacion = ConsultarCatacion(codCatacion);
            PANEL panel = ConsultarPanel(catacion.CODPANEL);
            EVENTO evento = ConsultarEvento(panel.CODEVENTO);
            CAFE cafe = ConsultarCafe(catacion.CODCAFE);
            string atributosCafeCatas = ObtenerAtributosCafes(panel.TIPOCAFE);

            if (catacion != null && panel != null && evento != null && cafe != null)
            {
                string hora = panel.HORA.ToString();
                string fecha = evento.FECHA.ToShortDateString();
                string tipoCafe = panel.TIPOCAFE;
                string CodCafe = cafe.CODCAFE;
                string cantVez = catacion.CANTIDAD.ToString();

                catas.Add("hora", hora);
                catas.Add("fecha", fecha);
                catas.Add("tipoCafe", tipoCafe);
                catas.Add("CodCafe", CodCafe);
                catas.Add("cantVez", cantVez);
                catas.Add("atributos", atributosCafeCatas);
            }

            return catas;

        }



        public bool RegistrarCata(CATA cata)
        {
            try
            {
                Catas.Add(cata);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


        public bool RegistrarCata(string codCatacion, int rancidez,
            int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal
            , int fragancia, int saborResidual, string observaciones)
        {
            if (!VerificarRango(rancidez, dulce, acidez, cuerpo, aroma,
             amargo, impresionGlobal, fragancia, saborResidual))
            {
                return false;
            }

            try
            {
                Catas.Add(new CATA
                {

                    CODCATACION = codCatacion,
                    VEZCATADA = 1,
                    RANCIDEZ = rancidez,
                    DULCE = dulce,
                    ACIDEZ = acidez,
                    AROMA = aroma,
                    AMARGO = amargo,
                    FRAGANCIA = fragancia,
                    SABORESIDUAL = saborResidual,
                    CUERPO = cuerpo,
                    IMPRESIONGLOBAL = impresionGlobal,
                    OBSERVACIONES = observaciones

                });

                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }

        }

        public bool RegistrarCatacion(string codPanel, string codCatador, string codCafe, int cantidad)
        {
            return false;
        }
        private static bool VerificarRango(int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal, int fragancia, 
                                    int saborResidual)
        {
            const int MAXIMO_RANGO = 10;
            if (rancidez < 0 || dulce < 0 || acidez < 0 || cuerpo < 0 || aroma < 0 || amargo < 0 || impresionGlobal < 0 || fragancia < 0 || 
                saborResidual < 0 || rancidez > MAXIMO_RANGO || dulce > MAXIMO_RANGO || acidez > MAXIMO_RANGO || cuerpo > MAXIMO_RANGO || 
                aroma > MAXIMO_RANGO || amargo > MAXIMO_RANGO || impresionGlobal > MAXIMO_RANGO || fragancia > MAXIMO_RANGO || 
                saborResidual > MAXIMO_RANGO )
            {
                return false;
            }
            return true;
        }

        public IList<CATADOR> ConsultarCatadores()
        {
            throw new NotImplementedException();
        }

        public IList<PANEL> ConsultarPanelesPorEvento(string codEvento)
        {
            throw new NotImplementedException();
        }

        public bool PanelTerminado(string codPanel)
        {
            throw new NotImplementedException();
        }

  
        public byte[] GenerarImagen(string codPanel)
        {
            throw new NotImplementedException();
        }

        public IList<CAFE> ObtenerCafesMismoTipoPanel(string codPanel)
        {
            throw new NotImplementedException();
        }

        public string[] GetObservaciones(string codPanel)
        {
            throw new NotImplementedException();
        }

        public bool PertenecePanel(string codPanel, string codEvento)
        {
            throw new NotImplementedException();
        }

        public string ObtenerValoresDefectoCafes(string tipoCafe)
        {
            throw new NotImplementedException();
        }

        public string GetCorreoCatador(string codCatador)
        {
            throw new NotImplementedException();
        }

        public string ConstruirAsuntoCorreo(string codPanel)
        {
            throw new NotImplementedException();
        }

        public string ConstruirMensajeCorreo(List<CATACION> cataciones)
        {
            throw new NotImplementedException();
        }

        public List<CATADOR> ObtenerCatadoresInhabilitados()
        {
            throw new NotImplementedException();
        }

        bool IRepositorio.HabilitarCatador(string codCatador)
        {
            throw new NotImplementedException();
        }

        public List<CATADOR> ObtenerCatadoresHabilitados()
        {
            throw new NotImplementedException();
        }

        public CATADOR CatadorHabilitado(string codCatador)
        {
            throw new NotImplementedException();
        }

        public bool ExistePanel(string codPanel)
        {
            throw new NotImplementedException();
        }
    }

}
