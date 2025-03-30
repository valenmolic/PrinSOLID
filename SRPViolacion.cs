using System;
using System.IO;
using System.Collections.Generic;

// Esta clase viola el SRP porque tiene múltiples responsabilidades:
// 1. Almacena datos del estudiante
// 2. Calcula el promedio
// 3. Muestra información en la consola
// 4. Guarda datos en un archivo
public class Estudiante
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<double> Calificaciones { get; set; }

    public Estudiante(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
        Calificaciones = new List<double>();
    }

    public void AgregarCalificacion(double calificacion)
    {
        Calificaciones.Add(calificacion);
    }

    public double CalcularPromedio()
    {
        double suma = 0;
        foreach (var calificacion in Calificaciones)
        {
            suma += calificacion;
        }
        return suma / Calificaciones.Count;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"Estudiante ID: {Id}");
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Calificaciones: {string.Join(", ", Calificaciones)}");
        Console.WriteLine($"Promedio: {CalcularPromedio()}");
    }

    public void GuardarEnArchivo(string rutaArchivo)
    {
        using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
        {
            writer.WriteLine($"{Id},{Nombre},{string.Join(";", Calificaciones)},{CalcularPromedio()}");
        }
    }
}

// Ejemplo de uso
class Program
{
    static void Main(string[] args)
    {
        Estudiante estudiante = new Estudiante(1, "Juan Pérez");
        
        Console.WriteLine("Ingrese las calificaciones del estudiante (escriba 'fin' para terminar):");
        
        string input;
        while (true)
        {
            input = Console.ReadLine();
            if (input.ToLower() == "fin")
                break;
                
            if (double.TryParse(input, out double calificacion))
            {
                estudiante.AgregarCalificacion(calificacion);
            }
            else
            {
                Console.WriteLine("Por favor ingrese un número válido.");
            }
        }
        
        estudiante.MostrarInformacion();
        estudiante.GuardarEnArchivo("estudiantes.csv");
        
        Console.WriteLine("Información guardada en el archivo 'estudiantes.csv'");
    }
}