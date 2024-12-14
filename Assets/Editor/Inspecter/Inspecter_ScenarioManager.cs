using UnityEditor;

[CustomEditor(typeof(ScenarioManager), true)]
[CanEditMultipleObjects]
public class Inspecter_ScenarioManager : Editor
{
    SerializedProperty text, charaName, light, background;
    void OnEnable()
    {
        text = serializedObject.FindProperty("_text");
        charaName = serializedObject.FindProperty("_charaName");
        background = serializedObject.FindProperty("_background");
        light = serializedObject.FindProperty("_light");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(text);
        EditorGUILayout.PropertyField(charaName);
        EditorGUILayout.PropertyField(background);
        EditorGUILayout.PropertyField(light);
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
