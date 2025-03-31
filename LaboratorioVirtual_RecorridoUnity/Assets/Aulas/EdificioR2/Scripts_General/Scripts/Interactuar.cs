using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para gestionar las escenas

public class Interactuar : MonoBehaviour
{
    [SerializeField] private GameObject tutorialClicDerecho;
    [SerializeField] private GameObject mano;
    [SerializeField] private MeshFilter mallaMano;
    [SerializeField] private Mesh mallaInteraccion;
    [SerializeField] private Mesh mallaDefecto;
    [SerializeField] private string nombreEscena;  // Agregar un campo para el nombre de la escena

    private bool enContacto = false;

    void Start()
    {
        tutorialClicDerecho.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clic izquierdo detectado.");
        }

        // Debug.Log("Estado de enContacto: " + enContacto); // Verifica el estado en consola

        if (enContacto && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Llamando a CambiarEscena...");
            CambiarEscena();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouse"))
        {
            enContacto = true;
            tutorialClicDerecho.SetActive(true);
            mallaMano.mesh = mallaInteraccion;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mouse"))
        {
            enContacto = false;
            tutorialClicDerecho.SetActive(false);
            mallaMano.mesh = mallaDefecto;
        }
    }

    private void CambiarEscena()
    {
        // Cambiar a la escena especificada por el nombre
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);  // Carga la escena usando el nombre
            Debug.Log("Cambiando a la escena: " + nombreEscena);
        }
        else
        {
            Debug.Log("No se ha asignado un nombre de escena.");
        }
    }
}
