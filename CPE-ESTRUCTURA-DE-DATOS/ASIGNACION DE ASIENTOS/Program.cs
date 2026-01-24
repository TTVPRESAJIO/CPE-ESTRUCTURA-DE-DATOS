using System;
using System.Collections.Generic;

class Persona
{
    public string Nombre { get; set; }

    public Persona(string nombre)
    {
        Nombre = nombre;
    }
}

class Atraccion
{
    private Queue<Persona> fila;
    private int asientosOcupados;
    private const int CAPACIDAD = 30;

    public Atraccion()
    {
        fila = new Queue<Persona>();
        asientosOcupados = 0;
    }

    public void AgregarPersona(string nombre)
    {
        fila.Enqueue(new Persona(nombre));
        Console.WriteLine($"{nombre} fue agregado a la fila.");
    }

    public void SubirPersona()
    {
        if (asientosOcupados >= CAPACIDAD)
        {
            Console.WriteLine("La atracción ya está llena. No hay asientos disponibles.");
            return;
        }

        if (fila.Count > 0)
        {
            Persona persona = fila.Dequeue();
            asientosOcupados++;
            Console.WriteLine($"{persona.Nombre} subió a la atracción.");
        }
        else
        {
            Console.WriteLine("No hay personas en la fila.");
        }
    }

    public void MostrarFila()
    {
        Console.WriteLine("\nPersonas en la fila:");

        if (fila.Count == 0)
        {
            Console.WriteLine("La fila está vacía.");
        }
        else
        {
            foreach (Persona p in fila)
            {
                Console.WriteLine("- " + p.Nombre);
            }
        }
    }

    // 👉 NUEVA FUNCIÓN
    public void MostrarAsientosDisponibles()
    {
        int disponibles = CAPACIDAD - asientosOcupados;
        Console.WriteLine($"\nAsientos disponibles: {disponibles}");

        if (disponibles == 0)
        {
            Console.WriteLine("La atracción está LLENA.");
        }
        else
        {
            Console.WriteLine("La atracción todavía tiene espacio.");
        }
    }
}

class Program
{
    static void Main()
    {
        Atraccion atraccion = new Atraccion();
        int opcion;

        do
        {
            Console.WriteLine("\n--- ATRACCIÓN DEL PARQUE ---");
            Console.WriteLine("1. Agregar persona a la fila");
            Console.WriteLine("2. Subir persona a la atracción");
            Console.WriteLine("3. Ver fila");
            Console.WriteLine("4. Ver asientos disponibles");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese el nombre de la persona: ");
                    string nombre = Console.ReadLine();
                    atraccion.AgregarPersona(nombre);
                    break;

                case 2:
                    atraccion.SubirPersona();
                    break;

                case 3:
                    atraccion.MostrarFila();
                    break;

                case 4:
                    atraccion.MostrarAsientosDisponibles();
                    break;

                case 5:
                    Console.WriteLine("Fin de la simulación.");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 5);
    }
}
