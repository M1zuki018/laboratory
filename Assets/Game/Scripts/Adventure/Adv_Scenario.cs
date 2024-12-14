using UnityEngine;
using UnityEngine.UI;

public class Adv_Scenario : MonoBehaviour
{
    [SerializeField] ScenarioDate _scenarioDate;
    [SerializeField] Text _name, _text;
    int _index;
    bool _playing;

    public void Scenario()
    {
        _playing = true;
        _text.text = _scenarioDate._scenarioText[_index];
        _name.text = _scenarioDate._charaName[_index];
    }

    void Update()
    {
        if(_playing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _index++;

                if(_index >= _scenarioDate._scenarioText.Length)
                {
                    _text.transform.parent.gameObject.SetActive(false);
                    _index = 0;
                    _playing = false;
                    return;
                }
                else 
                {
                    Scenario();
                }
            }
        }
    }
    
}
