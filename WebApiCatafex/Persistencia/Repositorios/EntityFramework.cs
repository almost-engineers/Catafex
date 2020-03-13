using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.IO;

namespace Persistencia.Repositorios
{
    public class EntityFramework : Repositorio

    {
        /// Variable de Tipo de Base de datos (Entity Framework), el cual nos permite tener una instancia
        /// de nuestra base de datos, esta variable no debe ser accedida por las demas clases
        private CatafexEntities db;
        public EntityFramework()
        {
            this.db = new CatafexEntities();
        }


        /*
         * Recibe como parametros todos los datos  y atributos  nececesarios para la creacion del registro de un cafe  en la base de 
         * datos, estos parametros ya fueron previamente validados 
         */
        public bool insertarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste)
        {
            try
            {
                this.db.CAFE.Add(new CAFE()
                {
                    CODCAFE = this.generarCodigo("CF"),
                    CODEVENTO = codEvento,
                    TIPOCAFE = tipoCafe,
                    NOMBRE = nombre,
                    ORIGEN = origen,
                    PROCEDENCIA = procedencia,
                    GRADOMOLIENDA = gradoMolienda,
                    PUNTOTUESTE = puntoTueste
                });
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool insertarCatador()
        {
            return false;
        }
        /// <summary>
        /// Recibe como parametros los atributos necesarios para la creacion de un panel, en estos no se incluye el codigo del panel
        /// dado que este se genera a partir de un metodo denominado generarCodigoPanel, que en su version 1.0 solo retorna 1.
        /// Añade a la lista de paneles(esta lista es propia de la base de datos, y no tiene relacion con la clase lista), un nuevo panel
        /// con los datos ingresados por parametro. Por ultimo se aceptan los cambios con la funcion SaveChanges
        /// </summary>
        /// <param name="codEvento"></param>
        /// <param name="tipoCafe"></param>
        /// <param name="hora"></param>
        /// <returns>Verdadero o Falso dependiendo de si sucede o no una excepcion, en caso de no suceder una excepcion retorna true,
        /// por lo contrario retorna false</returns>
        public bool insertarPanel(string codEvento, string tipoCafe, TimeSpan hora)
        {
            try
            {
                this.db.PANEL.Add(new PANEL()
                {
                    CODPANEL = this.generarCodigo("P"),
                    CODEVENTO = codEvento,
                    TIPOCAFE = tipoCafe,
                    HORA = hora
                });
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool insertarEvento(string nombre, DateTime fecha)
        {
            try
            {
                this.db.EVENTO.Add(new EVENTO()
                {
                    CODEVENTO = this.generarCodigo("E"),
                    NOMBRE = nombre,
                    FECHA = fecha
                });
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool insertarReporte()
        {
            return false;
        }

        /*
         *  Reibe como parametros los atributos de Cafe, se obtiene el cafe correspondiente al codigo ingesado por parametro,
         *  y si este codigo coincide los datos son actualizados, finalmente se guardan los cambios en la base de datos
         */
        public bool actualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste)
        {
            try
            {
                foreach (CAFE cafe in this.db.CAFE.ToList())
                {
                    if (cafe.CODCAFE.Equals(codCafe))
                    {
                        cafe.CODCAFE = codCafe;
                        cafe.NOMBRE = nombre;
                        cafe.TIPOCAFE = tipoCafe;
                        cafe.ORIGEN = origen;
                        cafe.PROCEDENCIA = procedencia;
                        cafe.GRADOMOLIENDA = gradoMolienda;
                        cafe.PUNTOTUESTE = puntoTueste;
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Reibe como parametros los atributos de Panel, se obtiene el panel correspondiente al codigo ingesado por parametro,
        /// y si este codigo coincide los datos son actualizados, finalmente se guardan los cambios en la base de datos
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="codEvento"></param>
        /// <param name="tipoCafe"></param>
        /// <param name="hora"></param>
        /// <returns>erdadero o falso dependiendo del exito de la operacion de actualizacion</returns>
        public bool actualizarPanel(string codigo, string codEvento, string tipoCafe, TimeSpan hora)
        {
            try
            {
                foreach (PANEL panel in this.db.PANEL.ToList())
                {
                    if (panel.CODPANEL.Equals(codigo))
                    {
                        panel.CODEVENTO = codEvento;
                        panel.TIPOCAFE = tipoCafe;
                        panel.HORA = hora;
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool actualizarCatador(string nombre, string cedula, string correo, string contraseña)
        {
            try
            {
                foreach (CATADOR catador in this.db.CATADOR.ToList())
                {
                    if (catador.CEDULA.Equals(cedula))
                    {
                        catador.NOMBRE = nombre;
                        catador.CORREO = correo;
                        catador.CONTRASEÑA = contraseña;
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool actualizarCatación(string codCatacion, string codCafe, string codPanel, string codCatador, int cantidad)
        {
            try
            {
                foreach (CATACION catacion in this.db.CATACION.ToList())
                {
                    if (catacion.CODCATACION.Equals(codCatacion))
                    {
                        catacion.CANTIDAD = cantidad;
                        catacion.CODCAFE = codCafe;
                        catacion.CODCATADOR = codCatador;
                        catacion.CODPANEL = codPanel;
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool actualizarEvento(string codigo, string nombre, DateTime fecha)
        {
            try
            {
                foreach (EVENTO evento in this.db.EVENTO.ToList())
                {
                    if (evento.CODEVENTO.Equals(codigo))
                    {
                        evento.NOMBRE = nombre;
                        evento.FECHA = fecha;
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public REPORTE buscarReporte(string codReporte)
        {
            return null;
        }

        /*
         * Este metodo se encarga de  buscar en la base de datos, en la tabla atributos de cafe los 
         * atributos correspondientes del cafe de acuerdo al parametro tipo cafe 
         */
        public ATRIBUTOSCAFE consultarAtributosCafe(string tipoCafe)
        {
            return this.db.ATRIBUTOSCAFE.FirstOrDefault(x => x.CAFE.Equals(tipoCafe));
        }
        /*
         * Este  metodo se conecta a la base de datos y me trae una lista de cafes los cuales son aquellos registrados
         * en la base de datos
         */
        public IList<CAFE> consultarCafes()
        {
            return this.db.CAFE.ToList();
        }

        /*
         * En este metodo recibimos el tipo de cafe por el cual necesitamos hacer una busqueda, para 
         * recorremos todos los cafes que se encuentran en la base de datos y añadimos a una lista todos aquellos 
         * que concuerden con  el tipo del cafe buscado y luego retornamos esta lista 
         */
        public IList<CAFE> consultarCafes(string tipoCafe)
        {
            IList<CAFE> cafesTipo = new List<CAFE>();
            foreach (CAFE cafe in this.db.CAFE.ToList())
            {
                if (cafe.ATRIBUTOSCAFE.TIPOCAFE.Equals(tipoCafe))
                {
                    cafesTipo.Add(cafe);
                }
            }
            return cafesTipo;
        }
        public IList<CATACION> consultarCataciones()
        {

            IList<CATACION> catacionesPendientes = new List<CATACION>();

            foreach (CATACION cat in this.db.CATACION.ToList())
            {
                PANEL panel = obtenerPanel(cat.CODPANEL);
                EVENTO evento = obtenerEvento(panel.CODEVENTO);


                if (evento.FECHA.CompareTo(DateTime.Today) >= 1 && panel.HORA.CompareTo(DateTime.Now) >= 1)
                {
                    catacionesPendientes.Add(cat);
                }


            }
            return catacionesPendientes;
        }
        private EVENTO obtenerEvento(string codigo)
        {
            foreach (EVENTO evento in this.db.EVENTO.ToList())
            {
                if (evento.CODEVENTO.Equals(codigo))
                {
                    return evento;
                }
            }
            return null;
        }
        private PANEL obtenerPanel(string codigo)
        {
            foreach (PANEL panel in this.db.PANEL.ToList())
            {
                if (panel.CODPANEL.Equals(codigo))
                {
                    return panel;
                }
            }
            return null;
        }
        public IList<CATACION> consultarCatacionesAsignadas(string codCatador)
        {

            IList<CATACION> catacionesPendientes = new List<CATACION>();

            foreach (CATACION cat in this.db.CATACION.ToList())
            {
                if (cat.CODCATADOR.Equals(codCatador) && cat.CANTIDAD > 0)
                {
                    catacionesPendientes.Add(cat);
                }
            }
            return catacionesPendientes;
        }

        public DateTime consultarFecha(string codigo)
        {
            return DateTime.Now;
        }
        /// <summary>
        /// Recibe como parametro el codigo del panel, las referencias de llamados del metodo se encuentran autoreferenciadas
        /// gracias a las facilidades que ofrece visual studio. Por medio de LinQ se realiza una consulta a la base de datos en la cual
        /// se obtiene el objeto el cual coincida con el codigo de Panel ingresado por parametro
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>retorna un PANEL (objeto de la base de datos)</returns>
        public PANEL consultarPanel(string codPanel)
        {
            return this.db.PANEL.FirstOrDefault(x => x.CODPANEL.Equals(codPanel));
        }
        /// <summary>
        /// Convierte en una lista todos los paneles de la base de datos y los retorna
        /// </summary>
        /// <returns>Una lista de PANELES</returns>
        public IList<PANEL> consultarPaneles()
        {
            return this.db.PANEL.ToList();
        }
        public IList<REPORTE> consultarReportes()
        {
            return null;
        }
        public ADMINISTRADOR consultarAdministrador(string correo)
        {
            return this.db.ADMINISTRADOR.FirstOrDefault(x => x.CORREO.Equals(correo));
        }
        public CATADOR consultarCatador(string correo)
        {
            return this.db.CATADOR.Where(x => x.CORREO.Equals(correo)).FirstOrDefault();
        }
        public bool consultarUsuario(string cedula)
        {
            return false;
        }
        /*
         * Recibe como parametro un codigo de cafe, este codigo es comparado con cada Cafe y su respectivo codigo
         * si coinciden, el cafe sera eliminado de la base da datos. Por ultimo los cambios de la base de datos deben ser aceptados
         * con la la funcion saveChanges
         */
        public bool eliminarCafe(string codCafe)
        {
            try
            {
                foreach (CAFE cafe in this.db.CAFE.ToList())
                {
                    if (cafe.CODCAFE.Equals(codCafe))
                    {
                        this.db.CAFE.Remove(cafe);
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Recibe como parametro un codigo de panel, este codigo es comparado con cada Panel y su respectivo codigo
        /// si coinciden, el panel sera eliminado de la base da datos. Por ultimo los cambios de la base de datos deben ser aceptados
        /// con la la funcion saveChanges
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>erdadero o Falso dependiendo del estado de eliminacion del panel</returns>
        public bool eliminarPanel(string codigo)
        {
            try
            {
                foreach (PANEL panel in this.db.PANEL.ToList())
                {
                    if (panel.CODPANEL.Equals(codigo))
                    {
                        this.db.PANEL.Remove(panel);
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool eliminarCatador(string cedula)
        {
            try
            {
                foreach (CATADOR catador in this.db.CATADOR.ToList())
                {
                    if (catador.CEDULA.Equals(cedula))
                    {
                        this.db.CATADOR.Remove(catador);
                    }
                }
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public int obtenerUltimaCata(string codCatacion)
        {
            try
            {
                return this.db.CATA.ToList<CATA>().Last(x => x.CODCATACION.Equals(codCatacion)).VEZCATADA + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public bool registrarCatacion(string codPanel, string codCatador, string codCafe, int cantidad)
        {
            try
            {
                this.db.CATACION.Add(new CATACION()
                {

                    CODCATACION = generarCodigo("CAT"),
                    CODPANEL = codPanel,
                    CODCATADOR = codCatador,
                    CODCAFE = codCafe,
                    CANTIDAD = cantidad
                });
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

       
        public IList<EVENTO> consultarEventos()
        {
            return this.db.EVENTO.ToList();
        }
        public bool eliminarEvento(string codEvento)
        {
            foreach (EVENTO e in this.db.EVENTO.ToList())
            {
                if (e.CODEVENTO.Equals(codEvento))
                {
                    return true;
                }
            }
            return false;
        }

        public EVENTO consultarEvento(string codEvento)
        {
            return this.db.EVENTO.FirstOrDefault(x => x.CODEVENTO.Equals(codEvento));
        }

       
        private string generarCodigo(string encabezado)
        {

            string ultimo = "0";
            switch (encabezado)
            {
                case "EV":
                    try
                    {
                        EVENTO ev = this.db.EVENTO.ToList().Last();
                        string[] cod = ev.CODEVENTO.Split('-');
                        ultimo = (int.Parse(cod[1]) + 1).ToString();
                    }
                    catch (Exception)
                    {
                        ultimo = "1";
                    }
                    break;
                case "CT":
                    try
                    {
                        CATACION ev = this.db.CATACION.ToList().Last();
                        string[] cod = ev.CODCATACION.Split('-');
                        ultimo = (int.Parse(cod[1]) + 1).ToString();
                    }
                    catch (Exception)
                    {
                        ultimo = "1";
                    }
                    break;
                case "PA":
                    try
                    {
                        PANEL pa = this.db.PANEL.ToList().Last();
                        string[] cod = pa.CODPANEL.Split('-');
                        ultimo = (int.Parse(cod[1]) + 1).ToString();
                    }
                    catch (Exception)
                    {
                        ultimo = "1";
                    }
                    break;
                case "CF":
                    try
                    {
                        CAFE ca = this.db.CAFE.ToList().Last();
                        string[] cod = ca.CODCAFE.Split('-');
                        ultimo = (int.Parse(cod[1]) + 1).ToString();
                    }
                    catch (Exception)
                    {
                        ultimo = "1";
                    }
                    break;
                case "CAT":
                    int cantidad = this.db.CATACION.ToList().Count() + 1;
                    ultimo = cantidad.ToString();
                    break;

            }
            return encabezado + '-' + ultimo;

        }

        public string obtenerTipoCafe(string codigo)
        {
            foreach (CATACION cat in this.db.CATACION.ToList())
            {
                if (cat.CODCATACION.Equals(codigo))
                    return this.consultarPanel(cat.CODPANEL).TIPOCAFE;
            }
            return "no se encuentra";
        }

        public string obtenerAtributosCafes(string tipoCafe)
        {
            try
            {

                return this.db.ATRIBUTOSCAFE.FirstOrDefault(x => x.TIPOCAFE.Equals(tipoCafe)).DATOS;
            }
            //comentario
            catch (Exception)
            {
                return "no existen datos para ese tipo de cafe";
            }
        }

        public string obtenerValoresDefectoCafes(string tipoCafe)
        {
            try
            {

                return this.db.ATRIBUTOSCAFE.FirstOrDefault(x => x.TIPOCAFE.Equals(tipoCafe)).VALOR_DEFECTO;
            }
            //comentario
            catch (Exception)
            {
                return "no existen datos para ese tipo de cafe";
            }
        }
        public CATA consultarCata(string codigo)
        {
            return this.db.CATA.FirstOrDefault(x => (x.CODCATACION + "-" + x.VEZCATADA).Equals(codigo));
        }

        public bool insertarCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp)
        {
            try
            {
                this.db.CATADOR.Add(new CATADOR()
                {
                    NOMBRE = nombre,
                    CEDULA = cedula,
                    CODIGO = codigo,
                    CORREO = correo,
                    CONTRASEÑA = contraseña,
                    NIVELEXP = nivelExp
                });
                this.db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool registrarCata(string codCatacion,
            int rancidez, int dulce, int acidez, int cuerpo, int aroma,
            int amargo, int impresionGlobal, int fragancia, int saborResidual,
            string observaciones)
        {
            int vezCatada = obtenerUltimaCata(codCatacion);
            CATACION catacionAux = this.consultarCatacion(codCatacion);


            if (!verificarRango(rancidez, dulce, acidez, cuerpo, aroma,
             amargo, impresionGlobal, fragancia, saborResidual))
            {
                return false;
            }
            try
            {
                if (catacionAux.CANTIDAD >= 1)
                {
                    this.db.CATA.Add(new CATA()
                    {

                        CODCATACION = codCatacion,
                        VEZCATADA = vezCatada,
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

                    }); ;
                    this.db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }


        public CATADOR buscarCedulaCatador(string cedula)
        {
            return this.db.CATADOR.FirstOrDefault(x => x.CEDULA.Equals(cedula));
        }

        public Dictionary<string, string> obtenerInformacionCatacion(string codCatacion)

        {

            Dictionary<string, string> catas = new Dictionary<string, string>();


            CATACION catacion = consultarCatacion(codCatacion);
            PANEL panel = consultarPanel(catacion.CODPANEL);
            EVENTO evento = consultarEvento(panel.CODEVENTO);
            CAFE cafe = consultarCafe(catacion.CODCAFE);
            string atributosCafe = obtenerAtributosCafes(panel.TIPOCAFE.ToString());
            string valoresDefectoCafe = obtenerValoresDefectoCafes(panel.TIPOCAFE.ToString());

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
                catas.Add("valoresDefecto", valoresDefectoCafe);
            }

            return catas;
        }

        public CATACION consultarCatacion(string codCatacion)
        {
            try
            {
                return this.db.CATACION.FirstOrDefault(x => x.CODCATACION.Equals(codCatacion));
            }
            catch (Exception)
            {
                return null;
            }

        }

        public CAFE consultarCafe(string codCafe)
        {
            try
            {
                return this.db.CAFE.FirstOrDefault(x => x.CODCAFE.Equals(codCafe));
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool verificarRango(int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal, int fragancia, int saborResidual)
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
            return this.db.CATADOR.ToList();
        }

        public IList<PANEL> consultarPanelesPorEvento(string codEvento)
        {
            return this.db.PANEL.Where(x => x.CODEVENTO.Equals(codEvento)).ToList();
        }

        public bool panelTerminado(string codPanel)
        {
            List<CATACION> cataciones = this.db.CATACION.Where(x => x.CODPANEL.Equals(codPanel)).ToList();
            return cataciones.Count() == cataciones.Where(x => x.CANTIDAD == 0).Count();
        }

        private string getCodEvento(string codPanel)
        {
            return this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault().CODEVENTO.ToString();
        }

        public IList<CAFE> obtenerCafesMismoTipoPanel(string codPanel)
        {
            string tipoCafe = this.getTipoCafe(codPanel);
            string codEvento = this.getCodEvento(codPanel);
            return this.db.CAFE.Where(x => x.TIPOCAFE.Equals(tipoCafe) && x.CODEVENTO.Equals(codEvento)).ToList();
        }

        public CATADOR catadorHabilitado(string codCatador)
        {
            CATADOR catador = this.db.CATADOR.Where(x => x.CODIGO.Equals(codCatador)).FirstOrDefault();

            if (catador != null)
            {
                if (catador.ESTADO.Equals("HABILITADO"))
                {
                    return catador;
                }
                return null;
            }
            return null;


        }
        //------------------ Calcular Promedio de catas -------------------------------------------------------

        private Dictionary<string, double> promedioCatas(string codPanel)
        {
            string[] atri = this.getAtributosCafe(this.getTipoCafe(codPanel));
            Dictionary<string, double> promedio = new Dictionary<string, double>();
            int cantidad = this.getCantidadCatasporPanel(codPanel);
            foreach (string str in atri)
            {
                promedio.Add(str, 0);
            }
            List<Dictionary<string, int>> valoresCata = getValores_AtributosCata(codPanel);
            foreach (Dictionary<string, int> datos in valoresCata)
            {
                foreach (string atributos in datos.Keys)
                {
                    foreach (string atr in atri)
                    {
                        if (atr.Equals(atributos))
                        {
                            promedio[atr] += datos[atributos];
                        }
                    }
                }

            }
            foreach (string prom in atri)
            {
                promedio[prom] = promedio[prom] / cantidad;
            }
            return promedio;
        }

        public string[] getObservaciones(string codPanel)
        {
            List<CATA> catas = this.obtenerCatas(codPanel).ToList();
            List<string> comentarios = new List<string>(); 
            int i = 0;
            foreach (CATA cata in catas)
            {
                if (!cata.OBSERVACIONES.Equals("null"))
                {
                    comentarios.Add((cata.OBSERVACIONES + "\n").ToString());
                }
            }
            string[] observaciones = new string[comentarios.Count];
            foreach(string comentario in observaciones)
            {
                observaciones[i] = comentario;
                i++;
            }
            return observaciones;
        }

        private List<Dictionary<string, int>> getValores_AtributosCata(string codPanel)
        {

            List<CATA> catas = this.obtenerCatas(codPanel).ToList();
            List<Dictionary<string, int>> datosFinales = new List<Dictionary<string, int>>();
            foreach (CATA cata in catas)
            {
                Dictionary<string, int> datos = new Dictionary<string, int>();
                datos.Add("RANCIDEZ", cata.RANCIDEZ.Value);
                datos.Add("DULCE", cata.DULCE.Value);
                datos.Add("ACIDEZ", cata.ACIDEZ.Value);
                datos.Add("AROMA", cata.AROMA.Value);
                datos.Add("AMARGO", cata.AMARGO.Value);
                datos.Add("FRAGANCIA", cata.FRAGANCIA.Value);
                datos.Add("SABOR_RESIDUAL", cata.SABORESIDUAL.Value);
                datos.Add("CUERPO", cata.CUERPO.Value);
                datos.Add("IMPRESION_GLOBAL", cata.IMPRESIONGLOBAL.Value);
                datosFinales.Add(datos);
            }
            return datosFinales;
        }


        private IList<CATA> obtenerCatas(string codPanel)
        {
            List<CATACION> cataciones = this.db.CATACION.Where(x => x.CODPANEL.Equals(codPanel)).ToList();
            List<CATA> catas = new List<CATA>();
            foreach (CATACION catacion in cataciones)
            {
                foreach (CATA cata in this.getCatas(catacion.CODCATACION))
                {
                    catas.Add(cata);
                }
            }
            return catas;
        }
        public bool existePanel(string codPanel)
        {
            PANEL panel = this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault();
            return panel == null;
        }
        private IList<CATA> getCatas(string codCatacion)
        {
            return this.db.CATA.Where(x => x.CODCATACION.Equals(codCatacion)).ToList();
        }

        private int getCantidadCatasporPanel(string codPanel)
        {
            return this.obtenerCatas(codPanel).Count();
        }
        public bool habilitarCatador(string codCatador)
        {
            try
            {
                this.db.CATADOR.Where(x => x.CODIGO.Equals(codCatador)).FirstOrDefault().ESTADO = "HABILITADO";
                this.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<CATADOR> getCatadoresInhabilitados()
        {
            return this.db.CATADOR.Where(x => x.ESTADO.Equals("INHABILITADO")).ToList();
        }
        public List<CATADOR> getCatadoresHabilitados()
        {
            return this.db.CATADOR.Where(x => x.ESTADO.Equals("HABILITADO")).ToList();
        }
        private double[] getValoresDefectoCafe(string tipoCafe)
        {
            string[] defecto = this.db.ATRIBUTOSCAFE.Where(x => x.TIPOCAFE.Equals(tipoCafe)).FirstOrDefault().VALOR_DEFECTO.Split(';');
            double[] valores = new double[defecto.Length];
            int i = 0;
            foreach (string defect in defecto)
            {
                valores[i] = Double.Parse(defect);
                i++;
            }
            return valores;
        }

        private string[] getAtributosCafe(string tipoCafe)
        {
            char[] charSeparators = new char[] { ';' };
            return this.db.ATRIBUTOSCAFE.Where(x => x.TIPOCAFE.Equals(tipoCafe)).FirstOrDefault().DATOS.Split(';');
        }

        private string getTipoCafe(string codPanel)
        {
            return this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault().TIPOCAFE.ToString();
        }

        //--------------------------- Fin calculo promedio de catas ------------------------------------------------

        //---------------------------------- Inicio Generar Imagen -------------------------------------------------
        public byte[] GenerarImagen(string codPanel)
        {
            Chart grafico = new Chart();
            ChartArea area = new ChartArea();
            area.Visible = true;
            grafico.ChartAreas.Add(area);
            grafico.ChartAreas[0].Area3DStyle.Enable3D = false;
            grafico.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            grafico.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            grafico.Width = 800;
            grafico.Titles.Add("Grafico del panel: " + codPanel);
            grafico.Series.Add("Patron");
            grafico.Series["Patron"].LabelToolTip = "Patron";
            grafico.Series.Add("Promedio Catas");
            grafico.Series["Patron"].ChartType = SeriesChartType.Radar;
            grafico.Series["Patron"]["RadarDrawingStyle"] = "Line";
            grafico.Series["Patron"]["AreaDrawingStyle"] = "Polygon";
            grafico.Series["Promedio Catas"].ChartType = SeriesChartType.Radar;
            grafico.Series["Promedio Catas"]["RadarDrawingStyle"] = "Line";
            grafico.Series["Promedio Catas"]["AreaDrawingStyle"] = "Polygon";
            grafico.Legends.Add(new Legend("Patron"));
            grafico.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            grafico.Series["Patron"].LegendText = "Patron";
            Dictionary<string, double> promedio = this.promedioCatas(codPanel);
            double[] valoresDefecto = this.getValoresDefectoCafe(this.getTipoCafe(codPanel));
            grafico.Series["Patron"].Points.DataBindXY(promedio.Keys, valoresDefecto);
            grafico.Series["Promedio Catas"].Points.DataBindXY(promedio.Keys, promedio.Values);
            //-------------------------------------------------------------------------------------
            MemoryStream stream = new MemoryStream();
            grafico.SaveImage(stream, ChartImageFormat.Png);
            BinaryReader binrayRdr = new BinaryReader(stream);
            byte[] info = ((MemoryStream)stream).ToArray();
            //------------------------------------------------------------------------------------
            return info;
        }

        //---------------------------------- Fin Generar Imagen ----------------------------------------------------

        public bool pertenecePanel(string codPanel, string codEvento)
        {
            PANEL panel = this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault();
            return panel.EVENTO.CODEVENTO.Equals(codEvento);
        }

        // -------------------------------- Metodos para enviar correo -----------------------------------------------

        private string getNombreEvento(string codPanel)
        {
            PANEL panel = this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault();
            return panel.EVENTO.NOMBRE.ToString();
        }

        private string getNombreCatador(string codCatador)
        {
            return this.db.CATADOR.Where(x => x.CODIGO.Equals(codCatador)).FirstOrDefault().NOMBRE.ToString();
        }

        private DateTime getFechaEvento(string codPanel)
        {
            return this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault().EVENTO.FECHA;
        }
        private string getHoraPanel(String codPanel)
        {
            return this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault().HORA.ToString();
        }
        public string getCorreoCatador(string codCatador)
        {
            return this.db.CATADOR.Where(x => x.CODIGO.Equals(codCatador)).FirstOrDefault().CORREO.ToString();
        }
        public string construirAsuntoCorreo(string codPanel)
        {
            string nombreEvento = this.getNombreEvento(codPanel);
            string asunto = "Seleccionado como catador para el evento : " + nombreEvento;
            return asunto;
        }
        private string getNombreCafe(string codCafe)
        {
            return this.db.CAFE.Where(x => x.CODCAFE.Equals(codCafe)).FirstOrDefault().NOMBRE.ToString();
        }
        public string construirMensajeCorreo(List<CATACION> cataciones)
        {
            StringBuilder mensaje = new StringBuilder();
            string fecha = this.getFechaEvento(cataciones.First().CODPANEL).ToString("dd/MM/yyyy");
            mensaje.Append("Señor (a) " + this.getNombreCatador(cataciones.First().CODCATADOR) + ", usted ha sido seleccionado (a) " +
                "para catar en el evento " + this.getNombreEvento(cataciones.First().CODPANEL) + ", el dia " + fecha
                 + ". " + "En el panel : " + cataciones.First().CODPANEL + ", a la hora " + this.getHoraPanel(cataciones.First().CODPANEL) + ", " +
                 "las siguientes muestras de cafe: \n");
            foreach (CATACION catacion in cataciones)
            {
                mensaje.Append("\t Nombre del cafe: " + this.getNombreCafe(catacion.CODCAFE) + ", Codigo: " + catacion.CODCAFE + "\n");
            }
            mensaje.Append("\n\n\n\n");
            mensaje.Append("\t\t Mensaje generado de manera automatica \n");
            mensaje.Append("\t\t\t Por favor NO RESPONDER \n");
            return mensaje.ToString();
        }
    }
}
