using UnityEngine;
using UnityEngine.UI;

public class PushPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 targetPosition;
    public float speed = 5f;
    public float jumpForce = 7f;
    private float time;
    private Rigidbody rb;
    private float lastPlayerY;
    public float pushForce = 10f; // Fuerza con la que empujará al jugador
    public float rotationSpeed = 5f; // Velocidad de rotación del enemigo hacia el jugador

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        if (playerTransform != null)
        {
            lastPlayerY = playerTransform.position.y; // Guarda la posición inicial del jugador en Y
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Guardar la posición del jugador en los tres ejes
        targetPosition = playerTransform.position;

        // Moverse suavemente hacia el jugador en X, Y y Z
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // Hacer que el enemigo gire hacia el jugador
        RotateTowardsPlayer();

        // Si el jugador sube significativamente en Y, hacer saltar este objeto
        if (playerTransform.position.y > lastPlayerY + 0.2f)
        {
            Jump();
        }

        // Actualizar la última posición en Y del jugador
        lastPlayerY = playerTransform.position.y;

        // Contador de tiempo para destruir el objeto después de 3 segundos
        time += Time.deltaTime;
        if (time >= 3f)
        {
            Destroy(gameObject);
        }
    }

    void Jump()
    {
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset en Y para evitar acumulación
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Hacer que el enemigo gire suavemente hacia el jugador
    void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0f; // Mantener la rotación solo en el plano XZ (evita inclinaciones raras)

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Detectar colisión con el jugador y empujarlo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Enemigo tocó al jugador y lo empuja!");

            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Calcular la dirección de empuje desde el enemigo hacia el jugador
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0f; // Evita empujarlo demasiado alto

                // Aplicar fuerza de empuje
                playerRb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
            }
        }
    }
}


