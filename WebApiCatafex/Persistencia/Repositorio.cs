﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistencia
{
<<<<<<< HEAD
    public interface Repositorio
=======
    public  interface Repositorio

>>>>>>> 125decc7897e93f3ee79593f52e6fb49b63a68b5
    {

        IList<string>/*<Panel>*/ consultarPaneles();
        IList<string>/*<Reporte>*/ consultarReportes();
        IList<string>/*<Catacion>*/ consultarCataciones();
        string/*Reporte*/ buscarReporte(string codReporte);
        void insertarCafe(/*Cafe cafe*/);
        IList<string>/*<Cafe>*/ consultarCafes();
        bool eliminarCafe(string codigo);
        bool actualizarCafe(/*Cafe cafe*/);
        string consultarUsuario(string correo, string contrasena);
        bool consultarUsuario(string cedula);
        bool insertarCatador(/*Usuario usuario*/);
        bool eliminarPanel(string codigo);
        bool actualizarPanel(/*Panel panel*/);
        bool insertarPanel(/*Panel panel*/);
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
        string/*Panel*/ consultarPanel(string codPanel);
        bool registrarCata(/*Cata*/);
        IList<string>/*Cata*/ consultarCatasAsignadas(string codCatador);
        bool insertarReporte(/*Reporte reporte*/);

    }
}
