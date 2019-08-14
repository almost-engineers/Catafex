using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistencia
{
    public  interface Repositorio

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
        IList<string> /*Evento*/ consultarEventos();
        string consultarAtributosCafe(string tipoCafe);
        bool eliminarEvento(string codEvento);
        bool actualizarEvento(/*Evento evento*/);
        bool insertarEvento(/*Evento evento*/);
        string /*Evento*/ consultarEvento(string codEvento);
<<<<<<< HEAD
        string/*Panel*/ consultarPanel(string codPanel);
        bool registrarCata(int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo,
            int impresionGlobal, int fragancia, int saborResidual, string observaciones);
=======
        PANEL consultarPanel(string codPanel);
        bool registrarCata(/*Cata*/);
>>>>>>> 60312c0a0ea7ac579b9bf03bce505bcd46cfa563
        IList<string>/*Cata*/ consultarCatasAsignadas(string codCatador);
        bool insertarReporte(/*Reporte reporte*/);

    }
}
