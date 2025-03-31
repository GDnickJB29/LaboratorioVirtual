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
            Debug.LogWarning("marcadorA no est� asignado en el Inspector.");

        if (marcadorB != null)
            renderB = marcadorB.GetComponent<Renderer>();
        else
            Debug.LogWarning("marcadorB no est� asignado en el Inspector.");

        if (marcadorC != null)
            renderC = marcadorC.GetComponent<Renderer>();
        else
            Debug.LogWarning("marcadorC no est� asignado en el Inspector.");

        if (marcadorD != null)
            renderD = marcadorD.GetComponent<Renderer>();
        else
            Debug.LogWarning("marcadorD no est� asignado en el Inspector.");
    }
    public void Colisionando(conexionesDisponibles conexion, Collider objetoTocado, bool poloEnviado)
    {


        try
        {
            switch (conexion)
            {
                case conexionesDisponibles.A:
                    Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " toc� la conexi�n A");
                    renderA.material = ColorPolo(poloEnviado);

                    break;
                case conexionesDisponibles.B:
                    Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " toc� la conexi�n B");
                    renderB.material = ColorPolo(poloEnviado);

                    break;
                case conexionesDisponibles.C:
                    Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " toc� la conexi�n C");
                    renderC.material = ColorPolo(poloEnviado);

                    break;
                case conexionesDisponibles.D:
                    Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " toc� la conexi�n D");
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
                Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " Dejo de tocar la conexi�n A");
                RecetearTodo();

                break;
            case conexionesDisponibles.B:
                Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " Dejo de tocar la conexi�n B");
                RecetearTodo();

                break;
            case conexionesDisponibles.C:
                Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " Dejo de tocar la conexi�n C");
                RecetearTodo();

                break;
            case conexionesDisponibles.D:
                Debug.Log("El objeto " + objetoTocado + " (polo: " + poloEnviado + ")" + " Dejo de tocar la conexi�n D");
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
            Debug.LogWarning("renderA no est� asignado.");
        }

        if (renderB != null)
        {
            renderB.material = materialBlanco;
        }
        else
        {
            Debug.LogWarning("renderB no est� asignado.");
        }

        if (renderC != null)
        {
            renderC.material = materialBlanco;
        }
        else
        {
            Debug.LogWarning("renderC no est� asignado.");
        }

        if (renderD != null)
        {
            renderD.material = materialBlanco;
        }
        else
        {
            Debug.LogWarning("renderD no est� asignado.");
        }
    }


}
