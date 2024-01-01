using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuevePersonaje : MonoBehaviour
{
    public Sokoban sokobanScript;
    public ContadorPasos contadorPasos;
    public ColisionMapa colisionMapa;
    public float velocidad = 1.0f;    
    bool puedeMover;
     
    private void Start()
    {
        AsignarObjetos();
        contadorPasos.Inicializar();
        puedeMover = false;
    }

    private void AsignarObjetos()
    {
        sokobanScript = FindObjectOfType<Sokoban>();
        if (sokobanScript == null)
        {
            Debug.LogError("No se encontró el script Sokoban en la escena.");
        }
        contadorPasos = FindObjectOfType<ContadorPasos>();
        if (contadorPasos == null)
        {
            Debug.LogError("No se encontró el Contador de Pasos.");
        }
        colisionMapa = new ColisionMapa(sokobanScript);     
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
        if(!colisionMapa.Bloqueado(transform.position, direccion))
        {
            DesplazaPersonaje(direccion);            
        }
    }

    private void DesplazaPersonaje(Vector2 direccion)
    {
        colisionMapa.MueveCajaSiColisiona(transform.position, direccion);

        Vector2 movimiento = direccion * sokobanScript.alturaImagen;
        transform.Translate(movimiento);

        if (contadorPasos.Inicio()) sokobanScript.nivel.IniciaTiempoPartida();
        contadorPasos.Aumenta();
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