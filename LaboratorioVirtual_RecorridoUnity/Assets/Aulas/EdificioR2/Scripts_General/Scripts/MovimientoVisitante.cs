using UnityEngine;

public class MovimientoVisitante : MonoBehaviour
{
    public CharacterController controlador;
    public float rapidez = 5f;
    public float gravedad = -9.8f;
    public float fuerzaSalto = 3f;
    public Transform chcadorSuelo;
    public float distanciaSuelo= 0.3f;
    public LayerMask mascaraSuelo;

    Vector3 velocidad;

    private bool tocoSuelo;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        tocoSuelo = Physics.CheckSphere(chcadorSuelo.position, distanciaSuelo, mascaraSuelo);


        if (tocoSuelo && velocidad.y < 0f)
        {
            velocidad.y = -2f;
        }




        float x = Input.GetAxis("Horizontal"); //movimiento horizontal "ad" o flecha arriva y abajo
        float z = Input.GetAxis("Vertical"); // movimiento verical "ws" o flecha izquierda y derecha 

        Vector3 move = transform.right * x + transform.forward * z;


        if (Input.GetKey(KeyCode.LeftShift))
        {
        controlador.Move(move * rapidez * 2f * Time.deltaTime);

        }
        else
        {
        controlador.Move(move * rapidez * Time.deltaTime);
            
        }



        if (Input.GetButtonDown("Jump") && tocoSuelo)
        {
            velocidad.y = Mathf.Sqrt(fuerzaSalto * -2 * gravedad);
        }

        velocidad.y += gravedad * Time.deltaTime;

        controlador.Move(velocidad * Time.deltaTime);

    }
}
