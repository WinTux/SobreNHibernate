using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SobreNHibernate
{
    
    public class Conexion
    {
        Configuration configuracion;

        public void Configurar() {
            //Inicializamos  el objeto
            configuracion = new Configuration();
            /*
            //Le pasamos la cadena de conexión, diver y dialecto pertinentes.
            configuracion.DataBaseIntegration(x =>
                {
                    x.ConnectionString = "Server=192.168.1.253;Database=Instituto X;" +
                        "User=Pepe;Password=123456;";
                    x.Driver<SqlClientDriver>();
                    x.Dialect<MsSql2012Dialect>();

                }
            );
            //Acá va a encontrar los archivos de mapeo
            configuracion.AddAssembly(Assembly.GetExecutingAssembly());
            */
            configuracion.Configure();
        }

        public void insertarEstudiante(Estudiante est) {
            //Se encarga de compilar todo el metadato  necesario para inicializar nhibernate
            var sesionFactory = configuracion.BuildSessionFactory();
            //sesionFactory puede ser usado para contruir sesiones (conexiones a DDBB)
            using (var sesion = sesionFactory.OpenSession()) {
                //Tras contar con una sesion, podemos iniciar una transacción
                using (var transaccion = sesion.BeginTransaction()) {
                    sesion.Save(est);//Registramos nuevo estudiante
                    transaccion.Commit();//Afirmamos su registro
                }
            }
        }

        public void mostrarEstudiantes()
        {
            var sesionFactory = configuracion.BuildSessionFactory();
            using (var sesion = sesionFactory.OpenSession())
            {
                using (var transaccion = sesion.BeginTransaction())
                {
                    var estudiantes = sesion.CreateCriteria<Estudiante>()
                        .List<Estudiante>();
                    foreach (Estudiante e in estudiantes)
                        Console.WriteLine($"CI:{e.ci}; " +
                            $"Nombre:{e.nombre} {e.apellido}; " +
                            $"Fecha:{e.fecha_nac.ToString("dd/MM/yyyy")}");
                    transaccion.Commit();//Afirmamos su registro
                }
            }
        }
        public void mostrarEstudiante(int ci)
        {
            var sesionFactory = configuracion.BuildSessionFactory();
            using (var sesion = sesionFactory.OpenSession())
            {
                using (var transaccion = sesion.BeginTransaction())
                {
                    var e = sesion.Get<Estudiante>(ci);
                    Console.WriteLine($"CI:{e.ci}; " +
                            $"Nombre:{e.nombre} {e.apellido}; " +
                            $"Fecha:{e.fecha_nac.ToString("dd/MM/yyyy")}");
                    transaccion.Commit();//Afirmamos su registro
                }
            }
        }
        public void modificarEstudiante(int ci)
        {
            var sesionFactory = configuracion.BuildSessionFactory();
            using (var sesion = sesionFactory.OpenSession())
            {
                using (var transaccion = sesion.BeginTransaction())
                {
                    var e = sesion.Get<Estudiante>(ci);
                    e.fecha_nac = new DateTime(1995,12,30);
                    transaccion.Commit();//Afirmamos su registro
                }
            }
            mostrarEstudiantes();
        }

        public void eliminarEstudiante(int ci)
        {
            var sesionFactory = configuracion.BuildSessionFactory();
            using (var sesion = sesionFactory.OpenSession())
            {
                using (var transaccion = sesion.BeginTransaction())
                {
                    var e = sesion.Get<Estudiante>(ci);
                    //Estudiante e = new Estudiante { ci = ci};
                    sesion.Delete(e);
                    transaccion.Commit();//Afirmamos su registro
                }
            }
            mostrarEstudiantes();
        }
    }
}
