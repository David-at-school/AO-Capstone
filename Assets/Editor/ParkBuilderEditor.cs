using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ParkBuilder))]
public class ParkBuilderEditor : Editor
{
    ParkBuilder parkBuilder;

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Reset Parts"))
        {
            parkBuilder.ResetParts();
        }

        if (GUILayout.Button("Build Part"))
        {
            parkBuilder.BuildPart();
        }

        if (GUILayout.Button("Generate Park"))
        {
            parkBuilder.BuildPark();
        }

        base.OnInspectorGUI();
    }

    private void OnEnable()
    {
        parkBuilder = (ParkBuilder)target;
    }
}
