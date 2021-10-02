﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InGameObjects
{
    public class GameBall : Ball
    {
        public int level;

        private void Start()
        {
            Color c = SelectColor();
            SetColor(c);
        }

        private void SetColor(Color c)
        {
            GetComponent<Renderer>().material.SetColor("_Color", c);
        }

        private Color SelectColor()
        {
            Color c = Color.grey;
            if (level == 0)
                c = Color.green;
            else if (level == 1)
                c = Color.blue;
            else if (level == 2)
                c = Color.red;
            else if (level == 3)
                c = Color.black;
            return c;
        }

        public void SetBallBounce(float value)
        {
            sphereCollider.material.bounciness = value;
            sphereCollider.material.dynamicFriction = 1 - value;
            sphereCollider.material.staticFriction = 1 - value;

        }
    }
}
