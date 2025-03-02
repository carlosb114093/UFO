
using UnityEngine;

public class PisoMovil : MonoBehaviour
{
    public float velocidad = 2f; // Velocidad del movimiento
    public float distancia = 30f; // Distancia máxima de movimiento

    private Vector3 posicionInicial;
    private int direccion = 1;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Mueve el piso de arriba a abajo
        transform.position += Vector3.up * velocidad * direccion * Time.deltaTime;

        // Cambia la dirección si alcanza la distancia máxima
        if (Mathf.Abs(transform.position.y - posicionInicial.y) >= distancia)
        {
            direccion *= -1;
        }
    }
}
