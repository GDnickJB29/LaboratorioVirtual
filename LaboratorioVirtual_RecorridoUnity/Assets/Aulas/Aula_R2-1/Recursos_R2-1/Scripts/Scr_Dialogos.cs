using System.Collections.Generic;
using UnityEngine;

public class Scr_Dialogos : MonoBehaviour
{
    public TextAsset archivoCSV; // Arrástralo en el Inspector
    private Dictionary<int, (string, string)> data = new Dictionary<int, (string, string)>();

    void Awake()
    {
        if (archivoCSV != null)
        {
            CargarCSV(archivoCSV.text);
        }
        else
        {
            Debug.LogError("No se asignó un archivo CSV en el Inspector.");
        }
    }

    void CargarCSV(string csvText)
    {
        string[] lines = csvText.Split('\n');

        for (int i = 1; i < lines.Length; i++) // Saltamos la primera línea (encabezado)
        {
            string[] columns = lines[i].Split(',');
            Debug.Log(columns.Length);
            if (columns.Length < 3)
            {
                Debug.LogError($"Fila {i} tiene menos de 3 columnas.");
                continue;
            }

            if (!int.TryParse(columns[0], out int id))
            {
                Debug.LogError($"Error en la ID en la fila {i}");
                continue;
            }

            string texto = columns[1].Trim('"');
            string figura = columns[2].Trim('"');

            data[id] = (texto, figura);
        }
    }

    public (string, string) ObtenerTexto(int id)
    {
        if (data.TryGetValue(id, out var contenido))
        {
            return contenido;
        }

        Debug.LogError($"El ID {id} no existe en el CSV.");
        return ("ID no encontrado", "default");
    }
}
