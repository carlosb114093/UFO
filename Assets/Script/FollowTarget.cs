using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public string targetTag = "Player"; // Etiqueta del objeto a seguir
    public Vector3 offset = new Vector3(0, 5, -10); // Desplazamiento de la cámara
    private Transform target;

    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con la etiqueta " + targetTag);
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target); // La cámara mira al objetivo
        }
    }
}