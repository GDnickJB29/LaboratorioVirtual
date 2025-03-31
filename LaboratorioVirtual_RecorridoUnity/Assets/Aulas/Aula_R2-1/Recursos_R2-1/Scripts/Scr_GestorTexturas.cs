using System.Collections.Generic;
using UnityEngine;

public class Scr_GestorTexturas : MonoBehaviour
{
    public Texture[] texturas; // Arrástralas en el Inspector

    private Dictionary<string, Texture> texturaDict = new Dictionary<string, Texture>();

    void Awake()
    {
        foreach (Texture tex in texturas)
        {
            texturaDict[tex.name] = tex;
            Debug.Log($"Textura cargada: {tex.name}");
        }

        // Imprimir todas las claves del diccionario para asegurarte de que "EmojiOne" está allí
        Debug.Log("Texturas en el diccionario:");
        foreach (var key in texturaDict.Keys)
        {
            Debug.Log($"Claves en diccionario: {key}");
        }
    }



    public Texture ObtenerTextura(string nombre)
    {
        if (texturaDict.TryGetValue(nombre, out Texture textura))
        {
            return textura;
        }
        Debug.LogError($"No se encontró la textura con nombre {nombre}");
        return null;
    }
}
