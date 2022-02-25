using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InGameObjects
{
    public class GameBall : Ball
    {
        public BallLevel level;

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
            Color c;
            switch (level)
            {
                case BallLevel.LEVEL_ZERO:
                    c = Color.green;
                    break;
                case BallLevel.LEVEL_ONE:
                    c = Color.black;
                    break;
                case BallLevel.LEVEL_TWO:
                    c = Color.blue;
                    break;
                case BallLevel.LEVEL_THREE:
                    c = Color.red;
                    break;
                default:
                    c = Color.grey;
                    break;
            }
            return c;
        }

      
    }
}
