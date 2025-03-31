using UnityEngine;

public class Scr_Configuraciones : MonoBehaviour
{

    private bool estaEncima = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void setEstaEncima(bool valor)
    {
        estaEncima=valor;
    }

    public bool getEstaEncima()
    {
        return estaEncima;
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
