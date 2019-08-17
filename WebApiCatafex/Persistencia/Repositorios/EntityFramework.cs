using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistencia.Entity;

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

        public void insertarCafe()
        {

        }
        /// <summary>
        /// Recibe como parametro todos los datos necesarios, que ya han sido previamente validados, y se disponen a ser insertados en la base de datos
        /// En esta parte no es necesario validar que el usuario no se encuentre registrado, dado que esta validacion es realizada por la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="cedula"></param>
        /// <param name="codigo"></param>
        /// <param name="correo"></param>
        /// <param name="contraseña"></param>
        /// <param name="nivelExp"></param>
        /// <returns>Retorna Verdadero si los cambios en la base de datos son logrados con exito de lo contrario retorna False</returns>
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
                    CODPANEL = this.generarCodigo("PA"),
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

        public bool actualizarCafe()
        {
            return false;
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
        public string consultarAtributosCafe(string tipoCafe)
        {
            return null;
        }
        public IList<string> consultarCafes()
        {
            return null;
        }
        public IList<string> consultarCafes(string tipoCafe)
        {
            return null;
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
        public IList<string> consultarCatasAsignadas(string codCatador)
        {
            return null;
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
            return this.db.CATADOR.FirstOrDefault(x => x.CORREO.Equals(correo));
        }
        public bool consultarUsuario(string cedula)
        {
            return false;
        }
        public IList<string> consultarUsuarios()
        {
            return null;
        }
        public bool eliminarCafe(string codigo)
        {
            return false;
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

        public bool registrarCata()
        {
            return false;
        }
        public bool registrarCatacion()
        {
            return false;
        }
        public bool registrarCata(string codCatacion, int vezCatada, int rancidez, int dulce, int acidez, int cuerpo, int aroma, int amargo, int impresionGlobal, int fragancia, int saborResidual, string observaciones)
        {
            try
            {
                this.db.CATA.Add(new CATA()
                {

                    CODCATACION = "CT-" + codCatacion + vezCatada,
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
            throw new NotImplementedException();
        }

        public EVENTO consultarEvento(string codEvento)
        {
            throw new NotImplementedException();
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

            }
            return encabezado + '-' + 2;

        }

        public string obtegerTipoCafe(string codigo)
        {
            foreach (PANEL panel in this.db.PANEL.ToList())
            {
                if (panel.CODPANEL.Equals(codigo))
                    return panel.TIPOCAFE;
            }
            return "";
        }

        public CATA consultarCata(string codigo)
        {
            return this.db.CATA.FirstOrDefault(x => (x.CODCATACION+"-"+x.VEZCATADA).Equals(codigo));
        }
        /// <summary>
        /// Recibe como parametro la cedula del catador, esta es comparada con la cedula de cata catador en la base de datos, si la cedula
        /// que se ingresa por parametro coincide con alguna de la base de datos se retorna true de lo contrario se retorna false
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns></returns>
        public bool buscarCedulaCatador(string cedula)
        {
            foreach(CATADOR catador in this.db.CATADOR.ToList())
            {
                if (catador.CEDULA.Equals(cedula))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
