using UnityEditor;

[CustomEditor(typeof(Adv_Sequence))]
[CanEditMultipleObjects]
public class HowUse_Adv_Sequence : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("�A�h�x���`���[�p�[�g�̕ҏW�͂����̕ҏW�ƁA�X�e�[�W�쐬�ł�", MessageType.Info);
        base.OnInspectorGUI();
    }
}
