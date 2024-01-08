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
        Debug.Log("Maximizado!");
        GuardaDatosNivel(nombreNivel, 9999, 9999.99f );
    }

    public void Guardar(string nombreNivel, int pasos, float tiempo)
    {
        int pasosGuardados = PlayerPrefs.GetInt("Pasos_" + nombreNivel, 999);
        float tiempoGuardado = PlayerPrefs.GetFloat("Tiempo_" + nombreNivel, 5999);
        Debug.Log("comprobando datos a guardar " + pasos + " - " + tiempo);
        Debug.Log("comprobando datos leidos " + pasosGuardados + " - " + tiempoGuardado);
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
        Debug.Log("Datos guardados!");
        Debug.Log(nombreNivel + " pasos: "  + pasos + " tiempo: "+ tiempo);
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
        textTiempo.text = string.Format("{0} {0:00}:{1:00}", etiquetaTiempo, minutos, segundos);
        Debug.Log("-->" + ObtenerPasos(nombreNivel).ToString() + " - " + ObtenerTiempo(nombreNivel));
    }
}
