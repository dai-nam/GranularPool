using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InGameObjects
{
    public class TargetLine : MonoBehaviour
    {
        LineRenderer lr;
        float thickness = 0.2f;
        Vector3 start;
        Vector3 end;
        private Vector3 direction;
        public Vector3 Direction
        {
            get { return start - end; }
        }

        private void Awake()
        {
            lr = gameObject.AddComponent<LineRenderer>();
            start = new Vector3();
            end = new Vector3();
            direction = new Vector3();
        }

        public void SetThickness(float value)
        {
            float f = MapDistanceToThickness(value);
            thickness = f;
        }
        private float MapDistanceToThickness(float velocity)
        {
            float temp = Mathf.InverseLerp(0.2f, 50f, velocity);
            float thickness = Mathf.Lerp(0f, 5f, temp);
            return thickness;
        }

        public void SetPoints(Vector3 start, Vector3 end)
        {
            this.start = start;
            this.end = end;
        }

        public void DrawLine()
        {
            lr.enabled = true;
            lr.startWidth = 0.2f;
            lr.endWidth = thickness;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }

        public void DisableTargetLine()
        {
            lr.enabled = false;
        }
    }
}
