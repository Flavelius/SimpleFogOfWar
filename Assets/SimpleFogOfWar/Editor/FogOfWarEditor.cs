using System;
using SimpleFogOfWar;
using SimpleFogOfWar.Renderers;
using UnityEditor;
using UnityEngine;
// ReSharper disable CheckNamespace

[CustomEditor(typeof(FogOfWarSystem))]
public class FogOfWarEditor : Editor
{
    readonly string[] renderers = {"Select", "DirectSeeThrough", "Projector"};
    readonly Type[] rendererTypes = {null, typeof (SeeThroughFogRenderer), typeof (ProjectorFogRenderer)};

    public override void OnInspectorGUI()
    {
        var fow = target as FogOfWarSystem;
        if (!fow) return;
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        GUI.enabled = !Application.isPlaying;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("size"), new GUIContent("Size", "Dimensions of the covered area"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Resolution"), new GUIContent("Resolution", "Resolution of the fog texture"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mode"), new GUIContent("Mode", "Persistent = additive uncovering of fog,\nonly really usable if fog renderer is kept static"));
        EditorGUILayout.Slider(serializedObject.FindProperty("edgeSoftness"), 0f, 1f, new GUIContent("Edge softness", "Pixelated <-> smoothed edges of the fog edge"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("color"), new GUIContent("Color", "Fog-color. Also controls transparency:\nblack <-> white = opaque <-> transparent"));

        GUI.enabled = true;
        var frp = serializedObject.FindProperty("fogRenderer");
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label(new GUIContent("RendererType", "How the fog is rendered"), GUILayout.Width(EditorGUIUtility.labelWidth-4));
            var existingObject = frp.objectReferenceValue;
            var activeSelection = existingObject == null ? 0 : Array.IndexOf(rendererTypes, existingObject.GetType());
            if (activeSelection < 0) activeSelection = 0;
            var newSelection = EditorGUILayout.Popup(activeSelection, renderers);
            if (newSelection != activeSelection)
            {
                switch (newSelection)
                {
                    case 1:
                        fow.SetFogRenderer(CreateInstance<SeeThroughFogRenderer>());
                        break;
                    case 2:
                        fow.SetFogRenderer(CreateInstance<ProjectorFogRenderer>());
                        break;
                }
            }
        }
        GUI.enabled = !Application.isPlaying;
        if (frp.objectReferenceValue != null)
        {
            var so = new SerializedObject(frp.objectReferenceValue);
            var it = so.GetIterator();
            it.NextVisible(true);
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUI.BeginChangeCheck();
                while (it.NextVisible(true))
                {
                    EditorGUILayout.PropertyField(it);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    so.ApplyModifiedProperties();
                }
            }
        }
        if (EditorGUI.EndChangeCheck()) serializedObject.ApplyModifiedProperties();
    }	
}
