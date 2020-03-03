using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistencia
{
    public interface Repositorio

    {
        List<CATADOR> getCatadoresHabilitados();
        bool habilitarCatador(string codCatador);
        List<CATADOR> getCatadoresInhabilitados();
        string getCorreoCatador(string codCatador);
        string construirAsuntoCorreo(string codPanel);
        string construirMensajeCorreo(List<CATACION> cataciones);
        bool pertenecePanel(string codPanel, string codEvento);
        byte[] GenerarImagen(string codPanel);
        IList<CAFE> obtenerCafesMismoTipoPanel(string codPanel);
        string[] getObservaciones(string codPanel);
        bool panelTerminado(string codPanel);
        IList<PANEL> consultarPanelesPorEvento(string codEvento);
        IList<PANEL> consultarPaneles();
        IList<REPORTE> consultarReportes();
        IList<CATACION> consultarCataciones();
        REPORTE buscarReporte(string codReporte);
        bool insertarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste);
        IList<CAFE> consultarCafes();
        bool eliminarCafe(string codigo);
        IList<CATACION> consultarCatacionesAsignadas(string codCatador);
        bool actualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste);
        bool consultarUsuario(string cedula);
        bool insertarCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp);
        bool eliminarPanel(string codigo);
        bool eliminarCatador(string cedula);
        bool actualizarCatador(string nombre, string cedula, string correo, string contraseña);
        bool actualizarCatación(string codCatacion, string codCafe, string codPanel, string codCatador, int cantidad);
        bool actualizarPanel(string codigo, string codEvento, string tipoCafe, TimeSpan hora);
        bool insertarPanel(string codEvento, string tipoCafe, TimeSpan hora);
        DateTime consultarFecha(string codigo);
        IList<CAFE> consultarCafes(string tipoCafe);
        bool registrarCatacion(string codPanel, string codCatador, string codCafe, int cantidad);
        IList<EVENTO> /*Evento*/ consultarEventos();
        ATRIBUTOSCAFE consultarAtributosCafe(string tipoCafe);
        bool eliminarEvento(string codEvento);
        bool actualizarEvento(string codigo, string nombre, DateTime fecha);
        bool insertarEvento(string nombre, DateTime fecha);
        EVENTO consultarEvento(string codEvento);

        bool registrarCata(string codCatacion, int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal, int fragancia, int saborResidual, string observaciones);

        CATA consultarCata(string codigo);
        PANEL consultarPanel(string codPanel);
        bool insertarReporte(/*Reporte reporte*/);
        CATADOR buscarCedulaCatador(string cedula);
        ADMINISTRADOR consultarAdministrador(string correo);
        CATADOR consultarCatador(string correo);

        CATACION consultarCatacion(string codCatacion);
        Dictionary<string, string> obtenerInformacionCatacion(string codCatacion);
        CAFE consultarCafe(string codCafe);

        IList<CATADOR> consultarCatadores();

        string obtenerValoresDefectoCafes(string tipoCafe);
    }
}
