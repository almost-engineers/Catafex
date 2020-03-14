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
    public class EntityFramework : IRepositorio

    {
        /// Variable de Tipo de Base de datos (Entity Framework), el cual nos permite tener una instancia
        /// de nuestra base de datos, esta variable no debe ser accedida por las demas clases
        readonly private CatafexEntities db;
        public EntityFramework()
        {
            this.db = new CatafexEntities();
        }


        /*
         * Recibe como parametros todos los datos  y atributos  nececesarios para la creacion del registro de un cafe  en la base de 
         * datos, estos parametros ya fueron previamente validados 
         */
        public bool InsertarCafe(string nombre, string tipoCafe, string origen, string codEvento, string procedencia, int gradoMolienda, int puntoTueste)
        {
            try
            {
                this.db.CAFE.Add(new CAFE
                {
                    CODCAFE = this.GenerarCodigo("CF"),
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
        public bool InsertarPanel(string codEvento, string tipoCafe, TimeSpan hora)
        {
            try
            {
                this.db.PANEL.Add(new PANEL
                {
                    CODPANEL = this.GenerarCodigo("P"),
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
        public bool InsertarEvento(string nombre, DateTime fecha)
        {
            try
            {
                this.db.EVENTO.Add(new EVENTO
                {
                    CODEVENTO = this.GenerarCodigo("E"),
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
        public bool InsertarReporte()
        {
            return false;
        }

        /*
         *  Reibe como parametros los atributos de Cafe, se obtiene el cafe correspondiente al codigo ingesado por parametro,
         *  y si este codigo coincide los datos son actualizados, finalmente se guardan los cambios en la base de datos
         */
        public bool ActualizarCafe(string codCafe, string nombre, string tipoCafe, string origen, string procedencia, int gradoMolienda, int puntoTueste)
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
        public bool ActualizarPanel(string codigo, string codEvento, string tipoCafe, TimeSpan hora)
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

        public bool ActualizarCatador(string nombre, string cedula, string correo, string contraseña)
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

        public bool ActualizarCatación(string codCatacion, string codCafe, string codPanel, string codCatador, int cantidad)
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
        public bool ActualizarEvento(string codigo, string nombre, DateTime fecha)
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
        public REPORTE BuscarReporte(string codReporte)
        {
            return null;
        }

        /*
         * Este metodo se encarga de  buscar en la base de datos, en la tabla atributos de cafe los 
         * atributos correspondientes del cafe de acuerdo al parametro tipo cafe 
         */
        public ATRIBUTOSCAFE ConsultarAtributosCafe(string tipoCafe)
        {
            return this.db.ATRIBUTOSCAFE.FirstOrDefault(x => x.CAFE.Equals(tipoCafe));
        }
        /*
         * Este  metodo se conecta a la base de datos y me trae una lista de cafes los cuales son aquellos registrados
         * en la base de datos
         */
        public IList<CAFE> ConsultarCafes()
        {
            return this.db.CAFE.ToList();
        }

        /*
         * En este metodo recibimos el tipo de cafe por el cual necesitamos hacer una busqueda, para 
         * recorremos todos los cafes que se encuentran en la base de datos y añadimos a una lista todos aquellos 
         * que concuerden con  el tipo del cafe buscado y luego retornamos esta lista 
         */
        public IList<CAFE> CconsultarCafes(string tipoCafe)
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
        public IList<CATACION> ConsultarCataciones()
        {

            IList<CATACION> catacionesPendientes = new List<CATACION>();

            foreach (CATACION cat in this.db.CATACION.ToList())
            {
                PANEL panel = ObtenerPanel(cat.CODPANEL);
                EVENTO evento = ObtenerEvento(panel.CODEVENTO);


                if (evento.FECHA.CompareTo(DateTime.Today) >= 1 && panel.HORA.CompareTo(DateTime.Now) >= 1)
                {
                    catacionesPendientes.Add(cat);
                }


            }
            return catacionesPendientes;
        }
        private EVENTO ObtenerEvento(string codigo)
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
        private PANEL ObtenerPanel(string codigo)
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
        public IList<CATACION> ConsultarCatacionesAsignadas(string codCatador)
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

        public DateTime ConsultarFecha(string codigo)
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
        public PANEL ConsultarPanel(string codPanel)
        {
            return this.db.PANEL.FirstOrDefault(x => x.CODPANEL.Equals(codPanel));
        }
        /// <summary>
        /// Convierte en una lista todos los paneles de la base de datos y los retorna
        /// </summary>
        /// <returns>Una lista de PANELES</returns>
        public IList<PANEL> ConsultarPaneles()
        {
            return this.db.PANEL.ToList();
        }
        public ADMINISTRADOR ConsultarAdministrador(string correo)
        {
            return this.db.ADMINISTRADOR.FirstOrDefault(x => x.CORREO.Equals(correo));
        }
        public CATADOR ConsultarCatador(string correo)
        {
            return this.db.CATADOR.Where(x => x.CORREO.Equals(correo)).FirstOrDefault();
        }
        public bool ConsultarUsuario(string cedula)
        {
            return false;
        }
        /*
         * Recibe como parametro un codigo de cafe, este codigo es comparado con cada Cafe y su respectivo codigo
         * si coinciden, el cafe sera eliminado de la base da datos. Por ultimo los cambios de la base de datos deben ser aceptados
         * con la la funcion saveChanges
         */
        public bool EliminarCafe(string codigo)
        {
            try
            {
                foreach (CAFE cafe in this.db.CAFE.ToList())
                {
                    if (cafe.CODCAFE.Equals(codigo))
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
        public bool EliminarPanel(string codigo)
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


        public bool EliminarCatador(string cedula)
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


        public int ObtenerUltimaCata(string codCatacion)
        {
            try
            {
                return this.db.CATA.AsEnumerable<CATA>().Last(x => x.CODCATACION.Equals(codCatacion)).VEZCATADA + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public bool RegistrarCatacion(string codPanel, string codCatador, string codCafe, int cantidad)
        {
            try
            {
                this.db.CATACION.Add(new CATACION
                {

                    CODCATACION = GenerarCodigo("CAT"),
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


        public IList<EVENTO> ConsultarEventos()
        {
            return this.db.EVENTO.ToList();
        }
        public bool EliminarEvento(string codEvento)
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

        public EVENTO ConsultarEvento(string codEvento)
        {
            return this.db.EVENTO.FirstOrDefault(x => x.CODEVENTO.Equals(codEvento));
        }


        private string GenerarCodigo(string encabezado)
        {

            string ultimo = "0";
            switch (encabezado)
            {
                case "EV":
                    try
                    {
                        EVENTO ev = this.db.EVENTO.AsEnumerable().Last();
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
                        CATACION ev = this.db.CATACION.AsEnumerable().Last();
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
                        PANEL pa = this.db.PANEL.AsEnumerable().Last();
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
                        CAFE ca = this.db.CAFE.AsEnumerable().Last();
                        string[] cod = ca.CODCAFE.Split('-');
                        ultimo = (int.Parse(cod[1]) + 1).ToString();
                    }
                    catch (Exception)
                    {
                        ultimo = "1";
                    }
                    break;
                case "CAT":
                    int cantidad = this.db.CATACION.AsEnumerable().Count() + 1;
                    ultimo = cantidad.ToString();
                    break;

            }
            return encabezado + '-' + ultimo;

        }

        public string ObtenerAtributosCafes(string tipoCafe)
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

        public string ObtenerValoresDefectoCafes(string tipoCafe)
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
        public CATA ConsultarCata(string codigo)
        {
            return this.db.CATA.FirstOrDefault(x => (x.CODCATACION + "-" + x.VEZCATADA).Equals(codigo));
        }

        public bool InsertarCatador(string nombre, string cedula, string codigo, string correo, string contraseña, string nivelExp)
        {
            try
            {
                this.db.CATADOR.Add(new CATADOR
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

        public bool RegistrarCata(string codCatacion,
            int rancidez, int dulce, int acidez, int cuerpo, int aroma,
            int amargo, int impresionGlobal, int fragancia, int saborResidual,
            string observaciones)
        {
            int vezCatada = ObtenerUltimaCata(codCatacion);
            CATACION catacionAux = this.ConsultarCatacion(codCatacion);


            if (!VerificarRango(rancidez, dulce, acidez, cuerpo, aroma,
             amargo, impresionGlobal, fragancia, saborResidual))
            {
                return false;
            }
            try
            {
                if (catacionAux.CANTIDAD >= 1)
                {
                    this.db.CATA.Add(new CATA
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

                    }); 
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


        public CATADOR BuscarCedulaCatador(string cedula)
        {
            return this.db.CATADOR.FirstOrDefault(x => x.CEDULA.Equals(cedula));
        }

        public Dictionary<string, string> ObtenerInformacionCatacion(string codCatacion)

        {

            Dictionary<string, string> catas = new Dictionary<string, string>();


            CATACION catacion = ConsultarCatacion(codCatacion);
            PANEL panel = ConsultarPanel(catacion.CODPANEL);
            EVENTO evento = ConsultarEvento(panel.CODEVENTO);
            CAFE cafe = ConsultarCafe(catacion.CODCAFE);
            string atributosCafe = ObtenerAtributosCafes(panel.TIPOCAFE);
            string valoresDefectoCafe = ObtenerValoresDefectoCafes(panel.TIPOCAFE);

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

        public CATACION ConsultarCatacion(string codCatacion)
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

        public CAFE ConsultarCafe(string codCafe)
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


        public static bool VerificarRango(int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal, 
                                    int fragancia, int saborResidual)
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
        //--> Desde aca cuarta entrega
        /// <summary>
        /// Este metodo se encarga de listar todos los catadores que existen en la base de datos
        /// </summary>
        /// <returns>una lista con todos los catadores</returns>
        public IList<CATADOR> ConsultarCatadores()
        {
            return this.db.CATADOR.ToList();
        }
        /// <summary>
        /// Este metodo se encarga se encarga de listar todos los paneles que existen en un evento.
        /// </summary>
        /// <param name="codEvento"></param>
        /// <returns>retorna una lista de paneles</returns>
        public IList<PANEL> ConsultarPanelesPorEvento(string codEvento)
        {
            return this.db.PANEL.Where(x => x.CODEVENTO.Equals(codEvento)).ToList();
        }
        /// <summary>
        /// Este metodo se encarga de determinar si un panel esta terminado o no, a partir
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns></returns>
        public bool PanelTerminado(string codPanel)
        {
            List<CATACION> cataciones = this.db.CATACION.Where(x => x.CODPANEL.Equals(codPanel)).ToList();
            if(cataciones.Count() == 0)
            {
                return false;
            }
            return cataciones.Count() == cataciones.Where(x => x.CANTIDAD == 0).Count();
        }
        /// <summary>
        /// Este metodo permite obtener el codigo de un evento al cual se encuentra asociado un panel
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>es codigo de un evento</returns>
        private string GetCodEvento(string codPanel)
        {
            return this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault().CODEVENTO;
        }
        /// <summary>
        /// Este metodo permite obtener todos los cafes cuyo tipo de cafe coincida con el tipo de cafe que tiene un panel, pero que 
        /// dichos cafes existan en el evento en el cual se encuentra el panel
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>retorna lista de CAFE</returns>
        public IList<CAFE> ObtenerCafesMismoTipoPanel(string codPanel)
        {
            string tipoCafe = this.GetTipoCafe(codPanel);
            string codEvento = this.GetCodEvento(codPanel);
            return this.db.CAFE.Where(x => x.TIPOCAFE.Equals(tipoCafe) && x.CODEVENTO.Equals(codEvento)).ToList();
        }
        /// <summary>
        /// Este metodo permite validar si un catador en especifico se encuentra habilitado
        /// </summary>
        /// <param name="codCatador"></param>
        /// <returns>retorna un catador si se encuentra habilitado o null en caso contrario o si el codigo
        /// del catador no existe</returns>
        public CATADOR CatadorHabilitado(string codCatador)
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
        /// <summary>
        /// Este metodo permite almacenar en un diccionario el promedio de catas de un panel, este diccionario
        /// contendra los atributos del cafe que correspondan con el tipo de cafe del panel, y el promedio sera
        /// calculado a partir de todas las catas realizadas para dicho panel
        /// </summary>
        /// <param name="codPanel">El codigo del panel para el cual se reliza el promedio</param>
        /// <returns>Un diccionario con el promedio de catas</returns>
        private Dictionary<string, double> PromedioCatas(string codPanel)
        {
            string[] atri = this.GetAtributosCafe(this.GetTipoCafe(codPanel));
            Dictionary<string, double> promedio = new Dictionary<string, double>();
            int cantidad = this.GetCantidadCatasporPanel(codPanel);
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
        /// <summary>
        /// Este metodo permite obtener los comentarios que se realizaron en todas las catas. Esto con la finalidad de 
        /// que se pueda generar un archivo donde se contengan las observaciones y un grafico
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>un arreglo con las observaciones del panel</returns>
        public string[] GetObservaciones(string codPanel)
        {
            List<CATA> catas = this.ObtenerCatas(codPanel).ToList();
            List<string> comentarios = new List<string>();
            foreach (CATA cata in catas)
            {
                comentarios.Add(cata.OBSERVACIONES);
            }
            return comentarios.ToArray();
        }
        /// <summary>
        /// Este metodo se encarga de obtener todos los valores de una cata, guardando su respectivo valor de la cata
        /// junto con su correspondiente atributo, esto para todas las catas realizadas en un panel
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns></returns>
        private List<Dictionary<string, int>> getValores_AtributosCata(string codPanel)
        {

            List<CATA> catas = this.ObtenerCatas(codPanel).ToList();
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
        /// <summary>
        /// Este metodo permite listar todas las catas que existen en un panel, es decir todas las catas que los diferentes
        /// catadores realizaron
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>una lista de todas las catas que se realizaron para un determinado panel</returns>
        private IList<CATA> ObtenerCatas(string codPanel)
        {
            List<CATACION> cataciones = this.db.CATACION.Where(x => x.CODPANEL.Equals(codPanel)).ToList();
            List<CATA> catas = new List<CATA>();
            foreach (CATACION catacion in cataciones)
            {
                foreach (CATA cata in this.GetCatas(catacion.CODCATACION))
                {
                    catas.Add(cata);
                }
            }
            return catas;
        }
        /// <summary>
        /// Este metodo permite validar que un panel exista, de este modo se garantiza que el usuario no ingrese paneles inexistentes
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>verdadero si el panel existe, falso en caso contrario</returns>
        public bool ExistePanel(string codPanel)
        {
            PANEL panel = this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault();
            return panel == null;
        }
        /// <summary>
        /// Este metodo permite obtener todas las catas que fueron de la misma catacion
        /// </summary>
        /// <param name="codCatacion"></param>
        /// <returns></returns>
        private IList<CATA> GetCatas(string codCatacion)
        {
            return this.db.CATA.Where(x => x.CODCATACION.Equals(codCatacion)).ToList();
        }
        /// <summary>
        /// Este metodo se encarga de obtener la cantidad de catas que 
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns></returns>
        private int GetCantidadCatasporPanel(string codPanel)
        {
            return this.ObtenerCatas(codPanel).Count();
        }
        /// <summary>
        /// Este metodo permite cambiar en la base de datos el estado de unc atdor en especifico, es decir cambiar el valor
        /// del estado de INHABILITADO a HABILITADO
        /// </summary>
        /// <param name="codCatador"></param>
        /// <returns>retorna verdadero si no existieron problemas en la base datos, falso en caso contrario</returns>
        public bool HabilitarCatador(string codCatador)
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
        /// <summary>
        /// este metodo se encarga de listar todos los catadores, cuyo estado sea habilitado
        /// </summary>
        /// <returns>una lista con los catadores inhabilitados</returns>
        public List<CATADOR> ObtenerCatadoresInhabilitados()
        {
            return this.db.CATADOR.Where(x => x.ESTADO.Equals("INHABILITADO")).ToList();
        }
        /// <summary>
        /// este metodo se encarga de listar todos los catadores, cuyo estado sea habilitado
        /// </summary>
        /// <returns>una lista con los catdores habilitados</returns>
        public List<CATADOR> ObtenerCatadoresHabilitados()
        {
            return this.db.CATADOR.Where(x => x.ESTADO.Equals("HABILITADO")).ToList();
        }
        /// <summary>
        /// Este metodo se encarga de listar los valores asociados al patron de un tipo de cafe en especifico, esto con el fin
        /// de presentar al usuario los valores de la regla, ademas de permitir en el grafico visualizar dichos valores
        /// </summary>
        /// <param name="tipoCafe"></param>
        /// <returns>Retorna un arreglo de numeros correspondientes al patron del cafe</returns>
        private double[] GetValoresDefectoCafe(string tipoCafe)
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
        /// <summary>
        /// Este metodo se encarga de listar los atributos que un tipo de cafe posee, es decir, retorna los atributos asociados
        /// como por ejemplo, aroma, amargo, etc.
        /// </summary>
        /// <param name="tipoCafe"></param>
        /// <returns>los atributos del cafe</returns>
        private string[] GetAtributosCafe(string tipoCafe)
        {
            ///char[] charSeparators = new char[] { ';' };
            return this.db.ATRIBUTOSCAFE.Where(x => x.TIPOCAFE.Equals(tipoCafe)).FirstOrDefault().DATOS.Split(';');
        }
        /// <summary>
        /// Este metodo se encarga de obtener el tipo de cafe de un panel, es valido aclarar que existen solo 4 tipos de cafe
        /// entre los cuales se encuentran (VERDE, SOLUBLE, EXTRACTO, TOSTADO)
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>El tipo de cafe del panel</returns>
        private string GetTipoCafe(string codPanel)
        {
            return this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault().TIPOCAFE;
        }

        //--------------------------- Fin calculo promedio de catas ------------------------------------------------

        //---------------------------------- Inicio Generar Imagen -------------------------------------------------
        /// <summary>
        /// Este metodo permite construir un array de bytes, el cual en su interior contendra la informacion de una imagen. 
        /// Dicha imagen a su vez esta conformada por una grafico de estrella generado a partir de el promedio de las cataciones
        /// que se realizaron para ese panel. La imagen obtenida se almacena en memoria volatil y finalmente es transformada en un
        /// array de bytes para ser retornada esa informacion
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>un array de bytes con la imagen del panel</returns>
        public byte[] GenerarImagen(string codPanel)
        {
            const int ANCHO_GRAFICO = 800;
            const string PATRON = "Patron";
            const string PROMEDIO_CATAS = "Promedio Catas";
            Chart grafico = new Chart();
            ChartArea area = new ChartArea();
            area.Visible = true;
            grafico.ChartAreas.Add(area);
            grafico.ChartAreas[0].Area3DStyle.Enable3D = false;
            grafico.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            grafico.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            grafico.Width = ANCHO_GRAFICO;
            grafico.Titles.Add("Grafico del panel: " + codPanel);
            grafico.Series.Add(PATRON);
            grafico.Series[PATRON].LabelToolTip = PATRON;
            grafico.Series.Add(PROMEDIO_CATAS);
            grafico.Series[PATRON].ChartType = SeriesChartType.Radar;
            grafico.Series[PATRON]["RadarDrawingStyle"] = "Line";
            grafico.Series[PATRON]["AreaDrawingStyle"] = "Polygon";
            grafico.Series[PROMEDIO_CATAS].ChartType = SeriesChartType.Radar;
            grafico.Series[PROMEDIO_CATAS]["RadarDrawingStyle"] = "Line";
            grafico.Series[PROMEDIO_CATAS]["AreaDrawingStyle"] = "Polygon";
            grafico.Legends.Add(new Legend(PATRON));
            grafico.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            grafico.Series[PATRON].LegendText = PATRON;
            Dictionary<string, double> promedio = this.PromedioCatas(codPanel);
            double[] valoresDefecto = this.GetValoresDefectoCafe(this.GetTipoCafe(codPanel));
            grafico.Series[PATRON].Points.DataBindXY(promedio.Keys, valoresDefecto);
            grafico.Series[PROMEDIO_CATAS].Points.DataBindXY(promedio.Keys, promedio.Values);
            //-------------------------------------------------------------------------------------
            MemoryStream stream = new MemoryStream();
            grafico.SaveImage(stream, ChartImageFormat.Png);
            BinaryReader binrayRdr = new BinaryReader(stream);
            byte[] info = (stream).ToArray();
            //------------------------------------------------------------------------------------
            return info;
        }
        
        //---------------------------------- Fin Generar Imagen ----------------------------------------------------
        /// <summary>
        /// Este metodo permite validar si un panel pertenece a un determinado evento, esto con la finalidad de que en el frontend
        /// no se puedan asignar paneles a eventos que no corresponden o viceversa
        /// </summary>
        /// <param name="codPanel"></param>
        /// <param name="codEvento"></param>
        /// <returns>verdadero si el panel pertenece a el evento o falso en caso contrario</returns>
        public bool PertenecePanel(string codPanel, string codEvento)
        {
            PANEL panel = this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault();
            return panel.EVENTO.CODEVENTO.Equals(codEvento);
        }

        // -------------------------------- Metodos para enviar correo -----------------------------------------------
        /// <summary>
        /// Este metodo permite obtener el nombre del evento a partir del codigo del panel, dado que el codEvento es una 
        /// llave foranea de la tabla panel
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>el nombre del evento al cual pertenece el panel</returns>
        private string GetNombreEvento(string codPanel)
        {
            PANEL panel = this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault();
            return panel.EVENTO.NOMBRE;
        }
        /// <summary>
        /// Este metodo se encarga de validar 
        /// </summary>
        /// <param name="codCatador"></param>
        /// <returns></returns>
        private string GetNombreCatador(string codCatador)
        {
            return this.db.CATADOR.Where(x => x.CODIGO.Equals(codCatador)).FirstOrDefault().NOMBRE;
        }
        /// <summary>
        /// Este metodo me permite obtener la fecha del evento, a partir de su llave primaria, es decir el codEvento, esta fecha
        /// se contempla el formato (dd/MM/yyyy)
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>retorna la fecha del evento</returns>
        private DateTime GetFechaEvento(string codPanel)
        {
            return this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault().EVENTO.FECHA;
        }
        /// <summary>
        /// Este metodo obtiene la hora en la que un panel esta establecido, a partir de su llave primaria, es decir el codigo del panel
        /// Esta hora contempla el formato (hh/mm/ss)
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>retorna la hora del panel</returns>
        private string GetHoraPanel(String codPanel)
        {
            return this.db.PANEL.Where(x => x.CODPANEL.Equals(codPanel)).FirstOrDefault().HORA.ToString();
        }
        /// <summary>
        /// Este metodo permite obtener el correo de un catador a partir de la llave primaria de la tabla, para este caso
        /// dicha llave primaria es el codigo del catador
        /// </summary>
        /// <param name="codCatador"></param>
        /// <returns></returns>
        public string GetCorreoCatador(string codCatador)
        {
            return this.db.CATADOR.Where(x => x.CODIGO.Equals(codCatador)).FirstOrDefault().CORREO;
        }
        /// <summary>
        /// Este metodo se encarga de construir una cabecera generica para todos los correos, en la cual se incluye 
        /// el nombre del evento al cual fue asignado un catador
        /// </summary>
        /// <param name="codPanel"></param>
        /// <returns>El asunto de un correo</returns>
        public string ConstruirAsuntoCorreo(string codPanel)
        {
            string nombreEvento = this.GetNombreEvento(codPanel);
            string asunto = "Seleccionado como catador para el evento : " + nombreEvento;
            return asunto;
        }
        /// <summary>
        /// Este metodo se encarga de obtener el nombre de un cafe de la base de datos a partir de su llave primaria, es decir, 
        /// el codCafe
        /// </summary>
        /// <param name="codCafe"></param>
        /// <returns>El nombre del cafe</returns>
        private string GetNombreCafe(string codCafe)
        {
            return this.db.CAFE.Where(x => x.CODCAFE.Equals(codCafe)).FirstOrDefault().NOMBRE;
        }
        /// <summary>
        /// Este metodo se encarga de construir un correo a partir de las cataciones, es decir las asignaciones correspondientes que se le 
        /// hacen a un catador, a partir de estos datos se construye una plantilla generica para todos los correos, de manera que el usuario
        /// pueda visualiuzar una informacion clara, y que sepa que es de manera automatica
        /// </summary>
        /// <param name="cataciones"></param>
        /// <returns>Retorna el contenido de un correo electronico, en formato de texto</returns>
        public string ConstruirMensajeCorreo(List<CATACION> cataciones)
        {
            StringBuilder mensaje = new StringBuilder();
            string fecha = this.GetFechaEvento(cataciones.First().CODPANEL).ToString("dd/MM/yyyy");
            mensaje.Append("Señor (a) " + this.GetNombreCatador(cataciones.First().CODCATADOR) + ", usted ha sido seleccionado (a) " +
                "para catar en el evento " + this.GetNombreEvento(cataciones.First().CODPANEL) + ", el dia " + fecha
                 + ". " + "En el panel : " + cataciones.First().CODPANEL + ", a la hora " + this.GetHoraPanel(cataciones.First().CODPANEL) + ", " +
                 "las siguientes muestras de cafe: \n");
            foreach (CATACION catacion in cataciones)
            {
                mensaje.Append("\t Nombre del cafe: " + this.GetNombreCafe(catacion.CODCAFE) + ", Codigo: " + catacion.CODCAFE + "\n");
            }
            mensaje.Append("\n\n\n\n");
            mensaje.Append("\t\t Mensaje generado de manera automatica \n");
            mensaje.Append("\t\t\t Por favor NO RESPONDER \n");
            return mensaje.ToString();
        }
    }
}
