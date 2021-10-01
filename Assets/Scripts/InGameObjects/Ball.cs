using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InGameObjects
{
    public abstract class Ball : MonoBehaviour
    {
        public delegate void FallenFromTable(Ball b);
        public event FallenFromTable OnFallenFromTable;

        [SerializeField] public bool isRespawnable;
        [SerializeField] float size = 1;
        public bool onTable;
        protected Rigidbody rb;
        protected SphereCollider sphereCollider;
        private static int id;
        public int instanceId;


        private void Awake()
        {
            onTable = true;
            rb = GetComponent<Rigidbody>();
            sphereCollider = GetComponent<SphereCollider>();
            instanceId = id++;
        }


        private void Update()
        {
            onTable = Table.Instance.CheckIfOnTable(transform.position);
            if (!onTable)
            {
                DecreaseVelocity();
            }
        }



        private void DecreaseVelocity()
        {
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.95f, 1f, 0.95f));
            //  rb.velocity = Vector3.Scale(rb.velocity, new Vector3(Time.deltaTime, 1f, Time.deltaTime));    //Bug
        }

        public void Respawn()
        {
            Vector3 randomPosition = Table.Instance.GetRandomPositionOnTable();
            RespawnAtPosition(randomPosition);
            rb.velocity = new Vector3(0, 0, 0);
        }



        private void RespawnAtPosition(Vector3 randomPosition)
        {
            transform.position = randomPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleFallFromTable(this);
        }
        public void HandleFallFromTable(Ball b)
        {
            if (isRespawnable)
            {
                Respawn();
            }
            else
            {
                OnFallenFromTable?.Invoke(b);
            }
        }

        public Vector2 GetXandZposition()
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }
}