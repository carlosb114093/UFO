using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeSwitcher : MonoBehaviour
{
    public GameObject tank; // Referencia al tanque
    public GameObject ufo; // Referencia al UFO
    private bool isControllingTank = true; // Controla qué vehículo está activo

    void Start()
    {
        SetActiveVehicle(true); // Comenzar con el tanque activo
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(""))
        {
            SwitchVehicle();
            Destroy(other.gameObject); // Destruir power-up después de usarlo
        }
    }

    void SwitchVehicle()
    {
        isControllingTank = !isControllingTank;
        SetActiveVehicle(isControllingTank);
    }

    void SetActiveVehicle(bool controlTank)
    {
        // Activar el tanque y desactivar el UFO, o viceversa
        tank.GetComponent<PhysicsMovement>().enabled = controlTank;
        tank.GetComponent<Rigidbody>().isKinematic = !controlTank; // Evita colisiones cuando está inactivo

        ufo.GetComponent<PhysicsMovement>().enabled = !controlTank;
        ufo.GetComponent<Rigidbody>().isKinematic = controlTank;
    }
}    
