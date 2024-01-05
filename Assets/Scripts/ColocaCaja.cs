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
        }
        else
        {
            CambiarColor(colorNormal);
        }
    }

    public bool EstaEnCasillaCorrecta()
    {
        int x = Mathf.FloorToInt(transform.position.x / sokobanScript.alturaImagen);
        int y = Mathf.FloorToInt(transform.position.y / sokobanScript.alturaImagen);
        return sokobanScript.datosMapaVisual[x, y] == 3;
    }

    void CambiarColor(Color nuevoColor)
    {
        image.color = nuevoColor;
    }
}