using UnityEditor;

[CustomEditor(typeof(Adv_Sequence))]
[CanEditMultipleObjects]
public class HowUse_Adv_Sequence : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("アドベンチャーパートの編集はここの編集と、ステージ作成です", MessageType.Info);
        base.OnInspectorGUI();
    }
}
