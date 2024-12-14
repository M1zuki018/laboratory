using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour
{
    [SerializeField] Text _text, _nameArea;
    [SerializeField] string _name;
    [SerializeField] string[] _textPattern;

    public void TextChange()
    {
        _nameArea.text = _name;
        int ranNum = Random.Range(0, _textPattern.Length);
        _text.text = _textPattern[ranNum];

        Invoke("TextBoxClose", 4);
    }

    void TextBoxClose()
    {
        _text.transform.parent.gameObject.SetActive(false);
    }
}
