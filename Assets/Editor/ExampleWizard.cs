using UnityEditor;
using UnityEngine;
public class ExampleWizard : ScriptableWizard
{
    public string gameObjectName;
    [MenuItem("Window/ExampleWizard")]
    static void Open()
    {
        DisplayWizard<ExampleWizard > ("Example Wizard");
    }
    void OnWizardCreate()
    {
        new GameObject(gameObjectName);
    }
}