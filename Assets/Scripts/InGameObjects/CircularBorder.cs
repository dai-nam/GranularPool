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
        [SerializeField] int radius = 65;
        [SerializeField] [Range(4, 100)] int resolution = 4;

        Vector3 center = new Vector3(0, 0, 0);
        public List<Vector3> vertices;



        void Start()
        {
            Mesh = new Mesh();
            Mesh.name = "NewMesh";
            Mesh.vertices = CreateVertices(resolution);
            Mesh.triangles = CreateTriangles();

            Mesh.RecalculateNormals();
            Mesh.RecalculateBounds();
            MeshFilter = gameObject.AddComponent<MeshFilter>();
            MeshFilter.mesh = Mesh;
            GetComponent<MeshCollider>().sharedMesh = Mesh;

        }

        private Vector3[] CreateVertices(int res)
        {
            vertices = new List<Vector3>();
            float stepSize = 2f / res;
            for (int i = 1; i <= res; i++)
            {
                float angle = (float)Math.PI * i * stepSize;
                Vector3 vertex1 = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized * radius;
                Vector3 vertex2 = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized * radius;
                vertex2.y += 10;             // height of border


                vertex1 += center;
                vertex2 += center;

                vertices.Add(vertex1);
                vertices.Add(vertex2);

            }
            return vertices.ToArray();
        }

        private int[] CreateTriangles()
        {
            List<int> edges = new List<int>();          //todo refactor
            int i = 0;
            for (; i < vertices.Count - 2; i += 2)
            {

                edges.Add(i);
                edges.Add(i + 1);
                edges.Add(i + 2);
                edges.Add(i + 1);
                edges.Add(i + 2);
                edges.Add(i + 3);
            }
            //Kreis schließen
            edges.Add(i);
            edges.Add(i + 1);
            edges.Add(0);
            edges.Add(i + 1);
            edges.Add(0);
            edges.Add(1);

            print(i);
            return edges.ToArray();


        }

        private void OnTriggerEnter(Collider other)
        {
            print("Collision");
        }


    }
}