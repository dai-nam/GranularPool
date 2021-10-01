using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTest : MonoBehaviour
{

    public Texture2D texture;
    public Sprite mySprite;
    private int[,] arr = new int[8, 8];

    void Start()
    {
        texture = new Texture2D(8, 8);

        // Array zufällig füllen
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                arr[x, y] = Random.Range(0, 2);
            }
        }

        // Textur erzeugen
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = (arr[x, y] == 0 ? Color.clear : Color.black);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();

        // Thanks to "Mr 3d" ;)
        mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}