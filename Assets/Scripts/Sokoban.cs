using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sokoban : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject padre;

    public int alturaImagen = 60;
    public const int tamanyo = 10;
    public int[,] datosMapaVisual;
    public int[,] datosMapaColision;

    public List<GameObject> cajas;
    private int levelActual;
    public Level nivel;
    public bool partidaIniciada;

    private void Start()
    {        
        partidaIniciada = false;
        AsignarValoresIniciales();        
        MostrarMapa();
        partidaIniciada = true;
    }

    public void CambiaNivel()
    {
        partidaIniciada = false;
        QuitarEscenario();
        levelActual++;
        nivel.CargarNivel(levelActual);
        MostrarMapa();
        Debug.Log("Se inicia otro Nivel");
        partidaIniciada = true;
    }

    void QuitarEscenario()
    {
        Transform[] hijos = padre.GetComponentsInChildren<Transform>();
        foreach (Transform hijo in hijos)
        {
            if (hijo.gameObject != padre)
            {
                if (cajas.Contains(hijo.gameObject))
                {
                    cajas.Remove(hijo.gameObject);
                }
                Destroy(hijo.gameObject);
            }
        }
        cajas.Clear();  
    }


    void AsignarValoresIniciales()
    {
        datosMapaVisual = new int[tamanyo, tamanyo];
        datosMapaColision = new int[tamanyo, tamanyo];

        levelActual = 1;
        nivel.CargarNivel(levelActual);
    }

    void MostrarMapa()
    {
        for (int x = 0; x < tamanyo; x++)
        {
            for (int y = 0; y < tamanyo; y++)
            {
                int valor = datosMapaVisual[x, y];
                if (valor >= 0 && valor < prefabs.Length)
                {                    
                    if (valor > 3)
                    {
                        Instantiate(prefabs[0], new Vector3((x + 0.5f )* alturaImagen, (y + 0.5f) * alturaImagen, 0), Quaternion.identity, padre.transform);
                    }
                    else
                    {
                        Instantiate(prefabs[valor], new Vector3((x + 0.5f) * alturaImagen, (y + 0.5f) * alturaImagen, 0), Quaternion.identity, padre.transform);
                    }
                }
            }
        }

        for (int x = 0; x < tamanyo; x++)
        {
            for (int y = 0; y < tamanyo; y++)
            {
                int valor = datosMapaVisual[x, y];
                if (valor >= 0 && valor < prefabs.Length)
                {                    
                    if (valor > 3)
                    {
                        GameObject objeto;
                        objeto = Instantiate(prefabs[valor], new Vector3((x + 0.5f) * alturaImagen, (y + 0.5f) * alturaImagen, 0), Quaternion.identity, padre.transform);

                        if (valor == 4)
                        {
                            Debug.Log("Caja en:" + x + "," + y);
                            Debug.Log("Caja position:" + objeto.transform.position);
                            cajas.Add(objeto);
                        }
                    }
                }
            }
        }
    }
}