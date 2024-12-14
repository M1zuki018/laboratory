using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
[AddComponentMenu("Original/Story/ImageDic")]
public class ImageDic : MonoBehaviour
{
    public List<CharaDic> _khalil;
    public List<CharaDic> _filou;
    public List<CharaDic> _agastia;
    public List<CharaDic> _louis;
    public Sprite _transparent;

    [System.Serializable]
    public class CharaDic
    {
        public FacialExpression _facialExpression;
        public Sprite _sprite;
    }

    public enum FacialExpression
    {
        通常,
        笑顔,
        挑発,
        困り,
        痛み,
        キメ
    }
}
