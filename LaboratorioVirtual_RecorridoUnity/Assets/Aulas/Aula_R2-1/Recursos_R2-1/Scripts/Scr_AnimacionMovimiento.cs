using UnityEngine;

public class Scr_AnimacionMovimiento : MonoBehaviour
{
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
    }

    void Update()
    {
        tiempo += Time.deltaTime * velocidad;
        float tiempoAjustado = tiempo + offsetTiempo;

        // Estiramiento de horizontal a vertical sin sobrepasar el m�nimo
        float factor = (Mathf.Sin(tiempoAjustado) + 1) / 2; // Valor entre 0 y 1
        float escalaX = Mathf.Lerp(escalaInicial.x, escalaInicial.x + rango, factor);
        float escalaY = Mathf.Lerp(escalaInicial.y, escalaInicial.y + rango, factor);
        transform.localScale = new Vector3(escalaX, escalaY, escalaInicial.z);

        // Rotaci�n oscilatoria con direcci�n aleatoria sin afectar la rotaci�n original en otros ejes
        float rotacionZ = Mathf.Sin(tiempoAjustado) * 15f * rango * 4 * direccionRotacion; // �ngulo de rotaci�n en Z
        transform.rotation = rotacionInicial * Quaternion.Euler(0, 0, rotacionZ);
    }
}
