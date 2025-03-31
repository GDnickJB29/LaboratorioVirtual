using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class Scr_Mouse : MonoBehaviour
{
    [SerializeField] private Mesh malla1; // Malla por defecto
    [SerializeField] private Mesh malla2; // Malla al entrar en el trigger
    [SerializeField] private Material textura1; // Textura por defecto
    [SerializeField] private Material textura2; // Textura al entrar en el trigger
    [SerializeField] private GameObject mouseIcono; // Canva del icono de mouse

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private float moverEnEjeX, moverEnEjeY;
    private Vector3 moverMouse;
    private bool mouseBloqueado = false;
    private bool primeraVez = true;

    private void Start()
    {
        primeraVez = true;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshFilter == null)
        {
            Debug.LogError("No se encontró un MeshFilter en el objeto.");
        }
        else
        {
            meshFilter.mesh = malla1; // Iniciar con la malla1
        }

        if (meshRenderer == null)
        {
            Debug.LogError("No se encontró un MeshRenderer en el objeto.");
        }
        else
        {
            meshRenderer.material = textura1; // Iniciar con textura1
        }
    }

    private void Update()
    {
        if ((!mouseBloqueado && Input.GetMouseButtonDown(0)) || primeraVez)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseBloqueado = true;
            primeraVez = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseBloqueado = false;
        }

        if (mouseBloqueado)
        {
            movimiento();
        }
    }

    private void movimiento()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        if (deltaX != 0 || deltaY != 0)
        {
            // Obtener la nueva posición sin aplicarla aún
            float nuevaX = Mathf.Clamp(transform.position.x + deltaX, -12f, 12f);
            float nuevaZ = Mathf.Clamp(transform.position.z + deltaY, -6f, 8f);

            // Aplicar la nueva posición dentro de los límites
            transform.position = new Vector3(nuevaX, transform.position.y, nuevaZ);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (meshFilter != null && malla2 != null)
        {
            meshFilter.mesh = malla2; // Cambia a malla2 al entrar en el trigger
        }

        if (meshRenderer != null && textura2 != null)
        {
            meshRenderer.material = textura2; // Cambia a textura2 al entrar en el trigger
        }

        mouseIcono.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (meshFilter != null && malla1 != null)
        {
            meshFilter.mesh = malla1; // Vuelve a malla1 al salir del trigger
        }

        if (meshRenderer != null && textura1 != null)
        {
            meshRenderer.material = textura1; // Vuelve a textura1 al salir del trigger
        }
        mouseIcono.SetActive(false);
    }
}
