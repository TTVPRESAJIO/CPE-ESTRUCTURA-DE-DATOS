using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Paciente> pacientes = new();
    static List<Medico> medicos = new();
    static List<Cita> citas = new();

    static void Main()
    {
        // Médico de ejemplo
        medicos.Add(new Medico
        {
            Id = 1,
            Nombre = "Dra. Gómez",
            Especialidad = "Medicina General",
            HoraInicio = new TimeSpan(8, 0, 0),
            HoraFin = new TimeSpan(16, 0, 0),
            DuracionCita = 30
        });

        while (true)
        {
            Console.WriteLine("\n=== AGENDA CLÍNICA ===");
            Console.WriteLine("1. Crear paciente");
            Console.WriteLine("2. Crear cita");
            Console.WriteLine("3. Listar citas");
            Console.WriteLine("4. Ver disponibilidad");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearPaciente();
                    break;
                case "2":
                    CrearCita();
                    break;
                case "3":
                    ListarCitas();
                    break;
                case "4":
                    VerDisponibilidad();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        }
    }

    // ================= FUNCIONES =================

    static void CrearPaciente()
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Cédula: ");
        string cedula = Console.ReadLine();

        pacientes.Add(new Paciente
        {
            Id = pacientes.Count + 1,
            Nombres = nombre,
            Cedula = cedula
        });

        Console.WriteLine("Paciente creado correctamente");
    }

    static void CrearCita()
    {
        if (pacientes.Count == 0)
        {
            Console.WriteLine("No hay pacientes registrados");
            return;
        }

        Console.Write("ID del paciente: ");
        int idPaciente = int.Parse(Console.ReadLine());

        Paciente paciente = pacientes.FirstOrDefault(p => p.Id == idPaciente);
        if (paciente == null)
        {
            Console.WriteLine("Paciente no encontrado");
            return;
        }

        Medico medico = medicos[0];

        Console.Write("Hora inicio (HH:mm): ");
        TimeSpan horaInicio = TimeSpan.Parse(Console.ReadLine());

        TimeSpan horaFin = horaInicio.Add(TimeSpan.FromMinutes(medico.DuracionCita));

        bool choque = citas.Any(c =>
            c.Medico.Id == medico.Id &&
            c.HoraInicio == horaInicio &&
            c.Estado == "Programada");

        if (choque)
        {
            Console.WriteLine("Horario no disponible");
            return;
        }

        citas.Add(new Cita
        {
            Id = citas.Count + 1,
            Paciente = paciente,
            Medico = medico,
            HoraInicio = horaInicio,
            HoraFin = horaFin,
            Estado = "Programada"
        });

        Console.WriteLine("Cita creada correctamente");
    }

    static void ListarCitas()
    {
        if (citas.Count == 0)
        {
            Console.WriteLine("No hay citas");
            return;
        }

        foreach (var c in citas)
        {
            Console.WriteLine($"{c.HoraInicio} - {c.Paciente.Nombres} ({c.Estado})");
        }
    }

    static void VerDisponibilidad()
    {
        Medico medico = medicos[0];

        Console.WriteLine($"\nDisponibilidad de {medico.Nombre}");

        for (TimeSpan hora = medico.HoraInicio; hora < medico.HoraFin; hora += TimeSpan.FromMinutes(medico.DuracionCita))
        {
            bool ocupado = citas.Any(c =>
                c.Medico.Id == medico.Id &&
                c.HoraInicio == hora &&
                c.Estado == "Programada");

            Console.WriteLine($"{hora:hh\\:mm} - {(ocupado ? "OCUPADO" : "LIBRE")}");
        }
    }
}

// ================= CLASES =================

class Paciente
{
    public int Id;
    public string Nombres = "";
    public string Cedula = "";
}

class Medico
{
    public int Id;
    public string Nombre = "";
    public string Especialidad = "";
    public TimeSpan HoraInicio;
    public TimeSpan HoraFin;
    public int DuracionCita;
}

class Cita
{
    public int Id;
    public Paciente Paciente;
    public Medico Medico;
    public TimeSpan HoraInicio;
    public TimeSpan HoraFin;
    public string Estado = "";
}
