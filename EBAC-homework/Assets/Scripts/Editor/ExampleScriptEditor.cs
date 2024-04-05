using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExampleScript))]
public class ExampleScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        ExampleScript myTarget = (ExampleScript)target;

        myTarget.carPrefab = (GameObject)EditorGUILayout.ObjectField(myTarget.carPrefab, typeof(GameObject), true);
        
        myTarget.speed = EditorGUILayout.IntField("Velocidade", myTarget.speed);
        myTarget.gear = EditorGUILayout.IntField("Marcha", myTarget.gear);

        EditorGUILayout.LabelField("Velocidade total", myTarget.TotalSpeed.ToString());

        EditorGUILayout.HelpBox("Calcule a velocidade total do carro!", MessageType.Info);

        if(myTarget.TotalSpeed > 200)
        {
            EditorGUILayout.HelpBox("velocidade acima do permitido", MessageType.Error);
        }

        GUI.color = Color.blue;

        if(GUILayout.Button("Create Car"))
        {
            myTarget.CreateCar();
        }

        GUI.color = Color.yellow;

        if(GUILayout.Button("Create Car"))
        {
            myTarget.CreateCar();
        }
    }
}
