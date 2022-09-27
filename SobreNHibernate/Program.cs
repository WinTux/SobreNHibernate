using System;

namespace SobreNHibernate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Conexion con = new Conexion();
            con.Configurar();

            Console.WriteLine("NHIBERNATE\n===========\n" +
                "1. Registrar estudiante 111000\n" +
                "2. Mostrar estudiantes\n" +
                "3. Mostrar a estudiante de ci: 123\n" +
                "4. Modificar estudiante 111000\n" +
                "5. Eliminar estudiante 111000\n");
            int val = int.Parse(Console.ReadLine());
            switch (val) {
                case 1:
                    Estudiante es = new Estudiante { 
                        ci = 111000,
                        nombre = "Raul",
                        apellido = "Molinedo",
                        direccion = "Z. Norte C. 1"
                    };
                    con.insertarEstudiante(es);
                    Console.WriteLine("Registro exitoso");
                    break;
                case 2:
                    con.mostrarEstudiantes();
                    break;
                case 3:
                    con.mostrarEstudiante(123);
                    break;
                case 4:
                    con.modificarEstudiante(111000);
                    break;
                case 5:
                    con.eliminarEstudiante(111000);
                    break;
                default:
                    break;
            }

            Console.WriteLine("FIN");
        }
    }
}
