using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorPasos : MonoBehaviour
{
    public int pasos;
    public TextMeshProUGUI textoPasos;
    public string etiquetaPasos;

    public void Inicializar()
    {
        pasos = 0;
        ActualizaTexto();
    }
    public void Aumenta()
    {
        pasos++;
        ActualizaTexto();
    }

    public bool Inicio()
    {
        return pasos == 0;
    }

    public void ActualizaTexto()
    {
        textoPasos.text =  etiquetaPasos + " " + pasos.ToString();
    }
}
