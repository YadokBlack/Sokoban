using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionMapa : MonoBehaviour
{
    public Sokoban sokobanScript;
    const int sinColision = 0;
    const int colisionMovil = 1;
    const int conColision = 2;

    public ColisionMapa(Sokoban soko)
    {
        sokobanScript = soko;
    }

    public void MueveCajaSiColisiona(Vector3 position, Vector2 direccion)
    {
        if (ColisionaConCaja(direccion))
        {
            MoverCaja(transform.position, direccion);
        }
    }

    private bool ColisionaConCaja(Vector2 direccion)
    {
        return ValorCasillaColision(transform.position, direccion) == colisionMovil;
    }

    private int ValorCasillaColision(Vector3 position, Vector2 direccion)
    {
        int siguienteX = SiguientePosicion(position.x, direccion.x);
        int siguienteY = SiguientePosicion(position.y, direccion.y);
        int valor = sokobanScript.datosMapaColision[siguienteX, siguienteY];
        return valor;
    }

    public bool Bloqueado(Vector3 position, Vector2 direccion)
    {
        int siguienteX = SiguientePosicion(position.x, direccion.x);
        int siguienteY = SiguientePosicion(position.y, direccion.y);

        if (FueraDeRango(siguienteX, siguienteY)) return false;

        int valor = sokobanScript.datosMapaColision[siguienteX, siguienteY];
        return (ColisionaConCaja(valor)) ? ColisionDetrasCaja(siguienteX, siguienteY, direccion) : ColisionaConMuro(valor);
    }

    private static bool ColisionaConCaja(int valor)
    {
        return valor == colisionMovil;
    }

    private static bool ColisionaConMuro(int valor)
    {
        return valor == conColision;
    }

    private bool ColisionDetrasCaja(int siguienteX, int siguienteY, Vector2 direccion)
    {
        int siguiente2X = siguienteX + Mathf.RoundToInt(direccion.x);
        int siguiente2Y = siguienteY + Mathf.RoundToInt(direccion.y);

        if (FueraDeRango(siguiente2X, siguiente2Y)) return false;

        int siguiente2Valor = sokobanScript.datosMapaColision[siguiente2X, siguiente2Y];
        return ColisionaConMuro(siguiente2Valor) || ColisionaConCaja(siguiente2Valor);
    }

    private static bool FueraDeRango(int siguienteX, int siguienteY)
    {
        return siguienteX < 0 || siguienteX >= Sokoban.tamanyo || siguienteY < 0 || siguienteY >= Sokoban.tamanyo;
    }
    private int SiguientePosicion(float position, float direccion)
    {
        return Mathf.FloorToInt(position / sokobanScript.alturaImagen) + Mathf.RoundToInt(direccion);
    }

    public void MoverCaja(Vector3 position, Vector2 direccion)
    {
        int siguienteX = SiguientePosicion(position.x, direccion.x);
        int siguienteY = SiguientePosicion(position.y, direccion.y);
        Debug.Log("Caja en:" + siguienteX + "," + siguienteY);
        float realX = (siguienteX + 0.5f) * sokobanScript.alturaImagen;
        float realY = (siguienteY + 0.5f) * sokobanScript.alturaImagen;
        float diferencia = sokobanScript.alturaImagen * 0.25f;
        GameObject caja = sokobanScript.cajas.Find(c => Vector2.Distance(new Vector2(c.transform.position.x, c.transform.position.y), new Vector2(realX, realY)) < diferencia);

        if (caja == null) Debug.LogError("No encontrada!!");

        DesplazarCaja(direccion, siguienteX, siguienteY, caja);
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
}
