using UnityEngine;
using UnityEngine.SceneManagement;

public class ClicCambiarDelNivel : MonoBehaviour
{
    [SerializeField] private string escenaDestino;
    private bool dentroDelTrigger = false;

    void OnTriggerEnter(Collider other)
    {
        dentroDelTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        dentroDelTrigger = false;
    }

    void Update()
    {
        if (dentroDelTrigger && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(escenaDestino);
            Debug.Log("Cambiando a la escena: " + escenaDestino);
        }
    }
}
