using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Original/Adventure/Adv_Manager")]
public class Adv_Sequence : MonoBehaviour
{
    [System.Serializable]
    public class AdvSequence
    {
        public string _name;
        [Tooltip("背景の番号")] public Sprite _background;
        [Tooltip("ライトの色")] public Color _color;
        [Tooltip("読み込むプレハブ")] public GameObject _stage;
        [Tooltip("インタラクトの数")] public int _interact;
        [Tooltip("部屋移動があるか")] public bool _roomMove;
    }

    public List<AdvSequence> _advSequence;

}
