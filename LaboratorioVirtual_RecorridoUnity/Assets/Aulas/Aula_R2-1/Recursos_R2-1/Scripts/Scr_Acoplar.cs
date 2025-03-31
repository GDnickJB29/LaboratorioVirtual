using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_Acoplar : MonoBehaviour
{


    public bool polo; // Variable booleana del objeto actual

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el otro objeto tiene la etiqueta "polo"
        if (other.CompareTag("PoloTag"))
        {
           
        }


    }
}
