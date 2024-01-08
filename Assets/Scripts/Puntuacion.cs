using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntuacion : MonoBehaviour
{
    public TextMeshProUGUI textPasos;
    public TextMeshProUGUI textTiempo;
    public string etiquetaPasos;
    public string etiquetaTiempo;

    public void GuardarMax(string nombreNivel)
    {
        GuardaDatosNivel(nombreNivel, 9999, 9999.99f );
    }

    public void Guardar(string nombreNivel, int pasos, float tiempo)
    {
        int pasosGuardados = PlayerPrefs.GetInt("Pasos_" + nombreNivel, 999);
        float tiempoGuardado = PlayerPrefs.GetFloat("Tiempo_" + nombreNivel, 5999);
        if ((pasos <= pasosGuardados && tiempo < tiempoGuardado))
        {
            GuardaDatosNivel(nombreNivel, pasos, tiempo);
        }
    }

    private void GuardaDatosNivel(string nombreNivel, int pasos, float tiempo)
    {
        string claveNombreNivel = "NombreNivel_" + nombreNivel;
        PlayerPrefs.SetString(claveNombreNivel, nombreNivel);
        PlayerPrefs.SetInt("Pasos_" + nombreNivel, pasos);
        PlayerPrefs.SetFloat("Tiempo_" + nombreNivel, tiempo);
        PlayerPrefs.Save();
    }

    private int ObtenerPasos(string nombreNivel)
    {
        return PlayerPrefs.GetInt("Pasos_" + nombreNivel, 999);
    }

    private float ObtenerTiempo(string nombreNivel)
    {
        return PlayerPrefs.GetFloat("Tiempo_" + nombreNivel, 5999);
    }

    public void CargaDatosNivel(string nombreNivel)
    {
        Debug.Log("Carga datos nivel " +  nombreNivel);
        textPasos.text = etiquetaPasos + " " + ObtenerPasos(nombreNivel).ToString();
        float tiempoTranscurrido =  ObtenerTiempo(nombreNivel);
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
        textTiempo.text = $"{etiquetaTiempo} {minutos:00}:{segundos:00}";
    }
}
