using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Entity;

namespace Persistencia.Listas
{
    public class Lista : Repositorio
    {
        IList<CATA> Catas;
        IList<CATADOR> catadores;
        IList<PANEL> paneles;
        IList<EVENTO> eventos;
        IList<ATRIBUTOSCAFE> atributosCafe;
        IList<CAFE> cafes;
        IList<CATACION> cataciones;

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

            this.eventos.Add(new EVENTO()
            {
                CODEVENTO = "EV-01",
                NOMBRE = "EVENTO 1",
                FECHA = DateTime.Parse("05/12/2019")
            });
            this.paneles.Add(new PANEL()
            {
                CODPANEL = "PA-01",
                CODEVENTO = "EV-01",
                TIPOCAFE = "VERDE",
                HORA = TimeSpan.Parse("16:00")
            });
            this.cafes.Add(new CAFE()
            {
                CODCAFE = "CA-01",
                TIPOCAFE = "VERDE",
                CODEVENTO = "EV-01",
            });
            this.atributosCafe.Add(new ATRIBUTOSCAFE()
            {
                TIPOCAFE = "VERDE",
                DATOS = "FRAGANCIA;AROMA;ACIDEZ;AMARGO;CUERPO;SABOR_RESIDUAL;IMPRESION_GLOBAL"
            });
            this.catadores.Add(new CATADOR()
            {
                NOMBRE = "Jhonathan",
                CEDULA = "1234",
                CORREO = "jhonathan@catafex.com",
                CODIGO = "CATADOR001",
                NIVELEXP = "EXPERIMENTADO",
                CONTRASEÑA = "e10adc3949ba59abbe56e057f20f883e",
                ESTADO = "HABILITADO"
            });
            this.cataciones.Add(new CATACION()
            {
                CODCATACION = "CAT-01",
                CODPANEL = "PA-01",
                CODCATADOR = "CATADOR001",
                CODCAFE = "CA-01",
                CANTIDAD = 3
            });
        }
        public bool actualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste)
        {
            return true;
        }

        public bool actualizarCatación(string codCatacion, string codCafe, string codPanel, string codCatador, int cantidad)
        {
            throw new NotImplementedException();
        }
        private bool validarDatosCatador2(string nombre, string cedula, string correo, string contraseña)
        {
            if (nombre.Equals("") || cedula.Equals("") || correo.Equals("") || contraseña.Equals(""))
            {
                return false;
            }
            return true;
        }

        public bool actualizarCatador(string nombre, string cedula, string correo, string contraseña)
        {
            if (!validarDatosCatador2(nombre, cedula, correo, contraseña))
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

        public bool actualizarEvento(string codigo, string nombre, DateTime fecha)
        {
            return true;
        }

        public bool actualizarPanel(string codigo, string codEvento, string tipoCafe, TimeSpan hora)
        {
            return true;
        }

        public CATADOR buscarCedulaCatador(string cedula)
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
        public CATADOR buscarCatador(string correo, string contraseña)
        {
            CATADOR catador = this.obtenerCatador(correo);
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

        private CATADOR obtenerCatador(string correo)
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
        public REPORTE buscarReporte(string codReporte)
        {
            return null;
        }

        public ADMINISTRADOR consultarAdministrador(string correo)
        {
            return null;
        }

        public ATRIBUTOSCAFE consultarAtributosCafe(string tipoCafe)
        {
            return null;
        }

        public CAFE consultarCafe(string codCafe)
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

        public IList<CAFE> consultarCafes() => null;


        public IList<CAFE> consultarCafes(string tipoCafe) => null;


        public CATA consultarCata(string codigo)
        {
            return null;
        }

        public CATACION consultarCatacion(string codCatacion)
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

        public IList<CATACION> consultarCataciones() => null;


        public IList<CATACION> consultarCatacionesAsignadas(string codCatador)
        {
            IList<CATACION> cataciones = new List<CATACION>();
            foreach (CATACION catacion in this.cataciones)
            {
                if (catacion.CODCATADOR.Equals(codCatador))
                {
                    cataciones.Add(catacion);
                }
            }
            if (cataciones.Count > 0)
            {
                return cataciones;
            }
            else
            {
                return null;
            }

        }



        public CATADOR consultarCatador(string correo)
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

        public EVENTO consultarEvento(string codEvento)
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


        public IList<EVENTO> consultarEventos() => null;

        public DateTime consultarFecha(string codigo)
        {
            return DateTime.Now;
        }

        public PANEL consultarPanel(string codPanel)
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

        public IList<PANEL> consultarPaneles() => null;

        public IList<REPORTE> consultarReportes() => null;

        public bool consultarUsuario(string cedula)
        {
            return false;
        }

        public bool eliminarCafe(string codigo)
        {
            return false;
        }

        public bool eliminarCatador(string cedula)
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
        private string getMD5Hash(string contraseña)
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
        private bool VerificarMd5Hash(string contraseña, string hash)
        {

            const int RESPUESTACOMPARER = 0;


            string hashContraseña = getMD5Hash(contraseña);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashContraseña, hash) == RESPUESTACOMPARER;
        }

        public bool eliminarEvento(string codEvento)
        {
            return false;
        }

        public bool eliminarPanel(string codigo)
        {
            return false;
        }

        public bool insertarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste)
        {

            return false;
        }

        public void habilitarCatador(string cedula)
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
        private bool validarDatosCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp)
        {
            if (nombre.Equals("") || cedula.Equals("") || codigo.Equals("") || correo.Equals("") || contraseña.Equals("") || nivelExp.Equals(""))
            {
                return false;
            }
            return true;
        }
        public bool insertarCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp)
        {
            try
            {
                Int64.Parse(cedula);
            }
            catch (FormatException)
            {
                return false;
            }
            if (!validarDatosCatador(nombre, cedula, codigo, correo, contraseña, nivelExp))
            {
                return false;
            }
            try
            {
                this.catadores.Add(new CATADOR()
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

        public bool insertarEvento(string nombre, DateTime fecha)
        {
            return false;
        }

        public bool insertarPanel(string codEvento, string tipoCafe, TimeSpan hora)
        {
            return false;
        }

        public bool insertarReporte()
        {
            return false;
        }
        private string obtenerAtributosCafes(string tipoCafe)
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
        public Dictionary<string, string> obtenerInformacionCatacion(string codCatacion)
        {
            Dictionary<string, string> catas = new Dictionary<string, string>();

            CATACION catacion = consultarCatacion(codCatacion);
            PANEL panel = consultarPanel(catacion.CODPANEL);
            EVENTO evento = consultarEvento(panel.CODEVENTO);
            CAFE cafe = consultarCafe(catacion.CODCAFE);
            string atributosCafe = obtenerAtributosCafes(panel.TIPOCAFE.ToString());

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
                catas.Add("atributos", atributosCafe);
            }

            return catas;

        }



        public bool registrarCata(CATA cata)
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


        public bool registrarCata(string codCatacion, int rancidez,
            int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal
            , int fragancia, int saborResidual, string observaciones)
        {
            if (!verificarRango(rancidez, dulce, acidez, cuerpo, aroma,
             amargo, impresionGlobal, fragancia, saborResidual))
            {
                return false;
            }

            try
            {
                Catas.Add(new CATA()
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

        public bool registrarCatacion(string codPanel, string codCatador, string codCafe, int cantidad)
        {
            return false;
        }
        private bool verificarRango(int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal, int fragancia, int saborResidual)
        {
            if (rancidez < 0 || dulce < 0 || acidez < 0 || cuerpo < 0 || aroma < 0 || amargo < 0 || impresionGlobal < 0 || fragancia < 0 || saborResidual < 0 ||
                 rancidez > 10 || dulce > 10 || acidez > 10 || cuerpo > 10 || aroma > 10 || amargo > 10 || impresionGlobal > 10 || fragancia > 10 || saborResidual > 10
                )
            {
                return false;
            }
            return true;
        }

        public IList<CATADOR> consultarCatadores()
        {
            throw new NotImplementedException();
        }

        public IList<PANEL> consultarPanelesPorEvento(string codEvento)
        {
            throw new NotImplementedException();
        }

        public bool panelTerminado(string codPanel)
        {
            throw new NotImplementedException();
        }

  
        public byte[] GenerarImagen(string codPanel)
        {
            throw new NotImplementedException();
        }

        public IList<CAFE> obtenerCafesMismoTipoPanel(string codPanel)
        {
            throw new NotImplementedException();
        }

        public string[] getObservaciones(string codPanel)
        {
            throw new NotImplementedException();
        }

        public bool pertenecePanel(string codPanel, string codEvento)
        {
            throw new NotImplementedException();
        }

        public string obtenerValoresDefectoCafes(string tipoCafe)
        {
            throw new NotImplementedException();
        }

        public string getCorreoCatador(string codCatador)
        {
            throw new NotImplementedException();
        }

        public string construirAsuntoCorreo(string codPanel)
        {
            throw new NotImplementedException();
        }

        public string construirMensajeCorreo(List<CATACION> cataciones)
        {
            throw new NotImplementedException();
        }

        public List<CATADOR> getCatadoresInhabilitados()
        {
            throw new NotImplementedException();
        }

        bool Repositorio.habilitarCatador(string codCatador)
        {
            throw new NotImplementedException();
        }
    }

}
