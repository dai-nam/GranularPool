using System.Collections;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Core
{
    [CustomEditor(typeof(Initializer))]

    public class InitializerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Initializer initializer = (Initializer)target;
            if(GUILayout.Button("(Re-)generate Balls"))
            {
                initializer.InstantiateNewBallsAndGrains();
            }
        }
    }
}