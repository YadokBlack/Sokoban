using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorTiempo : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    public string etiquetaTiempo;
    private float tiempoTranscurrido;
    private bool cuentaIniciada;
    private bool parar;

    void Update()
    {
        if (cuentaIniciada && !parar)
        {
            tiempoTranscurrido += Time.deltaTime;
            ActualizarTexto();
        }
    }

    public float Tiempo()
    {
        return tiempoTranscurrido;
    }

    public void IniciarCuenta()
    {
        tiempoTranscurrido = 0f;
        cuentaIniciada = true;
        parar = false;
    }

    public void PararCuenta() 
    {
        parar = true;
    }

    public void Reiniciar()
    {
        tiempoTranscurrido = 0f;
        ActualizarTexto();
        cuentaIniciada = false;
        parar = false;
    }

    private void ActualizarTexto()
    {
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
        textoTiempo.text = $"{etiquetaTiempo} {minutos:00}:{segundos:00}";        
    }
}
