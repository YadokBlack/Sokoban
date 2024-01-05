using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public Sokoban sokobanScript;
    public Puntuacion puntuacion;
    public ContadorPasos contadorPasos;
    public Level nivel;
    public int estados;

    private void Awake()
    {
        estados = 0;
    }

    private void Update()
    {
        if (estados == 1)
        {
            if (LevelCompletado())
            {
                ProximoLevel();
            }
        }

        if ( estados == 0 && sokobanScript.partidaIniciada)
        {
            estados = 1;
        }
    }

    public void CargaLevel()
    {
       // PlayerPrefs.DeleteAll();
        nivel.Inicializar();
        nivel.Cargar();
        
        puntuacion.CargaDatosNivel( nivel.NombreNivelActual() );
        sokobanScript.MostrarMapa();
    }

    public bool LevelCompletado()
    {
        foreach ( GameObject caja in sokobanScript.cajas )
        {
            if (!caja.GetComponent<ColocaCaja>().EstaEnCasillaCorrecta()) return false;
        }
        return true;
    }

    public void ProximoLevel()
    {
        Debug.Log("Nivel Completado");
        nivel.contadorTiempo.PararCuenta();
        puntuacion.Guardar(nivel.NombreNivelActual(), contadorPasos.pasos, nivel.contadorTiempo.Tiempo());
        estados = 0;
        sokobanScript.QuitarEscenario();
        nivel.contadorTiempo.Reiniciar();
        nivel.Aumenta();
        nivel.Cargar();
        puntuacion.CargaDatosNivel(nivel.NombreNivelActual());
        sokobanScript.MostrarMapa();
    }
}
