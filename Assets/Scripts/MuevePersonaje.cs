using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuevePersonaje : MonoBehaviour
{
    public float velocidad = 1.0f;
    public Sokoban sokobanScript;
    bool puedeMover;

    const int sinColision = 0;
    const int colisionMovil = 1;
    const int conColision = 2;

    private void Start()
    {
        puedeMover = false;
        sokobanScript = FindObjectOfType<Sokoban>();
        if (sokobanScript == null)
        {
            Debug.LogError("No se encontró el script Sokoban en la escena.");
        }
    }

    private void Update()
    {
        MoverPersonaje();
    }

    public void Mover(Vector2 direccion)
    {
        if (Mathf.Abs(direccion.x) < 0.5f)
        {
            direccion.x = 0;
        }
        else
        {
            direccion.y = 0;
        }
        direccion.Normalize();
        if(!Bloqueado(transform.position, direccion))
        {           
            if (ValorCasilla(transform.position, direccion) == 1)
            {
                Debug.Log("Caja para mover!.");
                MoverCaja(transform.position, direccion);
            }
            Vector2 movimiento = direccion * sokobanScript.alturaImagen;
            transform.Translate(movimiento);
        }
    }

    int ValorCasilla(Vector3 position, Vector2 direccion)
    {
        int x = Mathf.FloorToInt(position.x / sokobanScript.alturaImagen);
        int y = Mathf.FloorToInt(position.y / sokobanScript.alturaImagen);

        int siguienteX = x + Mathf.RoundToInt(direccion.x);
        int siguienteY = y + Mathf.RoundToInt(direccion.y);

        int valor = sokobanScript.datosMapaColision[siguienteX, siguienteY];
        return valor;
    }

    bool Bloqueado(Vector3 position, Vector2 direccion)
    {
        int x = Mathf.FloorToInt(position.x / sokobanScript.alturaImagen);
        int y = Mathf.FloorToInt(position.y / sokobanScript.alturaImagen);

        int siguienteX = x + Mathf.RoundToInt(direccion.x);
        int siguienteY = y + Mathf.RoundToInt(direccion.y);

        Debug.Log("Siguiente Coordenadas: " + siguienteX + ", " + siguienteY);

        if (siguienteX < 0 || siguienteX >= Sokoban.tamanyo || siguienteY < 0 || siguienteY >= Sokoban.tamanyo)
        {
            return false;
        }

        int valor = sokobanScript.datosMapaColision[siguienteX, siguienteY];
        Debug.Log("En la posicion " + siguienteX + "," + siguienteY + " = " + valor);

        if (valor == colisionMovil)
        {
            int siguiente2X = siguienteX + Mathf.RoundToInt(direccion.x);
            int siguiente2Y = siguienteY + Mathf.RoundToInt(direccion.y);

            if (siguiente2X < 0 || siguiente2X >= Sokoban.tamanyo || siguiente2Y < 0 || siguiente2Y >= Sokoban.tamanyo)
            {
                return false;
            }

            int siguiente2Valor = sokobanScript.datosMapaColision[siguiente2X, siguiente2Y];
            Debug.Log("Valor2 = " + siguiente2Valor);

            return siguiente2Valor == conColision;
        }
        return valor == conColision;
    }

    void MoverCaja(Vector3 position, Vector2 direccion)
    {
        int siguienteX = Mathf.FloorToInt(position.x / sokobanScript.alturaImagen) + Mathf.RoundToInt(direccion.x);
        int siguienteY = Mathf.FloorToInt(position.y / sokobanScript.alturaImagen) + Mathf.RoundToInt(direccion.y);

        Debug.Log("Caja en:" + siguienteX + "," + siguienteY);

        float realX = (siguienteX + 0.5f) * sokobanScript.alturaImagen;
        float realY = (siguienteY + 0.5f) * sokobanScript.alturaImagen;
        float diferencia = sokobanScript.alturaImagen * 0.25f;

        GameObject caja = sokobanScript.cajas.Find(c => Vector2.Distance(new Vector2(c.transform.position.x, c.transform.position.y), new Vector2(realX, realY)) < diferencia);
        
        if (caja != null )
        {
            // Debug.Log("caja encontrada!");

            int siguiente2X = siguienteX + Mathf.RoundToInt(direccion.x);
            int siguiente2Y = siguienteY + Mathf.RoundToInt(direccion.y);

            sokobanScript.datosMapaColision[siguienteX, siguienteY] = sinColision; 
            sokobanScript.datosMapaColision[siguiente2X, siguiente2Y] = colisionMovil; 

            float real2X = (siguiente2X + 0.5f) * sokobanScript.alturaImagen;
            float real2Y = (siguiente2Y + 0.5f) * sokobanScript.alturaImagen;

            caja.transform.position = new Vector3(real2X, real2Y, caja.transform.position.z);
        }
        else
        {
            Debug.Log("No encontrada!!");
        }

    }

    void MoverPersonaje()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        move.Normalize();

        if (move.sqrMagnitude > 0.5f )
        {
            if (puedeMover)
            {
                puedeMover = false;
                Mover(move);
            }
        }
        else
        {
            puedeMover = true;
        }
    }

    bool EsPosicionValida(int x, int y)
    {
        if (x < 0 || x >= Sokoban.tamanyo || y < 0 || y >= Sokoban.tamanyo)
        {
            return false; 
        }

        int valor = sokobanScript.datosMapaVisual[x, y];

        // 1 = pared  2 = arbol
        return valor != 2 && valor != 1; 
    }
}

