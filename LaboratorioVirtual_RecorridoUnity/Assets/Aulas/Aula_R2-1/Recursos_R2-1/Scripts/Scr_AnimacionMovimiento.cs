using UnityEngine;

public class Scr_AnimacionMovimiento : MonoBehaviour
{
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
    }

    void Update()
    {
        tiempo += Time.deltaTime * velocidad;
        float tiempoAjustado = tiempo + offsetTiempo;

        // Estiramiento de horizontal a vertical sin sobrepasar el mínimo
        float factor = (Mathf.Sin(tiempoAjustado) + 1) / 2; // Valor entre 0 y 1
        float escalaX = Mathf.Lerp(escalaInicial.x, escalaInicial.x + rango, factor);
        float escalaY = Mathf.Lerp(escalaInicial.y, escalaInicial.y + rango, factor);
        transform.localScale = new Vector3(escalaX, escalaY, escalaInicial.z);

        // Rotación oscilatoria con dirección aleatoria sin afectar la rotación original en otros ejes
        float rotacionZ = Mathf.Sin(tiempoAjustado) * 15f * rango * 4 * direccionRotacion; // Ángulo de rotación en Z
        transform.rotation = rotacionInicial * Quaternion.Euler(0, 0, rotacionZ);
    }
}
