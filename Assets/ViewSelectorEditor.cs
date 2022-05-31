using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ViewSelector))]
public class ViewSelectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ViewSelector viewSelector = (ViewSelector)target;
        if (GUILayout.Button("Change View"))
        {
            viewSelector.InvokeViewChange();
        }
    }
}
