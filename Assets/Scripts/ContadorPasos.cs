using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorPasos : MonoBehaviour
{
    public int pasos;
    public TextMeshProUGUI textoPasos;

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

    public void ActualizaTexto()
    {
        textoPasos.text = "Pasos: " + pasos.ToString();
    }
}
