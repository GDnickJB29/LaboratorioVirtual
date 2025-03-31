using UnityEngine;
using static Scr_DetectarConexion; //para llamar funsiones o objetos de otro script

public class Scr_ComponentesFunsion : MonoBehaviour
{
    public float voltaje, corriente, resistencia;

    public GameObject marcadorA, marcadorB, marcadorC, marcadorD;

    Renderer renderA, renderB, renderC, renderD;

    public bool activaionA, activaionB, activaionC, activacionD;

    public Material materialBlanco, materialNegro, materialRojo;

    private void Start()
    {
        // Asigna solo si el objeto no es null
        if (marcadorA != null)
            renderA = marcadorA.GetComponent<Renderer>();
        else
            Debug.LogWarning("marcadorA no está asignado en el Inspector.");

        if (marcadorB != null)
            renderB = marcadorB.GetComponent<Renderer>();
        else
            Debug.LogWarning("marcadorB no está asignado en el Inspector.");

        if (marcadorC != null)
            renderC = marcadorC.GetComponent<Renderer>();
        else
            Debug.LogWarning("marcadorC no está asignado en el Inspector.");

        if (marcadorD != null)
            renderD = marcadorD.GetComponent<Renderer>();
        else
            Debug.LogWarning("marcadorD no está asignado en el Inspector.");
    }
    public void Colisionando(conexionesDisponibles conexion, Collider objetoTocado, bool poloEnviado)
    {


        try
        {
            switch (conexion)
            {
                case conexionesDisponibles.A:
                    Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " tocó la conexión A");
                    renderA.material = ColorPolo(poloEnviado);

                    break;
                case conexionesDisponibles.B:
                    Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " tocó la conexión B");
                    renderB.material = ColorPolo(poloEnviado);

                    break;
                case conexionesDisponibles.C:
                    Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " tocó la conexión C");
                    renderC.material = ColorPolo(poloEnviado);

                    break;
                case conexionesDisponibles.D:
                    Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " tocó la conexión D");
                    renderD.material = ColorPolo(poloEnviado);

                    break;
                default:
                    RecetearTodo();

                    break;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
    public void Descolisionando(conexionesDisponibles conexion, Collider objetoTocado, bool poloEnviado)
    {
        switch (conexion)
        {
            case conexionesDisponibles.A:
                Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " Dejo de tocar la conexión A");
                RecetearTodo();

                break;
            case conexionesDisponibles.B:
                Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " Dejo de tocar la conexión B");
                RecetearTodo();

                break;
            case conexionesDisponibles.C:
                Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " Dejo de tocar la conexión C");
                RecetearTodo();

                break;
            case conexionesDisponibles.D:
                Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " Dejo de tocar la conexión D");
                RecetearTodo();

                break;
            default:
                RecetearTodo();

                break;
        }
    }

    public Material ColorPolo(bool poloPositivo)
    {
        if (poloPositivo)
        {
            return materialRojo;
        }
        else
        {
            return materialNegro;
        }
    }

    public void RecetearTodo()
    {
        if (renderA != null)
        {
            renderA.material = materialBlanco;
        }
        else
        {
            Debug.LogWarning("renderA no está asignado.");
        }

        if (renderB != null)
        {
            renderB.material = materialBlanco;
        }
        else
        {
            Debug.LogWarning("renderB no está asignado.");
        }

        if (renderC != null)
        {
            renderC.material = materialBlanco;
        }
        else
        {
            Debug.LogWarning("renderC no está asignado.");
        }

        if (renderD != null)
        {
            renderD.material = materialBlanco;
        }
        else
        {
            Debug.LogWarning("renderD no está asignado.");
        }
    }


}
