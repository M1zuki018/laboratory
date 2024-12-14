using UnityEditor;

[CustomEditor(typeof(ImageChange), true)]
[CanEditMultipleObjects]

public class Inspecter_ImageChanage : Editor
{
    SerializedProperty charaArea1, charaArea2, nameArea;
    void OnEnable()
    {
        charaArea1 = serializedObject.FindProperty("_charaArea1");
        charaArea2 = serializedObject.FindProperty("_charaArea2");
        nameArea = serializedObject.FindProperty("_nameArea");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(charaArea1);
        EditorGUILayout.PropertyField(charaArea2);
        EditorGUILayout.PropertyField(nameArea);
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
