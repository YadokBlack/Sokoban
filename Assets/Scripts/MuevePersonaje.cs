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
    public ContadorPasos contadorPasos;
    
    private void Start()
    {        
        contadorPasos = FindObjectOfType<ContadorPasos>();
        if (contadorPasos == null ) 
        {
            Debug.LogError("No se encontró el Contador de Pasos.");
        }
        contadorPasos.Inicializar();
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
            DesplazaPersonaje(direccion);            
        }
    }

    private void DesplazaPersonaje(Vector2 direccion)
    {
        if (ColisionaConCaja(direccion))
        {
            MoverCaja(transform.position, direccion);
        }
        Vector2 movimiento = direccion * sokobanScript.alturaImagen;
        transform.Translate(movimiento);
        contadorPasos.Aumenta();
    }

    private bool ColisionaConCaja(Vector2 direccion)
    {
        return ValorCasillaColision(transform.position, direccion) == colisionMovil;
    }

    int ValorCasillaColision(Vector3 position, Vector2 direccion)
    {
        int siguienteX = SiguientePosicion(position.x, direccion.x);
        int siguienteY = SiguientePosicion(position.y, direccion.y);
        int valor = sokobanScript.datosMapaColision[siguienteX, siguienteY];
        return valor;
    }

    bool Bloqueado(Vector3 position, Vector2 direccion)
    {
        int siguienteX = SiguientePosicion(position.x, direccion.x);
        int siguienteY = SiguientePosicion(position.y, direccion.y);

        if (FueraDeRango(siguienteX, siguienteY)) return false;
        
        int valor = sokobanScript.datosMapaColision[siguienteX, siguienteY];

        if (valor == colisionMovil)
        {
            int siguiente2X = siguienteX + Mathf.RoundToInt(direccion.x);
            int siguiente2Y = siguienteY + Mathf.RoundToInt(direccion.y);
            
            if (FueraDeRango(siguiente2X, siguiente2Y)) return false;
            
            int siguiente2Valor = sokobanScript.datosMapaColision[siguiente2X, siguiente2Y];
            return siguiente2Valor == conColision || siguiente2Valor == colisionMovil;
        }
        return valor == conColision;
    }

    private static bool FueraDeRango(int siguienteX, int siguienteY)
    {
        return siguienteX < 0 || siguienteX >= Sokoban.tamanyo || siguienteY < 0 || siguienteY >= Sokoban.tamanyo;
    }

    void MoverCaja(Vector3 position, Vector2 direccion)
    {
        int siguienteX = SiguientePosicion(position.x, direccion.x);
        int siguienteY = SiguientePosicion(position.y, direccion.y);
        Debug.Log("Caja en:" + siguienteX + "," + siguienteY);
        float realX = (siguienteX + 0.5f) * sokobanScript.alturaImagen;
        float realY = (siguienteY + 0.5f) * sokobanScript.alturaImagen;
        float diferencia = sokobanScript.alturaImagen * 0.25f;
        GameObject caja = sokobanScript.cajas.Find(c => Vector2.Distance(new Vector2(c.transform.position.x, c.transform.position.y), new Vector2(realX, realY)) < diferencia);

        if (caja != null)
        {
            DesplazarCaja(direccion, siguienteX, siguienteY, caja);
        }
        else
        {
            Debug.Log("No encontrada!!");
        }
    }

    private void DesplazarCaja(Vector2 direccion, int siguienteX, int siguienteY, GameObject caja)
    {
        int siguiente2X = siguienteX + Mathf.RoundToInt(direccion.x);
        int siguiente2Y = siguienteY + Mathf.RoundToInt(direccion.y);
        sokobanScript.datosMapaColision[siguienteX, siguienteY] = sinColision;
        sokobanScript.datosMapaColision[siguiente2X, siguiente2Y] = colisionMovil;
        float real2X = (siguiente2X + 0.5f) * sokobanScript.alturaImagen;
        float real2Y = (siguiente2Y + 0.5f) * sokobanScript.alturaImagen;
        caja.transform.position = new Vector3(real2X, real2Y, caja.transform.position.z);
    }

    private int SiguientePosicion(float position, float direccion)
    {
        return Mathf.FloorToInt(position / sokobanScript.alturaImagen) + Mathf.RoundToInt(direccion);
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
}

