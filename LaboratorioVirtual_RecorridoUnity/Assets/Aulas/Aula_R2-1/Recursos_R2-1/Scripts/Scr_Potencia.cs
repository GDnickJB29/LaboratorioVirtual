using TMPro;
using UnityEngine;

public class Scr_Potencia : MonoBehaviour
{
    private float posicionRotar;
    private float direccionManecillas;
    [SerializeField] private float fuerzaGiro = 100f; // Aumentado para mayor sensibilidad
    private float ohm = 1f;
    private float volt = 9f;
    private float amp = 9f;
    private float umbral = 0.01f; // Evita cambios insignificantes
    [SerializeField] GameObject tutoRueda;
    [SerializeField] GameObject tutoClic;

    void Start()
    {
        posicionRotar = 0;
        ActualizarValores();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButton(0))
        {
            direccionManecillas = Input.GetAxis("Mouse ScrollWheel") * 5f; // Aumentamos el efecto del scroll
            tutoRueda.SetActive(true);
            tutoClic.SetActive(false);
            

            if (Mathf.Abs(direccionManecillas) > umbral) // Solo aplicar cambios si el valor es significativo
            {
                Debug.Log("Toco la perilla con giro de: " + direccionManecillas);

                if (ohm <= 0.1f && direccionManecillas < 0) // Evitar valores negativos de ohm
                {
                    return;
                }

                Rotar(direccionManecillas);
                ohm = Mathf.Max(0.1f, ohm + (direccionManecillas > 0 ? 0.1f : -0.1f)); // Asegura incrementos y decrementos
                amp = volt / ohm;

                ActualizarValores();
            }
        }
        else
        {
            tutoRueda.SetActive(false);
            tutoClic.SetActive(true);
        }
    }


    private void ActualizarValores()
    {
        CambiarAmp(amp.ToString("F3") + " A");
        CambiarOhm(ohm.ToString("F3") + " ohm");
        CambiarVolt(volt.ToString("F3") + " V");
        IntensidadLuz(amp);
    }

    private void Rotar(float rota)
    {
        posicionRotar += rota * fuerzaGiro;
        transform.rotation = Quaternion.Euler(0, posicionRotar, 0);
    }

    private void CambiarOhm(string textoNuevo)
    {
        CambiarTexto("ohm", textoNuevo);
    }

    private void CambiarVolt(string textoNuevo)
    {
        CambiarTexto("volt", textoNuevo);
    }

    private void CambiarAmp(string textoNuevo)
    {
        CambiarTexto("amp", textoNuevo);
    }

    private void CambiarTexto(string tag, string textoNuevo)
    {
        GameObject textoVisual = GameObject.FindGameObjectWithTag(tag);
        if (textoVisual != null)
        {
            TextMeshProUGUI texto = textoVisual.GetComponent<TextMeshProUGUI>();
            if (texto != null)
            {
                texto.text = textoNuevo;
            }
            else
            {
                Debug.LogWarning($"El objeto con tag '{tag}' no tiene un componente TextMeshProUGUI.");
            }
        }
        else
        {
            Debug.LogWarning($"No se encontró un objeto con el tag '{tag}'.");
        }
    }

    private void IntensidadLuz(float intensidad)
    {
        GameObject luzObjeto = GameObject.FindGameObjectWithTag("led");
        if (luzObjeto != null)
        {
            Light luz = luzObjeto.GetComponent<Light>();
            if (luz != null)
            {
                luz.intensity = intensidad;
            }
            else
            {
                Debug.LogWarning("El objeto con tag 'led' no tiene un componente Light.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el tag 'led'.");
        }
    }
}
