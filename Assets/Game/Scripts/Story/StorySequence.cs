using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Original/Story/StorySequence")]
public class StorySequence : MonoBehaviour
{
    [System.Serializable]
    public class Sequence
    {
        public string _name;
        [Tooltip("背景の番号")] public Sprite _background;
        [Tooltip("ライトの色")] public Color _color;
        public int _finishIndex;
        public NextScene _nextScene;
    }

    public List<Sequence> _storySequence;

}

public enum NextScene
{
    Story,
    Adventure
}

