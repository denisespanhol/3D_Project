using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerController fsm = (PlayerController)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.playerStateMachine == null) return;

        if (fsm.playerStateMachine.CurrentState != null)
            EditorGUILayout.LabelField("Current State: ", fsm.playerStateMachine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available State");

        if (showFoldout)
        {
            if (fsm.playerStateMachine.dictionaryState != null)
            {
                var keys = fsm.playerStateMachine.dictionaryState.Keys.ToArray();
                var values = fsm.playerStateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], values[i]));
                }
            }
        }
    }
}
