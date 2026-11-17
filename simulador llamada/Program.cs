using System;
using System.Collections.Generic;

namespace PilaDeLlamadas
{
    class Llamada
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }

        public Llamada(int id, string nombre, string telefono)
        {
            Id = id;
            NombreCliente = nombre;
            Telefono = telefono;
            Estado = "Pendiente";
            Fecha = DateTime.Now;
        }

        public void Mostrar()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Cliente: " + NombreCliente);
            Console.WriteLine("Telefono: " + Telefono);
            Console.WriteLine("Estado: " + Estado);
            Console.WriteLine("Fecha: " + Fecha);
            Console.WriteLine("--------------------------------");
        }
    }

    class Program
    {
        // Pila principal de llamadas activas
        static Stack<Llamada> pilaLlamadas =
            new Stack<Llamada>();

        // Pila de llamadas finalizadas
        static Stack<Llamada> llamadasFinalizadas =
            new Stack<Llamada>();

        // Pila que simula las llamadas a funciones
        static Stack<string> pilaFunciones =
            new Stack<string>();

        static int contadorId = 1;

        static void Main(string[] args)
        {
            EjecutarFuncion("Main", MenuPrincipal);
        }

        // Simula la entrada y salida de una funcion
        static void EjecutarFuncion(string nombre, Action funcion)
        {
            pilaFunciones.Push(nombre);

            Console.WriteLine("\nEntrando a: " + nombre);
            MostrarPilaFunciones();

            try
            {
                funcion();
            }
            finally
            {
                Console.WriteLine("\nRegresando de: " + nombre);

                pilaFunciones.Pop();

                MostrarPilaFunciones();
            }
        }

        // Muestra la pila de funciones
        static void MostrarPilaFunciones()
        {
            Console.WriteLine("\nPILA DE FUNCIONES:");

            foreach (string funcion in pilaFunciones)
            {
                Console.WriteLine("-> " + funcion);
            }
        }

        // Menu principal
        static void MenuPrincipal()
        {
            int opcion;

            do
            {
                Console.WriteLine("\n================================");
                Console.WriteLine("       PILA DE LLAMADAS");
                Console.WriteLine("================================");
                Console.WriteLine("1. Registrar llamada");
                Console.WriteLine("2. Atender llamada");
                Console.WriteLine("3. Finalizar llamada");
                Console.WriteLine("4. Mostrar llamadas activas");
                Console.WriteLine("5. Recuperar ultima llamada");
                Console.WriteLine("6. Buscar llamada");
                Console.WriteLine("7. Eliminar llamada");
                Console.WriteLine("8. Salir");
                Console.WriteLine("================================");

                opcion = LeerEntero("Selecciona una opcion: ");

                switch (opcion)
                {
                    case 1:
                        EjecutarFuncion(
                            "RegistrarLlamada", RegistrarLlamada);
                        break;

                    case 2:
                        EjecutarFuncion(
                            "AtenderLlamada", AtenderLlamada);
                        break;

                    case 3:
                        EjecutarFuncion(
                            "FinalizarLlamada", FinalizarLlamada);
                        break;

                    case 4:
                        EjecutarFuncion(
                            "MostrarLlamadasActivas",
                            MostrarLlamadasActivas);
                        break;

                    case 5:
                        EjecutarFuncion(
                            "RecuperarUltimaLlamada",
                            RecuperarUltimaLlamada);
                        break;

                    case 6:
                        EjecutarFuncion(
                            "BuscarLlamada", BuscarLlamada);
                        break;

                    case 7:
                        EjecutarFuncion(
                            "EliminarLlamada", EliminarLlamada);
                        break;

                    case 8:
                        Console.WriteLine("\nSaliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }

            } while (opcion != 8);
        }

        // 1. Registrar llamada
        static void RegistrarLlamada()
        {
            Console.WriteLine("\n--- REGISTRAR LLAMADA ---");

            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Numero telefonico: ");
            string telefono = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(telefono))
            {
                Console.WriteLine("Los datos no pueden estar vacios.");
                return;
            }

            Llamada nueva = new Llamada(
                contadorId, nombre, telefono);

            pilaLlamadas.Push(nueva);

            Console.WriteLine("\nLlamada registrada correctamente.");
            Console.WriteLine("ID asignado: " + contadorId);

            contadorId++;
        }

        // 2. Atender llamada
        static void AtenderLlamada()
        {
            Console.WriteLine("\n--- ATENDER LLAMADA ---");

            if (pilaLlamadas.Count == 0)
            {
                Console.WriteLine("No hay llamadas disponibles.");
                return;
            }

            Llamada llamada = pilaLlamadas.Peek();

            if (llamada.Estado == "En atencion")
            {
                Console.WriteLine("Esta llamada ya esta siendo atendida.");
                return;
            }

            llamada.Estado = "En atencion";

            Console.WriteLine("\nLlamada atendida correctamente.");
            llamada.Mostrar();
        }

        // 3. Finalizar llamada
        static void FinalizarLlamada()
        {
            Console.WriteLine("\n--- FINALIZAR LLAMADA ---");

            if (pilaLlamadas.Count == 0)
            {
                Console.WriteLine("No hay llamadas para finalizar.");
                return;
            }

            Llamada llamada = pilaLlamadas.Peek();

            if (llamada.Estado != "En atencion")
            {
                Console.WriteLine(
                    "Primero debes atender la llamada.");
                return;
            }

            llamada = pilaLlamadas.Pop();

            llamada.Estado = "Finalizada";

            llamadasFinalizadas.Push(llamada);

            Console.WriteLine("\nLlamada finalizada correctamente.");
            llamada.Mostrar();
        }

        // 4. Mostrar llamadas activas
        static void MostrarLlamadasActivas()
        {
            Console.WriteLine("\n--- LLAMADAS ACTIVAS ---");

            if (pilaLlamadas.Count == 0)
            {
                Console.WriteLine("No existen llamadas activas.");
                return;
            }

            Console.WriteLine(
                "Total de llamadas: " + pilaLlamadas.Count);

            foreach (Llamada llamada in pilaLlamadas)
            {
                llamada.Mostrar();
            }
        }

        // 5. Recuperar ultima llamada finalizada
        static void RecuperarUltimaLlamada()
        {
            Console.WriteLine("\n--- RECUPERAR ULTIMA LLAMADA ---");

            if (llamadasFinalizadas.Count == 0)
            {
                Console.WriteLine(
                    "No existen llamadas finalizadas para recuperar.");
                return;
            }

            Llamada llamada = llamadasFinalizadas.Pop();

            llamada.Estado = "Pendiente";

            pilaLlamadas.Push(llamada);

            Console.WriteLine("\nLlamada recuperada correctamente.");
            llamada.Mostrar();
        }

        // 6. Buscar llamada
        static void BuscarLlamada()
        {
            Console.WriteLine("\n--- BUSCAR LLAMADA ---");

            int id = LeerEntero("Ingresa el ID de la llamada: ");

            foreach (Llamada llamada in pilaLlamadas)
            {
                if (llamada.Id == id)
                {
                    Console.WriteLine("\nLlamada encontrada:");
                    llamada.Mostrar();
                    return;
                }
            }

            foreach (Llamada llamada in llamadasFinalizadas)
            {
                if (llamada.Id == id)
                {
                    Console.WriteLine("\nLlamada encontrada:");
                    llamada.Mostrar();
                    return;
                }
            }

            Console.WriteLine("No se encontro la llamada.");
        }

        // 7. Eliminar llamada
        static void EliminarLlamada()
        {
            Console.WriteLine("\n--- ELIMINAR LLAMADA ---");

            int id = LeerEntero("Ingresa el ID de la llamada: ");

            if (EliminarDePila(pilaLlamadas, id))
            {
                Console.WriteLine(
                    "Llamada activa eliminada correctamente.");
                return;
            }

            if (EliminarDePila(llamadasFinalizadas, id))
            {
                Console.WriteLine(
                    "Llamada finalizada eliminada correctamente.");
                return;
            }

            Console.WriteLine("No se encontro la llamada.");
        }

        // Elimina una llamada especifica conservando
        // el orden de los elementos de la pila
        static bool EliminarDePila(
            Stack<Llamada> pila, int id)
        {
            Stack<Llamada> auxiliar = new Stack<Llamada>();

            bool encontrada = false;

            while (pila.Count > 0)
            {
                Llamada llamada = pila.Pop();

                if (llamada.Id == id)
                {
                    encontrada = true;
                    break;
                }

                auxiliar.Push(llamada);
            }

            while (auxiliar.Count > 0)
            {
                pila.Push(auxiliar.Pop());
            }

            return encontrada;
        }

        // Validar que el usuario ingrese un numero entero
        static int LeerEntero(string mensaje)
        {
            int numero;

            while (true)
            {
                Console.Write(mensaje);

                if (int.TryParse(Console.ReadLine(), out numero))
                {
                    return numero;
                }

                Console.WriteLine("Ingresa un numero valido.");
            }
        }
    }
}