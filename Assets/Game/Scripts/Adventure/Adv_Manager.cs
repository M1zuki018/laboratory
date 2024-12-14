using UnityEngine;
using UnityEngine.Rendering.Universal;

[DisallowMultipleComponent]
[AddComponentMenu("Original/Adventure/Adv_Manager")]
[RequireComponent(typeof(Adv_Sequence), typeof(SceneLoader))]
public class Adv_Manager : MonoBehaviour
{
    [SerializeField] GameObject _nextButton;
    [SerializeField] GameObject _roomMoveSet;
    [SerializeField] SpriteRenderer _background;
    [SerializeField] Light2D _light;
    bool[] _interactFlg;
    bool _clear;

    Adv_Sequence _sequence;
    SceneLoader _sceneLoader;

    void Start()
    {
        _sequence = GetComponent<Adv_Sequence>();
        _sceneLoader = GetComponent<SceneLoader>();

        Initialize();
        _nextButton.SetActive(false);
    }

    void Initialize()
    {
        //�X�e�[�W�Z�b�g
        for (int i = 0; i < _sequence._advSequence.Count; i++)
        {
            _sequence._advSequence[i]._stage.SetActive(false);
        }
        _sequence._advSequence[SystemManager._sequenceIndex - 1]._stage.SetActive(true);

        //�w�i
        _background.sprite = _sequence._advSequence[SystemManager._sequenceIndex - 1]._background;

        //���C�g
        _light.color = _sequence._advSequence[SystemManager._sequenceIndex - 1]._color;

        //�����ړ��̎d�g�݂̃I���I�t
        if (_sequence._advSequence[SystemManager._sequenceIndex - 1]._roomMove)
            _roomMoveSet.SetActive(true);
        else
            _roomMoveSet.SetActive(false);

        _interactFlg = new bool[_sequence._advSequence[SystemManager._sequenceIndex - 1]._interact];

    }

    public void Interact(int Index)
    {
        _interactFlg[Index] = true;

        for (int i = 0; i < _interactFlg.Length; i++)
        {
            if (_interactFlg[i]) _clear = true;
            else
            {
                _clear = false;
                return;
            }
        }

        if (_clear) _nextButton.SetActive(true);
    }

    public void NextScene()
    {
        _sceneLoader.LoadScene("Story");
    }
}
