using System.Collections.Generic;
using System.IO;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Original/Story/CsvReader")]
public class CsvReader : MonoBehaviour
{
    TextAsset _csvFile;
    [HideInInspector] public List<string[]> _csvDate = new List<string[]>();
    void Awake()
    {
        _csvFile = Resources.Load("laboratory_Scenario") as TextAsset;
        StringReader reader = new StringReader(_csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            _csvDate.Add(line.Split(','));
        }
    }
}
