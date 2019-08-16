using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistencia
{
    public interface Repositorio

    {
        IList<PANEL> consultarPaneles();
        IList<REPORTE> consultarReportes();
        IList<CATACION> consultarCataciones();
        REPORTE buscarReporte(string codReporte);
        void insertarCafe(/*Cafe cafe*/);
        IList<string>/*<Cafe>*/ consultarCafes();
        bool eliminarCafe(string codigo);
        bool actualizarCafe(/*Cafe cafe*/);
        string consultarUsuario(string correo, string contrasena);
        bool consultarUsuario(string cedula);
        bool insertarCatador(/*Usuario usuario*/);
        bool eliminarPanel(string codigo);
        bool actualizarPanel(string codigo, string codEvento, string tipoCafe, TimeSpan hora);
        bool insertarPanel(string codEvento, string tipoCafe, TimeSpan hora);
        DateTime consultarFecha(string codigo);

        IList<string>/*Cafe*/consultarCafes(string tipoCafe);
        IList<string>/*Usuario*/ consultarUsuarios();
        bool registrarCatacion(/*Catacion catacion*/);
        IList<EVENTO> /*Evento*/ consultarEventos();
        string consultarAtributosCafe(string tipoCafe);
        bool eliminarEvento(string codEvento);
        bool actualizarEvento(string codigo, string nombre, DateTime fecha);
        bool insertarEvento(string nombre, DateTime fecha);
        EVENTO consultarEvento(string codEvento);


        bool registrarCata(string codCatacion, int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo,
            int impresionGlobal, int fragancia, int saborResidual, string observaciones);
        bool registrarCata();
        int obtenerUltimaCata(string codCatacion);

        string obtenerTipoCafe(string codigo);
        CATA consultarCata(string codigo);
        PANEL consultarPanel(string codPanel);
      
        IList<CATACION>consultarCatacionesAsignadas(string codCatador);
        bool insertarReporte(/*Reporte reporte*/);

        string obtenerAtributosCafes(string tipoCafe);

    }
}
