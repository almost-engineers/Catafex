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
        private CatafexEntities db;
        public EntityFramework()
        {
            this.db = new CatafexEntities();
        }
        public bool actualizarCafe(string nombre, string tipoCafe, string codCafe, string origen, string procedencia )
        {
            try
                {
                  foreach(CAFE cafe in this.db.CAFE.ToList())
                    {
                      if(cafe.CODCAFE.Equals(codCafe))
                        {
                          cafe.NOMBRE = nombre;
                          cafe.TIPOCAFE = tipoCafe;
                          cafe.CODCAFE = codCafe; 
                          cafe.ORIGEN = origen;
                          cafe.PROCEDENCIA = procedencia;
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

        public bool actualizarEvento()
        {
            return false;
        }

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

        public PANEL consultarPanel(string codPanel)
        {
            return this.db.PANEL.FirstOrDefault(x => x.CODPANEL.Equals(codPanel));
        }

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

        public bool eliminarEvento(string codEvento)
        {
            return false;
        }

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
             try
            {
                this.db.CAFE.Add(new CAFE()
                {
                    NOMBRE = nombre,
                    TIPOCAFE = tipocafe,
                    PROCEDENCIA = procedencia,
                    ORIGEN = origen,
                    CODCAFE = this.generarCodigoCafe(),
             
                });

                this.db.SaveChanges();
                return true;
            }
             catch(Exception)
               {
                return false;
               }
           
        }

        private string generarCodigoCafe()
        {
            Random random = new Random () ;
            int randomNumber = random.Next(1,100);
            
        }

        public bool insertarCatador()
        {
            return false;
        }

        public bool insertarEvento()
        {
            return false;
        }

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
            return "1";
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
