
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float speed = 10f; 
    public Vector3 minBounds = new Vector3(-95f, 0f, -12f); // Límite mínimo en X, Y, Z
    public Vector3 maxBounds = new Vector3(100f, 25f, 170f); // Límite máximo en X, Y, Z
    private Rigidbody rb;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = new Vector3(Random.Range(-90f, 90f), Random.Range(0f, 90f), Random.Range(-90f, 90f)).normalized; // Solo valores positivos en Y
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;

        // Rebote en el eje X
        if (transform.position.x <= minBounds.x || transform.position.x >= maxBounds.x)
        {
            direction.x *= -1; // Invierte la dirección en X
        }

        // Rebote en el eje Z
        if (transform.position.z <= minBounds.z || transform.position.z >= maxBounds.z)
        {
            direction.z *= -1; // Invierte la dirección en Z
        }

        // Rebote en el eje Y (0 - 25)
        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            direction.y = Mathf.Abs(direction.y); // Rebota hacia arriba
        }
        if (transform.position.y > 25f)
        {
            transform.position = new Vector3(transform.position.x, 25f, transform.position.z);
            direction.y = -Mathf.Abs(direction.y); // Rebota hacia abajo
        }
    }
}

