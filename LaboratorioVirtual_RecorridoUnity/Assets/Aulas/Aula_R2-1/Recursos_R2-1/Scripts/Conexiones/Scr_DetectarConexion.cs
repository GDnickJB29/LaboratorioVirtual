using UnityEngine;

public class Scr_DetectarConexion : MonoBehaviour
{
    public bool polo;
    public conexionesDisponibles nombreConexion;
    public Scr_ComponentesFunsion padre;

    public enum conexionesDisponibles
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3
    }

    private void OnTriggerEnter(Collider other)
    {
        Scr_DetectarConexion detectarConexion = other.GetComponent<Scr_DetectarConexion>(); // Este script del objeto tocado

        if (detectarConexion == null)
        {
            Debug.LogWarning($"{other.name} no tiene el script 'Scr_DetectarConexion'.");
            return;
        }

        if (padre == null)
        {
            Debug.LogWarning($"El objeto '{gameObject.name}' no tiene asignado un padre en el Inspector.");
            return;
        }

        padre.Colisionando(nombreConexion, other, detectarConexion.polo); // Avisar al padre de la colisión
    }

    private void OnTriggerExit(Collider other)
    {
        Scr_DetectarConexion detectarConexion = other.GetComponent<Scr_DetectarConexion>(); // Este script del objeto tocado

        if (detectarConexion == null)
        {
            Debug.LogWarning($"{other.name} no tiene el script 'Scr_DetectarConexion'.");
            return;
        }

        if (padre == null)
        {
            Debug.LogWarning($"El objeto '{gameObject.name}' no tiene asignado un padre en el Inspector.");
            return;
        }

        padre.Descolisionando(nombreConexion, other, detectarConexion.polo); // Avisar al padre de que se dejó de tocar
    }
}
