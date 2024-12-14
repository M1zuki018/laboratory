using UnityEditor;

[CustomEditor(typeof(Adv_Manager), true)]
[CanEditMultipleObjects]
public class Inspecter_Adv_Manager : Editor
{
    SerializedProperty button, obj, background, light;

    void OnEnable()
    {
        button = serializedObject.FindProperty("_nextButton");
        obj = serializedObject.FindProperty("_roomMoveSet");
        background = serializedObject.FindProperty("_background");
        light = serializedObject.FindProperty("_light");
    }

    public override void OnInspectorGUI()
    {
        // �f�[�^�̍X�V�J�n
        serializedObject.Update();

        // anotherValue �͕ҏW�s��
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(button);
        EditorGUILayout.PropertyField(obj);
        EditorGUILayout.PropertyField(background);
        EditorGUILayout.PropertyField(light);
        EditorGUI.EndDisabledGroup();

        // �f�[�^�̍X�V���f
        serializedObject.ApplyModifiedProperties();
    }
}
