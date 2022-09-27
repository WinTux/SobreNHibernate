using System;
using System.Collections.Generic;
using System.Text;

namespace SobreNHibernate
{
    public class Estudiante
    {
        public virtual int ci { get; set; }
        public virtual string nombre { get; set; }
        public virtual string apellido { get; set; }
        public virtual DateTime fecha_nac { get; set; }
        public virtual string email { get; set; }
        public virtual string direccion { get; set; }
    }
}
