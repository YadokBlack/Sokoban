using UnityEngine;
using System.IO;
using TMPro;

public class Level : MonoBehaviour
{
    [System.Serializable]
    public class DatosNivel
    {
        public string nombre;
        public string[] lineas = new string[20];
    }

    public Sokoban sokoban;
    public ContadorTiempo contadorTiempo;
    DatosNivel nivel;
    private int levelActual;
    public int nivelInicial = 1;
    public TextMeshProUGUI textNombreNivel;

    public void Inicializar()
    {
        levelActual = nivelInicial;
    }

    public void IniciaTiempoPartida()
    {
        contadorTiempo.IniciarCuenta();
    }

    public void Cargar()
    {
        Debug.Log("Cargando Nivel ...");
        CargarDatosNivel(GenerarRutaArchivoNivel(levelActual));       
    }

    public void Aumenta()
    {
        levelActual++;
    }

    public string NombreNivelActual()
    {
        return nivel.nombre;
    }

    private void CargarDatosNivel(string rutaArchivo)
    {
        nivel = null;                   
        LeerArchivo(rutaArchivo);
    }

    private void LeerArchivo(string nombreArchivo)
    {
        Debug.Log($"Intentando cargar archivo desde Resources: levels/{nombreArchivo}");
        TextAsset archivo = Resources.Load<TextAsset>($"levels/{nombreArchivo}");
        if (archivo == null)
        {
            Debug.LogError("No se encontró el archivo en Resources: " + nombreArchivo);
            return;
        }

        string contenidoArchivo = archivo.text;
        nivel = JsonUtility.FromJson<DatosNivel>(contenidoArchivo);

        if (nivel == null)
            Debug.LogError("Error al deserializar el archivo JSON.");

        CargaArchivoNivel();
    }

    private void CargaArchivoNivel()
    {
        string nombre = nivel.nombre;
        textNombreNivel.text = nombre;
        string[] lineas = nivel.lineas;
        Debug.Log("Nombre: " + nombre);
        ActualizaMapaVisual();
        ActualizaMapaColision();
        // Debug.Log("Nombre del nivel cargado: " + nivel.nombre);
        //  ImprimirMapa(sokoban.datosMapaVisual, "Visual");
        //  ImprimirMapa(sokoban.datosMapaColision, "Colision");
    }

    private void ActualizaMapaVisual()
    {
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
    }

    private void ActualizaMapaColision()
    {
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
    }

    string ObtenerRutaDelEjecutable()
    {
        return Directory.GetCurrentDirectory();
    }

    string GenerarRutaArchivoNivel(int numeroDeNivel)
    {
        return $"Level{numeroDeNivel:D2}"; 
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
