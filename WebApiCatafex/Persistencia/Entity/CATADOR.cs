//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Persistencia.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class CATADOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATADOR()
        {
            this.CATACION = new HashSet<CATACION>();
        }
    
        public string CEDULA { get; set; }
        public string CORREO { get; set; }
        public string CONTRASEÑA { get; set; }
        public string NOMBRE { get; set; }
        public string CODIGO { get; set; }
        public string NIVELEXP { get; set; }
        public string ESTADO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CATACION> CATACION { get; set; }
    }
}
