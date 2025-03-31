using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarNivel : MonoBehaviour
{
    [SerializeField] private string escenaSiguienteNivel;
    [SerializeField] private int iD;
    [SerializeField] private TextMeshProUGUI input;
    [SerializeField] private Light luz;
    [SerializeField] private GameObject gia;
    private float contador;

    void Update()
    {
        if (nivelEscogido(iD, input))
        {
            CambiarEscena(escenaSiguienteNivel);
        }
    }

    private bool nivelEscogido(int nivel, TextMeshProUGUI texto)
    {
        switch (nivel)
        {
            case 0:
                return true;
            case 1:
                string textoIngresado = texto.text;
                Debug.Log("Texto ingresado: " + textoIngresado);

                if (!string.IsNullOrEmpty(textoIngresado) && textoIngresado.Length > 1)
                {
                    string numeroStr = textoIngresado.Substring(0, textoIngresado.Length - 2); // Elimina " A"

                    if (float.TryParse(numeroStr, out float numero))
                    {
                        if (Mathf.Approximately(numero, 5f)) // Comparación con tolerancia
                        {
                            if (luz.color != Color.green)
                            {
                                gia.SetActive(true);
                                luz.color = Color.green; // Cambio inmediato
                            }

                            contador += Time.deltaTime;

                            if (contador > 2)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            luz.color = Color.red; // Solo cambia si el número no es correcto
                            gia.SetActive(false);
                            contador = 0;
                        }
                    }
                    else
                    {
                        luz.color = Color.red;
                        contador = 0;
                    }
                }
                else
                {
                    luz.color = Color.red;
                    contador = 0;
                }

                return false;


            default:
                return false;
        }
    }

    private void CambiarEscena(string escena)
    {
        if (!string.IsNullOrEmpty(escena))
        {
            SceneManager.LoadScene(escena);
            Debug.Log("Cambiando a la escena: " + escena);
        }
        else
        {
            Debug.Log("No se ha asignado un nombre de escena válido.");
        }
    }
}
