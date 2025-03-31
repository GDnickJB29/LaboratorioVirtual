using UnityEngine;

public class VistaCamara : MonoBehaviour
{

    [SerializeField] private float sensivilidadMouse = 80f;
    public Transform cuerpoVisitante;
    float xRotacion = 0;
    private bool mause_capturado; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mause_capturado = true;
    }

    // Update is called once per frame
    void Update()
    {
        


        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mause_capturado = true;
        }

        if (mause_capturado)
        {
            // movimiento con mouse
            float mouseX = Input.GetAxis("Mouse X") * sensivilidadMouse * Time.deltaTime;

            float mouseY = Input.GetAxis("Mouse Y") * sensivilidadMouse * Time.deltaTime;

            xRotacion -= mouseY;

            xRotacion = Mathf.Clamp(xRotacion, -90f, 90f);// para limitar la rotacion

            transform.localRotation = Quaternion.Euler(xRotacion, 0f, 0f); // rotar eje x

            cuerpoVisitante.Rotate(Vector3.up * mouseX);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mause_capturado = false;
        }
    }
}
