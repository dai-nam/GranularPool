using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InGameObjects
{
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] FallenFromTableBehaviour fallenFromTableBehaviour;
      //  [SerializeField] float size = 1;
        public bool onTable;
        protected Rigidbody rb;
        protected SphereCollider sphereCollider;
        public static int numBalls;
        public int instanceId;

        public delegate void HitRespawnPlane(Ball b);
        public event HitRespawnPlane OnHitRespawnPlane;

        private void Awake()
        {
            onTable = true;
            rb = GetComponent<Rigidbody>();
            sphereCollider = GetComponent<SphereCollider>();
            instanceId = numBalls++;
        }


        private void Update()
        {
            ConstrainPositiveYPosition();

            onTable = Table.Instance.CheckIfOnTable(transform.position);
            if (!onTable)
            {
                DecreaseVelocity();
            }
        }

        private void ConstrainPositiveYPosition()
        {
            if (transform.position.y > 2.6)
            {
                transform.position.Set(transform.position.x, 2.5f, transform.position.z);
                print("Constrained");

            }
        }

        private void DecreaseVelocity()
        {
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.95f, 1f, 0.95f));
            //  rb.velocity = Vector3.Scale(rb.velocity, new Vector3(Time.deltaTime, 1f, Time.deltaTime));    //Bug
        }

        public Vector2 GetXandZposition()
        {
            return new Vector2(transform.position.x, transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHitRespawnPlane?.Invoke(this);
        }

        public void SetBallBounce(float value)
        {
            sphereCollider.material.bounciness = value;
            sphereCollider.material.dynamicFriction = 1 - value;
            sphereCollider.material.staticFriction = 1 - value;
        }
    }
}