using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sokoban : MonoBehaviour
{
    public NextLevel proximoNivel;
    public GameObject[] prefabs;
    public GameObject padre;
    public int alturaImagen = 60;
    public const int tamanyo = 10;
    public int[,] datosMapaVisual;
    public int[,] datosMapaColision;
    public List<GameObject> cajas;
    public bool partidaIniciada;
    const int Caja = 4;

    private void Start()
    {        
        partidaIniciada = false;
        AsignarValoresIniciales();
    }

    public void IniciaContarTiempo()
    {
        proximoNivel.nivel.contadorTiempo.IniciarCuenta();
    }

    public void QuitarEscenario()
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

        //   proximoNivel.nivel.Inicializar();
        //   proximoNivel.nivel.Cargar();
        //   MostrarMapa();
        proximoNivel.CargaLevel();
    }

    public void MostrarMapa()
    {
        for (int x = 0; x < tamanyo; x++)
        {
            for (int y = 0; y < tamanyo; y++)
            {
                InstanciarObjetosFijos(x, y);
            }
        }
        for (int x = 0; x < tamanyo; x++)
        {
            for (int y = 0; y < tamanyo; y++)
            {
                InstanciarObjetosMoviles(x, y);
            }
        }
        partidaIniciada = true;
    }

    private void InstanciarObjetosMoviles(int x, int y)
    {
        int valor = datosMapaVisual[x, y];
        if (cantidadValida(valor))
        {
            if (ObjetosMoviles(valor))
            {
                InstanciaObjetoMovil(x, y, valor);
            }
        }
    }

    private void InstanciaObjetoMovil(int x, int y, int valor)
    {
        GameObject objeto = Instantiate(prefabs[valor], new Vector3((x + 0.5f) * alturaImagen, (y + 0.5f) * alturaImagen, 0), Quaternion.identity, padre.transform);

        if (valor == Caja)
        {
            // Debug.Log("Caja en:" + x + "," + y);
            // Debug.Log("Caja position:" + objeto.transform.position);
            cajas.Add(objeto);
        }
    }

    private static bool ObjetosMoviles(int valor)
    {
        return valor > 3;
    }

    private bool cantidadValida(int valor)
    {
        return valor >= 0 && valor < prefabs.Length;
    }

    private void InstanciarObjetosFijos(int x, int y)
    {
        int valor = datosMapaVisual[x, y];
        if (cantidadValida(valor))
        {
            if (ObjetosMoviles(valor))
            {
                Instantiate(prefabs[0], new Vector3((x + 0.5f) * alturaImagen, (y + 0.5f) * alturaImagen, 0), Quaternion.identity, padre.transform);
            }
            else
            {
                Instantiate(prefabs[valor], new Vector3((x + 0.5f) * alturaImagen, (y + 0.5f) * alturaImagen, 0), Quaternion.identity, padre.transform);
            }
        }
    }
}