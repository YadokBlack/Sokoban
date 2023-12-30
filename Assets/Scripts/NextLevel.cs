using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public Sokoban sokobanScript;
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
                Debug.Log("Nivel Completado");
                sokobanScript.nivel.contadorTiempo.PararCuenta();
                ProximoLevel();
            }
        }

        if ( estados == 0 && sokobanScript.partidaIniciada)
        {
            estados = 1;
        }
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
        estados = 0;
        sokobanScript.CambiaNivel();
        Debug.Log("Ha cargado el siguiente nivel");
    }
}
