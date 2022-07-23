using System;
using UnityEditor;
using UnityEngine;

namespace AirplaneInput
{
    [CustomEditor(typeof(IP_Base_Airplane_Input))]
    public class IP_BaseAirplaneInput_Editor : Editor
    {
    #region Variables

        private IP_Base_Airplane_Input targetInput;

    #endregion

    #region Builtin Methods

        private void OnEnable()
        {
            targetInput = (IP_Base_Airplane_Input)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            //Assembling data to write
            string debugInfo = "";
            debugInfo += $"Pitch: {targetInput.Pitch}\n";
            debugInfo += $"Roll: {targetInput.Roll}\n";
            debugInfo += $"Yaw: {targetInput.Yaw}\n";
            debugInfo += $"Throttle: {targetInput.Throttle}\n";
            debugInfo += $"Brake: {targetInput.Brake}\n";
            debugInfo += $"Flaps: {targetInput.Flaps}\n";
            
            //Custom editor code
            EditorGUILayout.Space(10);
            EditorGUILayout.TextArea(debugInfo,GUILayout.Height(200));
            EditorGUILayout.Space(10);
            
            Repaint();
        }

    #endregion
    }
}