#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BerTaDEV
{
    [CustomEditor(typeof(TilePrefab))]
    public class TilePrefabEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); 
            TilePrefab myScript = (TilePrefab)target;
            if (GUILayout.Button("Generate Tile"))
            {
                myScript.GenerateTileFromEditor();
            }
        }
    }
}
#endif