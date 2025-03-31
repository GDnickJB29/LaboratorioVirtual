using UnityEngine;

public class ControlesInteractivos : MonoBehaviour
{
    [SerializeField] private GameObject tutorial, techo;
    private bool seActivoTecho = true, seActivoTuto = true;

    void Update()
    {
        // Activar o desactivar el techo con la tecla O
        if (Input.GetKeyDown(KeyCode.E))
        {
            seActivoTecho = !seActivoTecho;
            techo.SetActive(seActivoTecho);
        }

        // Activar o desactivar el tutorial con la tecla Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            seActivoTuto = !seActivoTuto;
            tutorial.SetActive(seActivoTuto);
        }
    }
}
