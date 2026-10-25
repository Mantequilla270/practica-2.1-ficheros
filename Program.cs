using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Practica
{
    public string rutaArchivo = "";

    public static void Main(string[] args)
    {
        Practica programa = new Practica();
        bool salir = false;

        while (!salir)
        {
            programa.MostrarMenu();
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    programa.CrearFichero();
                    break;
                case "2":
                    programa.AnadirRegistros();
                    break;
                case "3":
                    programa.EliminarRegistros();
                    break;
                case "4":
                    programa.EliminarFichero();
                    break;
                case "5":
                    programa.VisualizarRegistros();
                    break;
                case "6":
                    salir = true;
                    Console.WriteLine("Cerrando la aplicación...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine();
        }
    }

    public void MostrarMenu()
    {
        Console.WriteLine("========== MENÚ ==========");
        Console.WriteLine("1. Crear un nuevo fichero");
        Console.WriteLine("2. Añadir registros en el fichero");
        Console.WriteLine("3. Eliminar todos los registros del fichero");
        Console.WriteLine("4. Eliminar el fichero");
        Console.WriteLine("5. Visualizar los registros");
        Console.WriteLine("6. Cerrar la aplicación");
        Console.Write("Elige una opción: ");
    }

    public bool HayFichero()
    {
        if (rutaArchivo == "" || !File.Exists(rutaArchivo))
        {
            Console.WriteLine("Primero tienes que crear un fichero (opción 1).");
            return false;
        }
        return true;
    }

    public void CrearFichero()
    {
        Console.Write("Nombre del fichero: ");
        string nombre = Console.ReadLine();
        rutaArchivo = "./" + nombre + ".txt";

        if (File.Exists(rutaArchivo))
        {
            Console.WriteLine("El fichero ya existe, se usará ese.");
            return;
        }

        try
        {
            FileStream archivo = File.Create(rutaArchivo);
            archivo.Close();
            Console.WriteLine("**Archivo creado exitosamente.**");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al crear el archivo: " + ex.Message);
        }
    }

    public void AnadirRegistros()
    {
        if (!HayFichero()) return;

        List<string> lineas = File.ReadAllLines(rutaArchivo).ToList();

        Console.WriteLine("Escribe los registros (línea vacía para terminar):");
        string registro = Console.ReadLine();

        while (registro != "")
        {
            lineas.Add(registro);
            registro = Console.ReadLine();
        }

        File.WriteAllLines(rutaArchivo, lineas);
        Console.WriteLine("**Registros añadidos.**");
    }

    public void EliminarRegistros()
    {
        if (!HayFichero()) return;

        File.WriteAllText(rutaArchivo, "");
        Console.WriteLine("**Registros eliminados.**");
    }

    public void EliminarFichero()
    {
        if (!HayFichero()) return;

        try
        {
            File.Delete(rutaArchivo);
            rutaArchivo = "";
            Console.WriteLine("**Fichero eliminado.**");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al eliminar el fichero: " + ex.Message);
        }
    }

    public void VisualizarRegistros()
    {
        if (!HayFichero()) return;

        string[] lineas = File.ReadAllLines(rutaArchivo).ToArray();

        if (lineas.Length == 0)
        {
            Console.WriteLine("El fichero está vacío.");
            return;
        }

        int numero = 1;
        foreach (string s in lineas)
        {
            Console.WriteLine(numero + ". " + s);
            numero++;
        }
    }
}
