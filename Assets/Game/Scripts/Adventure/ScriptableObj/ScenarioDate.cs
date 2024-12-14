using UnityEditor;
using UnityEngine;

public class ScenarioDate : ScriptableObject
{
    public string _scenarioIndex;
    public string[] _scenarioText;
    public string[] _charaName;

    [MenuItem("Adventure/Create NewScenario")]
    static void CreateAdventureAssetInstance()
    {
        var adventureAsset = CreateInstance<ScenarioDate>();

        AssetDatabase.CreateAsset(adventureAsset, "Assets/Game/AdvScenario/ScenarioDate.asset");
        AssetDatabase.Refresh();
    }
}
