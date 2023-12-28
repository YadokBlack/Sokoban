using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// using static Level;

public class Level : MonoBehaviour
{
    [System.Serializable]
    public class DatosNivel
    {
        public string nombre;
        public string[] lineas = new string[20];
    }

    public Sokoban sokoban;

    DatosNivel nivel;
 
    public void CargarNivel(int nivel)
    {
        Debug.Log("Cargando Nivel ...");
        CargarDatosNivel(GenerarRutaArchivoNivel(nivel));
    }

    public void CargarDatosNivel(string rutaArchivo)
    {
        nivel = null;

        if (File.Exists(rutaArchivo))
        {
            Debug.Log("Archivo leido");
            string contenidoArchivo = File.ReadAllText(rutaArchivo);
            nivel = JsonUtility.FromJson<DatosNivel>(contenidoArchivo);

            if (nivel == null)
            {
                Debug.LogError("Error al deserializar el archivo JSON.");
            }
            else
            {
                // Accede a las variables del objeto
                string nombre = nivel.nombre;
                string[] lineas = nivel.lineas;

                // Imprime las variables
                Debug.Log("Nombre: " + nombre);

                for (int i = 0; i < 10; i++)
                {
                    string linea = nivel.lineas[i];
                    for (int j = 0; j < 10; j++)
                    {
                        char caracter = linea[j];
                        int valor = caracter - 'a';
                        sokoban.datosMapaVisual[i, j] = valor;
                    }
                }

                for (int i = 10; i < 20; i++)
                {
                    string linea = nivel.lineas[i];
                    for (int j = 0; j < 10; j++)
                    {
                        char caracter = linea[j];
                        int valor = caracter - 'a';
                        sokoban.datosMapaColision[i - 10, j] = valor;
                    }
                }

                Debug.Log("Nombre del nivel cargado: " + nivel.nombre);
                ImprimirMapa(sokoban.datosMapaVisual, "Visual");
                ImprimirMapa(sokoban.datosMapaColision, "Colision");
            }
        }
        else
        {
            Debug.LogError("Archivo no encontrado en la ruta: " + rutaArchivo);
        }
    }

    string ObtenerRutaDelEjecutable()
    {
        return Directory.GetCurrentDirectory();
    }

    string GenerarRutaArchivoNivel(int numeroDeNivel)
    {
        string nombreArchivo = $"level{numeroDeNivel:D2}.json"; 
        string rutaCompleta = Path.Combine(ObtenerRutaDelEjecutable(), "levels", nombreArchivo);

        return rutaCompleta;
    }

    private void ImprimirMapa(int[,] mapa, string titulo)
    {
        Debug.Log("Mapa " + titulo + ":" );
        for (int i = 0; i < mapa.GetLength(0); i++)
        {
            string row = "";
            for (int j = 0; j < mapa.GetLength(1); j++)
            {
                row += mapa[i, j] + " ";
            }
            Debug.Log(row);
        }
    }
}
