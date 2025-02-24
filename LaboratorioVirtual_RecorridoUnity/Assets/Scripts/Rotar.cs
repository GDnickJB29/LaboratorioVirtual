using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class Rotar : MonoBehaviour
{
    Vector3 movimiento;
    int contador = -1; // Inicializa en -1 para detectar si ya se obtuvo el score real
    Rigidbody rb;
    public TMP_Text texto;
    private string urlGetScore = "http://localhost/LaboratorioVirtual/LaboratorioVirtual_pagina/api/get_score.php";
    private string urlUpdateScore = "http://localhost/LaboratorioVirtual/LaboratorioVirtual_pagina/api/update_score.php";


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ObtenerScore());
        movimiento = new Vector3(0, 0, 10);
    }

    void Update()
    {
            texto.text = contador.ToString();


        //if (contador == -1) return; // No hacer nada hasta que se obtenga el score real
        try
        {
            StartCoroutine(ObtenerScore());


            if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click del mouse");
            contador++;

            rb.AddTorque(movimiento);

            // Actualizar el score en la base de datos solo si cambia
            StartCoroutine(ActualizarScore(contador));
        }

          
                
            


        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Click derecho");
 

            rb.AddTorque(movimiento);
            
            for (int i = 0; i < contador; i++)
            {
                rb.AddTorque(movimiento);
                texto.text = "BOOM!";
            }
            
        }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            throw;
        }

    }

    IEnumerator ObtenerScore()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(urlGetScore))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string json = www.downloadHandler.text;
                ScoreData data = JsonUtility.FromJson<ScoreData>(json);
                contador = data.score; // Ahora sí se usa el score real
                texto.text = contador.ToString();

            }
            else
            {
                Debug.LogError("Error al obtener el score: " + www.error);
                contador = 0; // Si falla la carga, evita que se quede en -1
            }
        }
    }

    IEnumerator ActualizarScore(int nuevoScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", nuevoScore);

        using (UnityWebRequest www = UnityWebRequest.Post(urlUpdateScore, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al actualizar el score: " + www.error);
            }
        }
    }
}

[System.Serializable]
public class ScoreData
{
    public int score;
}
