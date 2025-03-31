using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_MostrarTexto : MonoBehaviour
{
    [System.Serializable]
    private struct Dialogo
    {
        [TextArea(3, 10)] public string texto;
        public ObjetosDisponibles idObjRemarcado; // Ahora es un enum y aparecer� como lista en el Inspector
        public Texture textura; // Textura asociada al di�logo
    }


    [SerializeField] private Dialogo[] dialogos; // Array de di�logos con ID y texturas
    [SerializeField] private GameObject panel;
    [SerializeField] private RawImage guiaRawImage; // RawImage para mostrar la textura
    [SerializeField] private GameObject LuzBateria, LuzTodo, LuzLed, LuzPotenciometro;
    [SerializeField] private Mesh botonContinuar, botonReiniTuto;
    [SerializeField] private MeshFilter boton;
    [SerializeField] private TextMeshProUGUI FormulaAmp, RespuestaAmp, FormulaOhm, RespuestaOhm, FormulaVolt, RespuestaVolt;


    private bool primeraVez = false;  //hace que todo se inicie al principio
    private bool textoMostrado = false; // Evita m�ltiples activaciones
    private bool dentroDelTrigger = false; // Controla si el jugador est� dentro del �rea del trigger
    private TextMeshProUGUI mensajeTexto;
    private int indiceActual = 0; // �ndice para llevar el seguimiento del di�logo y las texturas
    private int idObjetoAMostrar = -1; // ID del objeto que se mostrar�
    [SerializeField] private bool esSalida = true;
    [SerializeField] private string nombreEscena;




    private void Start()
    {
        if (!esSalida)
        {
        primeraVez = true;
            Debug.Log(this.gameObject);
            
        }
        panel.SetActive(true);
        textoMostrado = true;
        InicializarComponentes();
        boton.mesh = botonContinuar;
    }


    public enum ObjetosDisponibles
    {
        Ninguno = 0,
        Bateria = 1,
        Led = 2,
        Potenciometro = 3,
        Todo = 4,
        Amperaje = 5,
        Oms = 6,
        Voltaje = 7
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouse"))
        {
            dentroDelTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mouse"))
        {
            dentroDelTrigger = false;
        }
    }

    void Update()
    {
        if (dentroDelTrigger && !textoMostrado && Input.GetMouseButtonDown(0))
        {
            if (dialogos.Length > 0)
            {
                
                panel.SetActive(true);
                textoMostrado = true;
                InicializarComponentes();
                boton.mesh = botonContinuar;
            }
            else
            {
                Debug.LogError("El array 'dialogos' est� vac�o.");
            }
        }

        if ((dentroDelTrigger && textoMostrado && Input.GetMouseButtonDown(0)) || primeraVez)
        {
            if (indiceActual < dialogos.Length)
            {
                panel.SetActive(true);
                MostrarTexto(indiceActual);
                idObjetoAMostrar = (int)dialogos[indiceActual].idObjRemarcado;

                DesactivarTodo();
                ActivarUnObjeto(dialogos[indiceActual].idObjRemarcado);


                indiceActual++;
            }
            else
            {
                panel.SetActive(false);
                textoMostrado = false;
                DesactivarTodo();
                boton.mesh = botonReiniTuto;
                indiceActual = 0;

                if (esSalida)
                {
                    CambiarEscena();
                }

            }

                primeraVez = false;
        }
    }

    void DesactivarTodo()
    {
        LuzBateria.SetActive(false);
        LuzLed.SetActive(false);
        LuzPotenciometro.SetActive(false);
        LuzTodo.SetActive(false);
        FormulaAmp.color = Color.white;
        FormulaOhm.color = Color.white;
        FormulaVolt.color = Color.white;
        RespuestaAmp.color = Color.white;
        RespuestaVolt.color = Color.white;
        RespuestaOhm.color = Color.white;

        
    }

    private void CambiarEscena()
    {
        // Cambiar a la escena especificada por el nombre
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);  // Carga la escena usando el nombre
            Debug.Log("Cambiando a la escena: " + nombreEscena);
        }
        else
        {
            Debug.Log("No se ha asignado un nombre de escena.");
        }
    }

    void ActivarUnObjeto(ObjetosDisponibles objeto)
    {
        switch (objeto)
        {
            case ObjetosDisponibles.Bateria:
                LuzBateria.SetActive(true);
                break;
            case ObjetosDisponibles.Led:
                LuzLed.SetActive(true);
                break;
            case ObjetosDisponibles.Potenciometro:
                LuzPotenciometro.SetActive(true);
                break;
            case ObjetosDisponibles.Todo:
                LuzTodo.SetActive(true);
                break;
            case ObjetosDisponibles.Amperaje:
                FormulaAmp.color = Color.red;
                RespuestaAmp.color = Color.red;
                break;
            case ObjetosDisponibles.Oms:
                FormulaOhm.color = Color.red;
                RespuestaOhm.color = Color.red;
                break;
            case ObjetosDisponibles.Voltaje:
                FormulaVolt.color = Color.red;
                RespuestaVolt.color = Color.red;
                break;
            default:
                Debug.Log("No Se resalta nada en este momento.");
                break;
        }
    }


    void InicializarComponentes()
    {
        GameObject mensajeObj = GameObject.FindGameObjectWithTag("Mensaje");
        if (mensajeObj != null)
        {
            mensajeTexto = mensajeObj.GetComponent<TextMeshProUGUI>();
            if (mensajeTexto == null)
                Debug.LogError("El objeto con tag 'Mensaje' no tiene un componente Text.");
        }
        else
        {
            Debug.LogError("No se encontr� el objeto con tag 'Mensaje'.");
        }

        if (guiaRawImage == null)
        {
            Debug.LogError("No se asign� el RawImage para la textura.");
        }
    }

    public void MostrarTexto(int id)
    {
        if (id >= 0 && id < dialogos.Length)
        {
            string texto = dialogos[id].texto;

            if (mensajeTexto == null)
            {
                Debug.LogError("mensajeTexto no est� inicializado.");
                return;
            }

            if (!string.IsNullOrEmpty(texto))
            {
                mensajeTexto.text = texto;
            }
            else
            {
                Debug.LogError($"No se encontr� texto para el ID {id}.");
            }

            if (dialogos[id].textura != null)
            {
                CambiarTextura(dialogos[id].textura);
            }
            else
            {
                Debug.LogError($"No se encontr� textura para el ID {id}.");
            }
        }
        else
        {
            Debug.LogError("El ID proporcionado est� fuera de los l�mites de los arrays.");
        }
    }

    void CambiarTextura(Texture nuevaTextura)
    {
        if (guiaRawImage == null)
        {
            Debug.LogError("guiaRawImage no est� inicializado.");
            return;
        }

        if (nuevaTextura != null)
        {
            guiaRawImage.texture = nuevaTextura;
        }
        else
        {
            Debug.LogError("No se encontr� la textura.");
        }
    }
}
