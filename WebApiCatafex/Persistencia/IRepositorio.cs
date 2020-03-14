using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistencia
{
    public interface IRepositorio

    {
        bool ExistePanel(string codPanel);
        CATADOR CatadorHabilitado(string codCatador);
        List<CATADOR> ObtenerCatadoresHabilitados();
        bool HabilitarCatador(string codCatador);
        List<CATADOR> ObtenerCatadoresInhabilitados();
        string GetCorreoCatador(string codCatador);
        string ConstruirAsuntoCorreo(string codPanel);
        string ConstruirMensajeCorreo(List<CATACION> cataciones);
        bool PertenecePanel(string codPanel, string codEvento);
        byte[] GenerarImagen(string codPanel);
        IList<CAFE> ObtenerCafesMismoTipoPanel(string codPanel);
        string[] GetObservaciones(string codPanel);
        bool PanelTerminado(string codPanel);
        IList<PANEL> ConsultarPanelesPorEvento(string codEvento);
        IList<PANEL> ConsultarPaneles();
        IList<CATACION> ConsultarCataciones();
        REPORTE BuscarReporte(string codReporte);
        bool InsertarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste);
        IList<CAFE> ConsultarCafes();
        bool EliminarCafe(string codigo);
        IList<CATACION> ConsultarCatacionesAsignadas(string codCatador);
        bool ActualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste);
        bool ConsultarUsuario(string cedula);
        bool InsertarCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp);
        bool EliminarPanel(string codigo);
        bool EliminarCatador(string cedula);
        bool ActualizarCatador(string nombre, string cedula, string correo, string contraseña);
        bool ActualizarCatación(string codCatacion, string codCafe, string codPanel, string codCatador, int cantidad);
        bool ActualizarPanel(string codigo, string codEvento, string tipoCafe, TimeSpan hora);
        bool InsertarPanel(string codEvento, string tipoCafe, TimeSpan hora);
        DateTime ConsultarFecha(string codigo);
        IList<CAFE> CconsultarCafes(string tipoCafe);
        bool RegistrarCatacion(string codPanel, string codCatador, string codCafe, int cantidad);
        IList<EVENTO> /*Evento*/ ConsultarEventos();
        ATRIBUTOSCAFE ConsultarAtributosCafe(string tipoCafe);
        bool EliminarEvento(string codEvento);
        bool ActualizarEvento(string codigo, string nombre, DateTime fecha);
        bool InsertarEvento(string nombre, DateTime fecha);
        EVENTO ConsultarEvento(string codEvento);

        bool RegistrarCata(string codCatacion, int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal, int fragancia, int saborResidual, string observaciones);

        CATA ConsultarCata(string codigo);
        PANEL ConsultarPanel(string codPanel);
        bool InsertarReporte(/*Reporte reporte*/);
        CATADOR BuscarCedulaCatador(string cedula);
        ADMINISTRADOR ConsultarAdministrador(string correo);
        CATADOR ConsultarCatador(string correo);

        CATACION ConsultarCatacion(string codCatacion);
        Dictionary<string, string> ObtenerInformacionCatacion(string codCatacion);
        CAFE ConsultarCafe(string codCafe);

        IList<CATADOR> ConsultarCatadores();

        string ObtenerValoresDefectoCafes(string tipoCafe);
    }
}
