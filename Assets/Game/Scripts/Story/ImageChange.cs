using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[AddComponentMenu("Original/Story/ImageChange")]
public class ImageChange : MonoBehaviour
{
    [SerializeField] Image _charaArea1, _charaArea2;
    [SerializeField] Text _nameArea;
    ImageDic _imageDic;

    void Awake()
    {
        _imageDic = GetComponent<ImageDic>();
    }

    /// <summary>
    /// 左側もしくは中央のキャラクターの画像を変更
    /// </summary>
    /// <param name="index">キャラクターの画像のID</param>
    public void CharaLeftImageChange(Sprite sprite)
    {
        _charaArea1.sprite = sprite;

        _charaArea1.color = Color.white;
        _charaArea2.color = Color.gray;
        _charaArea1.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.3f);
        _charaArea2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
    }

    public void CharaRightImageChange(Sprite sprite) //右側
    {
        if (!_charaArea2.gameObject.activeSelf) _charaArea2.gameObject.SetActive(true);
        _charaArea2.sprite = sprite;

        _charaArea1.color = Color.gray;
        _charaArea2.color = Color.white;
        _charaArea1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
        _charaArea2.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.3f);
    }

    public void CharaRightImageActive()
    {
        if (_charaArea2.gameObject.activeSelf)
        {
            _charaArea2.gameObject.SetActive(false);
        }
        else
        {
            _charaArea2.gameObject.SetActive(true);
        }
    }

    public void PlayerSpeak()
    {
        _charaArea1.color = Color.gray;
        _charaArea2.color = Color.gray;
        _charaArea1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
        _charaArea2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
    }
}
