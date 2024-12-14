using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SystemManager : SingletonMonoBehaviour<SystemManager>
{
    protected override bool _dontDestroyOnLoad { get { return true; } }

    [ContextMenuItem("Reset", "ResetIndex")]
    public Sequence _sequence;

    public static int _index, _sequenceIndex;
    Dictionary<Sequence, List<int>> _sequenceDic;


    void OnEnable()
    {
        _sequenceDic = new Dictionary<Sequence, List<int>>() {
            { Sequence.�ڊo��, new List<int> { 0, 0 }},
            { Sequence.�A��, new List<int> { 1, 21 }},
            { Sequence.�A�K�X�e�B�[�A, new List<int> { 2, 24}},
        };

        _sequenceIndex = _sequenceDic[_sequence][0];
        _index = _sequenceDic[_sequence][1];
    }

    void ResetIndex()
    {
        _sequenceIndex = _sequenceDic[_sequence][0];
        _index = _sequenceDic[_sequence][1];
    }
}

public enum Sequence
{
    �ڊo��,
    �A��,
    �A�K�X�e�B�[�A
}
