using UnityEditor;
public class Example : EditorWindow
{
    static Example exampleWindow;
    [MenuItem("CustomMenu/Example")]
    static void Open()
    {
        if (exampleWindow == null)
        {
            exampleWindow = CreateInstance<Example>();
        }
        exampleWindow.ShowUtility();
    }

    [MenuItem("CustomMenu/Example/Story/Grandchild")]
    static void Example1()
    {
    }

}