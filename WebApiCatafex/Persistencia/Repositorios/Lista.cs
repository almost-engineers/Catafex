using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Entity;

namespace Persistencia.Listas
{
    public class Lista : Repositorio
    {
        IList<CATA> Catas;

        public Lista()
        {
            this.Catas = new List<CATA>();
                }
        public bool actualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste)
        {
            return true;
        }

        public bool actualizarCatador(string nombre, string cedula, string correo, string contraseña)
        {
            throw new NotImplementedException();
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

        public IList<CAFE> consultarCafes() => null;


        public IList<CAFE> consultarCafes(string tipoCafe) => null;
        

        public CATA consultarCata(string codigo)
        {
            return null;
        }

        public IList<CATACION> consultarCataciones() => null;


        public IList<CATACION> consultarCatacionesAsignadas(string codCatador) => null;
      
        public CATADOR consultarCatador(string correo)
        {
            return null;
        }

        public EVENTO consultarEvento(string codEvento)
        {
            return null;
        }

        public IList<EVENTO> consultarEventos() => null;

        public DateTime consultarFecha(string codigo)
        {
            return DateTime.Now;
        }

        public PANEL consultarPanel(string codPanel)
        {
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
            throw new NotImplementedException();
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

        public bool insertarCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp)
        {
            return false;
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




        public bool registrarCata(CATA cata) {
            try { 
                Catas.Add(cata);
                return true;
            }
            catch(Exception e) { 
                return false;
            }

        }


        public bool registrarCata(string codCatacion, int rancidez, 
            int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal
            , int fragancia, int saborResidual, string observaciones)
        {

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
            catch (NullReferenceException e) {
                return false;
            }
           
        }

        public bool registrarCatacion(string codCatacion, string codPanel, string codCatador, string codCafe, int cantidad)
        {
            return false;
        }
    }

}
