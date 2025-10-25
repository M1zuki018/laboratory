using System;
using UnityEngine;

namespace CryStar.PerProject
{
    [Serializable]
    public class SeData
    {
        [SerializeField] private string _path;
        [SerializeField, Range(0, 1)] private float _volume = 1;
        
        public string Path => _path;
        public float Volume => _volume;
    }
}
