using UnityEngine;

public class Scr_SeguirReferencia : MonoBehaviour
{
    // Referencia al objeto de referencia (por ejemplo, el jugador)
    [SerializeField] private Transform objetoReferencia;
    [SerializeField] private float velocidadRotacion = 500f; // Velocidad de rotaci�n para seguir al objeto

    // Par�metros de la animaci�n de movimiento
    [SerializeField] float rango = 0.05f;
    [SerializeField] float velocidad = 2f; // Velocidad de la animaci�n

    private Vector3 escalaInicial;
    private Quaternion rotacionInicial;
    private float tiempo;
    private float direccionRotacion;
    private float offsetTiempo;

    void Start()
    {
        escalaInicial = transform.localScale;
        rotacionInicial = transform.rotation;
        direccionRotacion = Random.Range(0, 2) == 0 ? 1f : -1f; // Direcci�n aleatoria de rotaci�n
        offsetTiempo = Random.Range(0f, Mathf.PI * 2f); // Desfase aleatorio para el inicio de la animaci�n

        // Si no se asigna un objeto de referencia, se muestra un mensaje de advertencia
        if (objetoReferencia == null)
        {
            Debug.LogWarning("No se ha asignado un objeto de referencia. Asigna uno en el inspector.");
        }
    }

    void Update()
    {
        // 1. Animaci�n de escala (oscila entre el valor inicial y el rango)
        tiempo += Time.deltaTime * velocidad;
        float tiempoAjustado = tiempo + offsetTiempo;

        float factor = (Mathf.Sin(tiempoAjustado) + 1) / 2; // Valor entre 0 y 1
        float escalaX = Mathf.Lerp(escalaInicial.x, escalaInicial.x + rango, factor);
        float escalaY = Mathf.Lerp(escalaInicial.y, escalaInicial.y + rango, factor);
        transform.localScale = new Vector3(escalaX, escalaY, escalaInicial.z);

        // 2. Rotaci�n oscilatoria con direcci�n aleatoria en el eje Z
        float rotacionZ = Mathf.Sin(tiempoAjustado) * 15f * rango * 4 * direccionRotacion; // �ngulo de rotaci�n en Z
        transform.rotation = rotacionInicial * Quaternion.Euler(0, 0, rotacionZ);

        // 3. Rotaci�n para seguir al objeto de referencia (solo en el eje Y)
        if (objetoReferencia != null)
        {
            // Obtener la direcci�n desde el objeto hacia el objeto de referencia
            Vector3 direccion = objetoReferencia.position - transform.position;

            // Asegurarse de que el objeto solo gire alrededor del eje Y
            direccion.y = 0; // Descartar componente Y para evitar rotaci�n vertical

            if (direccion.sqrMagnitude > 0.01f)
            {
                // Calcular la rotaci�n hacia el objeto de referencia solo en el eje Y
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);

                // Forzar la rotaci�n solo alrededor del eje Y
                rotacionObjetivo.eulerAngles = new Vector3(0, rotacionObjetivo.eulerAngles.y, 0);

                // Aplicar una rotaci�n suave (interpolaci�n)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
            }
        }
    }
}
