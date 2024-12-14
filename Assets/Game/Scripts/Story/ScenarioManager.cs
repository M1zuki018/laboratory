using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static ImageDic;

[DisallowMultipleComponent]
[AddComponentMenu("Original/Story/ScenarioManager")]
[RequireComponent(typeof(StorySequence), typeof(CsvReader), typeof(SceneLoader))]
[RequireComponent(typeof(ImageDic), typeof(ImageChange))]
public class ScenarioManager : MonoBehaviour
{
    [SerializeField] Text _text, _charaName;
    [SerializeField] Light2D _light;
    [SerializeField] SpriteRenderer _background;
    bool _isLoaded;
    
    Sprite _charaSprite = default;

    CsvReader _csvReader;
    ImageDic _imageDic;
    ImageChange _imageChange;
    SceneLoader _sceneLoader;
    StorySequence _storySequence;

    Dictionary<string, List<CharaDic>> _charaImage;


    void Start()
    {
        _csvReader = GetComponent<CsvReader>();
        _imageDic = GetComponent<ImageDic>();
        _imageChange = GetComponent<ImageChange>();
        _sceneLoader = GetComponent<SceneLoader>();
        _storySequence = GetComponent<StorySequence>();
        Setup();
        _isLoaded = false;

        //シーケンスの次をセットする
        _light.color = _storySequence._storySequence[SystemManager._sequenceIndex]._color; //ライトの色
        _background.sprite = _storySequence._storySequence[SystemManager._sequenceIndex]._background; //背景素材
        SystemManager._sequenceIndex++;

        Scenario();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isLoaded)
        {
            SystemManager._index++;

            if(SystemManager._index > _storySequence._storySequence[SystemManager._sequenceIndex - 1]._finishIndex)
            {
                if (_storySequence._storySequence[SystemManager._sequenceIndex - 1]._nextScene == NextScene.Adventure)
                {
                    _sceneLoader.LoadScene("Adventure");
                    _isLoaded = true;
                    return;
                }
                else
                {
                    _sceneLoader.LoadScene("Story");
                    _isLoaded = true;
                    return;
                }
            }
            Scenario();
        }
    }

    void Scenario()
    {
        _charaName.text = _csvReader._csvDate[SystemManager._index][0]; //喋っているキャラの名前をセットする
        FicialExpressionCheak(); //表情を取得・立ち絵を変更する
        TextSet(SystemManager._index); //テキストを変更する
    }

    void TextSet(int index)
    {
        if (_csvReader._csvDate[index][2].Contains("\\n")) //改行の処理
            _text.text = _csvReader._csvDate[index][2].Replace("\\n", Environment.NewLine);
        else
            _text.text = _csvReader._csvDate[index][2];
    }

    void FicialExpressionCheak()
    {
        if(_csvReader._csvDate[SystemManager._index][0] == "フェイ")
        {
            _imageChange.PlayerSpeak();
            return;
        }

        switch (_csvReader._csvDate[SystemManager._index][1])
        {
            case "通常":
                CharaCheck(_csvReader._csvDate[SystemManager._index][0], 0);
                break;
            case "表示しない":
                _charaSprite = _imageDic._transparent;
                break;
        }

        DisplayLocation();
    }


    void CharaCheck(string name, int index)
    {
        _charaSprite = _charaImage[name][index]._sprite;
    }

    /// <summary>
    /// 表情差分データの配列を読み込む
    /// </summary>
    void Setup()
    {
        _charaImage = new Dictionary<string, List<CharaDic>>
        {
            { "カリル", _imageDic._khalil },
            { "フィルウ", _imageDic._filou },
            { "アガスティーア", _imageDic._agastia },
            { "ルイス", _imageDic._louis }
        };
    }

    void DisplayLocation()
    {
        switch (_csvReader._csvDate[SystemManager._index][3])
        {
            case "s":
                _imageChange.CharaLeftImageChange(_charaSprite);
                break;
            case "l":
                _imageChange.CharaLeftImageChange(_charaSprite);
                break;
            case "r":
                _imageChange.CharaRightImageChange(_charaSprite);
                break;
            case "a":
                _imageChange.CharaLeftImageChange(_charaSprite);
                _imageChange.CharaRightImageActive();
                break;
        }
    }
}
