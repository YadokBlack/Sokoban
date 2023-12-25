using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColocaCaja : MonoBehaviour
{
    public Image image;
    public Color colorCorrecto = Color.green;  
    public Color colorNormal = Color.white;    
    public Sokoban sokobanScript;

    private void Start()
    {
        sokobanScript = FindObjectOfType<Sokoban>();

        if (sokobanScript == null)
        {
            Debug.LogError("No se encontró el script Sokoban en la escena.");
        }

        image.color = colorNormal;
    }

    void Update()
    {
        if (EstaEnCasillaCorrecta())
        {
            CambiarColor(colorCorrecto);
           // Debug.Log("Colocada en su sitio");
        }
        else
        {
            CambiarColor(colorNormal);
           // Debug.Log("fuera de lugar");
        }
    }

    bool EstaEnCasillaCorrecta()
    {
        int x = Mathf.FloorToInt(transform.position.x / sokobanScript.alturaImagen);
        int y = Mathf.FloorToInt(transform.position.y / sokobanScript.alturaImagen);

        Debug.Log("Comprobando caja que esta en:" + x + ", " + y);
        Debug.Log("valor " + sokobanScript.datosMapaVisual[x, y]);

        return sokobanScript.datosMapaVisual[x, y] == 3;
    }

    void CambiarColor(Color nuevoColor)
    {
        image.color = nuevoColor;
    }
}