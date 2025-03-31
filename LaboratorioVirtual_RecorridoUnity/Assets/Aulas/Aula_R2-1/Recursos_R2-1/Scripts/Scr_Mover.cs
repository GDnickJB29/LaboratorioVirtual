using UnityEngine;

public class Scr_Mover : MonoBehaviour
{
    [SerializeField] private float fuerzaGiro = 25;
    public string targetTag = "Mouse";
    private float moverEnEjeX, moverEnEjeY, direccionManecillas, posicionRotar;
    private Vector3 posicionComponente;
    private bool estaEncima = false;
    private static Scr_Mover objetoEnMovimiento = null; // Solo un objeto a la vez

    void Start()
    {
        moverEnEjeX = transform.position.x;
        moverEnEjeY = transform.position.z;
        posicionComponente = transform.position;
        posicionRotar = 0;




        Vector3 nuevaBase = transform.eulerAngles; // Guarda la rotación actual
        transform.rotation = Quaternion.identity; // Resetea la rotación
        transform.Rotate(nuevaBase); // Aplica la rotación anterior como nueva base

    }

    void Update()
    {
        if (estaEncima && objetoEnMovimiento == this)
        {
            if (Input.GetMouseButton(0))
            {
                Movimiento();
                direccionManecillas = Input.GetAxis("Mouse ScrollWheel");
                Rotar(direccionManecillas);
            }

            if (Input.GetMouseButtonUp(0))
            {
                estaEncima = false;
                objetoEnMovimiento = null; // Libera el control
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(targetTag) && objetoEnMovimiento == null)
        {
            Debug.Log(gameObject.name + " es el único en colisión");
            estaEncima = true;
            objetoEnMovimiento = this; // Solo este objeto puede moverse
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag(targetTag) && objetoEnMovimiento == this)
        {
            estaEncima = false;
            objetoEnMovimiento = null; // Libera el control al salir
        }
    }

    private void Movimiento()
    {
        moverEnEjeX += Input.GetAxis("Mouse X");
        moverEnEjeY += Input.GetAxis("Mouse Y");

        posicionComponente.x = moverEnEjeX;
        posicionComponente.z = moverEnEjeY;
        transform.position = posicionComponente;
    }

    private void Rotar(float rota)
    {
        
            posicionRotar += rota * fuerzaGiro;


        transform.rotation = Quaternion.Euler(0, posicionRotar, 0);
    }
}
