using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorTiempo : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    private float tiempoTranscurrido;
    private bool cuentaIniciada;
    private bool parar;

    void Update()
    {
        if (cuentaIniciada && !parar)
        {
            tiempoTranscurrido += Time.deltaTime;
            ActualizarTextoTiempo();
        }
    }

    public void IniciarCuentaTiempo()
    {
        tiempoTranscurrido = 0f;
        cuentaIniciada = true;
        parar = false;
    }

    public void PararCuentaTiempo() 
    {
        parar = true;
    }

    private void ActualizarTextoTiempo()
    {
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
        string tiempoFormateado = string.Format("Tiempo {0:00}:{1:00}", minutos, segundos);
        textoTiempo.text = tiempoFormateado;
    }
}
