using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public bool actualizarCafe()
        {
            return false;
        }

        public bool actualizarEvento()
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
            return null;
        }

        public IList<string> consultarCatasAsignadas(string codCatador)
        {
            return null;
        }

        public string consultarEvento(string codEvento)
        {
            return null;
        }

        public IList<string> consultarEventos()
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

        public string consultarUsuario(string correo, string contrasena)
        {
            return null;
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

        public bool eliminarEvento(string codEvento)
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

        public void insertarCafe()
        {
           
        }

        public bool insertarCatador()
        {
            return false;
        }

        public bool insertarEvento()
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
                    CODPANEL = this.generarCodigoPanel(),
                    CODEVENTO = codEvento,
                    TIPOCAFE = tipoCafe,
                    HORA = hora
                }) ;
                this.db.SaveChanges();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        private string generarCodigoPanel()
        {
            PANEL panel;
            string codigo = "";
            try {
                panel = this.db.PANEL.ToList().Last();
                codigo = "PA-" + this.numeroPanel(panel.CODPANEL);
            }
            catch (Exception)
            {
                codigo = "PA-01";
            }
            return codigo;
        }

        private int numeroPanel(string codigo)
        {
            string[] cod = codigo.Split('-');
            return int.Parse(cod[1]) + 1;
        }

        public bool insertarReporte()
        {
            return false;
        }

        public bool registrarCata()
        {
            return false;
        }

        public bool registrarCatacion()
        {
            return false;
        }
    }
}
