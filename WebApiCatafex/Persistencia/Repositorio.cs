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
        bool insertarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste);
        IList<CAFE> consultarCafes();
        bool eliminarCafe(string codigo);
        bool actualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste);
        string consultarUsuario(string correo, string contrasena);
        bool consultarUsuario(string cedula);
        bool insertarCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp);
        bool eliminarPanel(string codigo);
        bool actualizarPanel(string codigo, string codEvento, string tipoCafe, TimeSpan hora);
        bool insertarPanel(string codEvento, string tipoCafe, TimeSpan hora);
        DateTime consultarFecha(string codigo);
        IList<CAFE>consultarCafes(string tipoCafe);
        IList<string>/*Usuario*/ consultarUsuarios();
        bool registrarCatacion(/*Catacion catacion*/);
        IList<EVENTO> /*Evento*/ consultarEventos();
        string consultarAtributosCafe(string tipoCafe);
        bool eliminarEvento(string codEvento);
        bool actualizarEvento(string codigo, string nombre, DateTime fecha);
        bool insertarEvento(string nombre, DateTime fecha);
        EVENTO consultarEvento(string codEvento);


        bool registrarCata(string codCatacion, int vezCatada ,int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo,
            int impresionGlobal, int fragancia, int saborResidual, string observaciones);

        string obtegerTipoCafe(string codigo);
        CATA consultarCata(string codigo);
        PANEL consultarPanel(string codPanel);
        bool registrarCata(/*Cata*/);

        IList<string>/*Cata*/ consultarCatasAsignadas(string codCatador);
        bool insertarReporte(/*Reporte reporte*/);

        bool buscarCedulaCatador(string cedula);
    }
}
