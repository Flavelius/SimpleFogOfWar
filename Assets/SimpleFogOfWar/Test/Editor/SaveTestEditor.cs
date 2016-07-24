using UnityEditor;
using UnityEngine;

namespace SimpleFogOfWar.Test
{
    [CustomEditor(typeof(SaveTest))]
    public class SaveTestEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            var st = target as SaveTest;
            if (!st) return;
            if (st.SaveData == null || st.SaveData.Length == 0)
            {
                if (GUILayout.Button("Save current State"))
                {
                    var fow = FindObjectOfType<FogOfWarSystem>();
                    if (fow)
                    {
                        st.SaveData = fow.GetPersistentData();
                    }
                }
            }
            else
            {
                if (GUILayout.Button("Restore saved State"))
                {
                    var fow = FindObjectOfType<FogOfWarSystem>();
                    if (fow)
                    {
                        fow.LoadPersistentData(st.SaveData);
                    }
                }
            }
        }
    }
}
