using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaBoundaryDetector : MonoBehaviour
{
    private bool hasExited = false;

    // Límites del área permitida
    public Vector3 boundaryMin = new Vector3(-99f, 0f, -15f);  
    public Vector3 boundaryMax = new Vector3(110f, 30f, 180f); 

    private Vector3 initialPosition; // Posición inicial del tanque

    void Start()
    {
        // Guardamos la posición inicial del tanque
        initialPosition = transform.position;
    }

    void Update()
    {
        // Verificamos si el tanque ha salido de los límites
        if (IsOutsideBoundary())
        {
            OnExitBoundary();
        }
    }

    bool IsOutsideBoundary()
    {
        return transform.position.x < boundaryMin.x || transform.position.x > boundaryMax.x ||
               transform.position.y < boundaryMin.y || transform.position.y > boundaryMax.y || 
               transform.position.z < boundaryMin.z || transform.position.z > boundaryMax.z;
    }

    void OnExitBoundary()
    {
        if (hasExited) return; // Evita múltiples llamadas
        hasExited = true;

        Debug.Log(gameObject.name + " ha salido del área permitida. Reiniciando posición...");

        // Ajustar la posición para mantenerla dentro de los límites
        Vector3 correctedPosition = transform.position;
        correctedPosition.x = Mathf.Clamp(correctedPosition.x, boundaryMin.x, boundaryMax.x);
        correctedPosition.y = Mathf.Clamp(correctedPosition.y, boundaryMin.y, boundaryMax.y);
        correctedPosition.z = Mathf.Clamp(correctedPosition.z, boundaryMin.z, boundaryMax.z);
        transform.position = correctedPosition;

        // Opcional: Reinicia la rotación para evitar giros extraños
        transform.rotation = Quaternion.identity;

        // Detener cualquier movimiento indeseado
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        hasExited = false; // Permite detectar futuras salidas
    }
}

     

