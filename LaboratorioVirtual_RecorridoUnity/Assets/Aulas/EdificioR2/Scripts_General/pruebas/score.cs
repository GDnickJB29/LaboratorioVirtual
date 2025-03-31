using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

public class score : MonoBehaviour
{
    [SerializeField] private string apiUrl = "http://tudominio.com/api/update_score.php";
    [SerializeField] private string getScoreUrl = "http://tudominio.com/api/get_score.php";

    public int playerScore;

    void Start()
    {
        StartCoroutine(GetScore());
    }

    public void UpdateScore(int newScore)
    {
        playerScore = newScore;
        StartCoroutine(SendScore(playerScore));
    }

    IEnumerator SendScore(int score)
    {
        string jsonData = JsonConvert.SerializeObject(new { score = score });

        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Score enviado correctamente: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error al enviar score: " + www.error);
            }
        }
    }

    IEnumerator GetScore()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(getScoreUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<ScoreResponse>(www.downloadHandler.text);
                playerScore = response.score;
                Debug.Log("Score obtenido: " + playerScore);
            }
            else
            {
                Debug.LogError("Error al obtener score: " + www.error);
            }
        }
    }

    class ScoreResponse
    {
        public int score;
    }
}
