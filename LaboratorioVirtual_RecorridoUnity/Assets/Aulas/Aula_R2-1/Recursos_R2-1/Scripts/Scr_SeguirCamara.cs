using UnityEngine;

public class Scr_SeguirReferencia : MonoBehaviour
{
    // Referencia al objeto de referencia (por ejemplo, el jugador)
    [SerializeField] private Transform objetoReferencia;
    [SerializeField] private float velocidadRotacion = 500f; // Velocidad de rotación para seguir al objeto

    // Parámetros de la animación de movimiento
    [SerializeField] float rango = 0.05f;
    [SerializeField] float velocidad = 2f; // Velocidad de la animación

    private Vector3 escalaInicial;
    private Quaternion rotacionInicial;
    private float tiempo;
    private float direccionRotacion;
    private float offsetTiempo;

    void Start()
    {
        escalaInicial = transform.localScale;
        rotacionInicial = transform.rotation;
        direccionRotacion = Random.Range(0, 2) == 0 ? 1f : -1f; // Dirección aleatoria de rotación
        offsetTiempo = Random.Range(0f, Mathf.PI * 2f); // Desfase aleatorio para el inicio de la animación

        // Si no se asigna un objeto de referencia, se muestra un mensaje de advertencia
        if (objetoReferencia == null)
        {
            Debug.LogWarning("No se ha asignado un objeto de referencia. Asigna uno en el inspector.");
        }
    }

    void Update()
    {
        // 1. Animación de escala (oscila entre el valor inicial y el rango)
        tiempo += Time.deltaTime * velocidad;
        float tiempoAjustado = tiempo + offsetTiempo;

        float factor = (Mathf.Sin(tiempoAjustado) + 1) / 2; // Valor entre 0 y 1
        float escalaX = Mathf.Lerp(escalaInicial.x, escalaInicial.x + rango, factor);
        float escalaY = Mathf.Lerp(escalaInicial.y, escalaInicial.y + rango, factor);
        transform.localScale = new Vector3(escalaX, escalaY, escalaInicial.z);

        // 2. Rotación oscilatoria con dirección aleatoria en el eje Z
        float rotacionZ = Mathf.Sin(tiempoAjustado) * 15f * rango * 4 * direccionRotacion; // Ángulo de rotación en Z
        transform.rotation = rotacionInicial * Quaternion.Euler(0, 0, rotacionZ);

        // 3. Rotación para seguir al objeto de referencia (solo en el eje Y)
        if (objetoReferencia != null)
        {
            // Obtener la dirección desde el objeto hacia el objeto de referencia
            Vector3 direccion = objetoReferencia.position - transform.position;

            // Asegurarse de que el objeto solo gire alrededor del eje Y
            direccion.y = 0; // Descartar componente Y para evitar rotación vertical

            if (direccion.sqrMagnitude > 0.01f)
            {
                // Calcular la rotación hacia el objeto de referencia solo en el eje Y
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);

                // Forzar la rotación solo alrededor del eje Y
                rotacionObjetivo.eulerAngles = new Vector3(0, rotacionObjetivo.eulerAngles.y, 0);

                // Aplicar una rotación suave (interpolación)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
            }
        }
    }
}
