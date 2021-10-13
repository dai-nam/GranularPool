using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts.InGameObjects
{
    public class CircularBorder : MonoBehaviour
    {
        protected MeshFilter MeshFilter;

        protected Mesh Mesh;
        [SerializeField] int radius = 100;
        [SerializeField] [Range(4, 100)] int resolution = 4;

        Vector3 center = new Vector3(0, 0, 0);
        public List<Vector3> vertices;



        void Start()
        {
            Mesh = new Mesh();
            Mesh.name = "NewMesh";
            Mesh.vertices = CreateVertices(resolution);
            Mesh.triangles = CreateTriangles();

            //   Mesh.RecalculateNormals();
            //   Mesh.RecalculateBounds();
            MeshFilter = gameObject.AddComponent<MeshFilter>();
            MeshFilter.mesh = Mesh;

        }

        private int[] CreateTriangles()
        {
            return new int[]
            {
                    0,1,2,
                    0,2,3
            };
        }

        private Vector3[] CreateVertices(int res)
        {
            vertices = new List<Vector3>();
            float stepSize = 2f / res;

            for (int i = 1; i <= res; i++)
            {
                float angle = (float) Math.PI * i * stepSize;
                Vector3 vertex = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized * radius;

                vertex += center;
                vertices.Add(vertex);

            }

            return vertices.ToArray();
        }


    }
}