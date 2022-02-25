using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class WaveformTexture : MonoBehaviour
{
    int width = 128;
    int height = 128;
    public Texture2D texture;

    private void Start()
    {
        texture = new Texture2D(width, height);
        for (int y = 0; y < 128; y++)
        {
            for (int x = 0; x < 128; x++)
            {
                texture.SetPixel(x, y, Random.ColorHSV());
            }
        }
        
        texture.Apply();
        ImportTextureAsset(texture);
    }

    private void ImportTextureAsset(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        string path = Path.Combine(Application.dataPath, "Textures\\texture1.png");
        print(path);
        File.WriteAllBytes(path, bytes);
        AssetDatabase.ImportAsset("Assets\\Textures\\texture1.png");
    
}
}
