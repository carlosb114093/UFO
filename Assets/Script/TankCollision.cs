using UnityEngine;
using TMPro;

public class TankCollision : MonoBehaviour
{
    public float initialSpeed = 20f; // Velocidad inicial del tanque
    private float currentSpeed;
    private int hitCount = 0; // Contador de colisiones
    public TextMeshProUGUI hitCounterText; // Referencia al TextMeshPro

    void Start()
    {
        currentSpeed = initialSpeed;
        UpdateHitCounterText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Asegúrate de que el enemigo tenga el tag "Enemy"
        {
            hitCount++;
            currentSpeed *= 0.8f; // Reduce la velocidad en un 20%
            UpdateHitCounterText();
            Debug.Log("El tanque fue golpeado " + hitCount + " veces. Velocidad actual: " + currentSpeed);
        }
    }

    void UpdateHitCounterText()
    {
        if (hitCounterText != null)
        {
            hitCounterText.text = "Golpes: " + hitCount;
        }
    }
}

