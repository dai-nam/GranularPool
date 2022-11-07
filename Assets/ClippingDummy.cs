using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingDummy : MonoBehaviour
{

   [SerializeField] GrainRectangle gr;
   [SerializeField] RectTransform parentRt;
    RectTransform rt;
    Vector3[] worldCornersOfSampler;
    Vector3[] worldCorners;
    bool isClipped = false;
    float width;


    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        worldCorners = new Vector3[4];
        worldCornersOfSampler = new Vector3[4];


    }

    void Update()
    {
        width = gr.gd.grainWidth * UiAudioManager.Instance.GetComponent<RectTransform>().rect.width;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, gr.grainWidthInPixels);
        rt.position = parentRt.position + (Vector3.right * gr.gd.grainPosition * parentRt.rect.width);
        rt.position += new Vector3(0, -parentRt.rect.height/2, 0);

    }

    public Vector3[] GetCornersWithRelativePosition()
    {
        Vector3[] cornersWithRelativePosition = new Vector3[4];
        rt.GetWorldCorners(worldCorners);
        Vector3 v = new Vector3();
        for (int i = 0; i < 4; i++)
        {
            Vector3 tmp = worldCorners[i] - worldCornersOfSampler[i % 2];      // %2 weil relative Position aller Ecken zu den linken Ecken (0 und 1) des Hintergrunds bestimmt werden soll 
            float width = parentRt.rect.width;
            v.Set(tmp.x / width, tmp.y, tmp.z);
            //print("Dummy: "+i + ": " + v.x);
            cornersWithRelativePosition[i] = v;
        }

        return cornersWithRelativePosition;
    }
}


