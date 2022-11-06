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
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Park"))
        {
            parkBuilder.BuildPark();
        }
    }

    private void OnEnable()
    {
        parkBuilder = (ParkBuilder)target;
    }
}
