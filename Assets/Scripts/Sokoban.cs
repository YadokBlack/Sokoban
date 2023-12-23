using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Sokoban : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject padre;

    int alturaImagen = 60;
    const int tamanyo = 10;
    int[,] datosMapa;

    private void Start()
    {
        AsignarValoresIniciales();

        MostrarMapa();
    }

    void AsignarValoresIniciales()
    {
        datosMapa = new int[tamanyo, tamanyo]
        {
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            {2, 1, 0, 0, 0, 0, 0, 0, 1, 2},
            {2, 1, 0, 4, 0, 5, 3, 0, 1, 2},
            {2, 1, 0, 0, 0, 0, 0, 0, 1, 2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        };
    }

    void MostrarMapa()
    {
        for (int x = 0; x < tamanyo; x++)
        {
            for (int y = 0; y < tamanyo; y++)
            {
                int valor = datosMapa[x, y];
                if (valor >= 0 && valor < prefabs.Length)
                {

                    if (valor > 3)
                    {
                        Instantiate(prefabs[0], new Vector3((x + 0.5f )* alturaImagen, (y + 0.5f) * alturaImagen, 0), Quaternion.identity, padre.transform);
                    }
                    Instantiate(prefabs[valor], new Vector3((x + 0.5f) * alturaImagen, (y+0.5f)*alturaImagen,0), Quaternion.identity, padre.transform);                    
                }
            }
        }
    }
}