using CryStar.Attribute;
using UnityEngine;

namespace CryStar.PerProject
{
    /// <summary>
    /// インゲームのBGMのAddressableパスのデータを管理するスクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(fileName = "InGameBGMPathSO", menuName = "Scriptable Objects/InGameBGMPathSO")]
    public class InGameBGMPathSO : ScriptableObject
    {
        [SerializeField, Comment("日中")] private string _daytimePath;
        [SerializeField, Comment("夜")] private string _nightPath;
        
        /// <summary>
        /// 日中のBGMのパス
        /// </summary>
        public string DaytimePath => _daytimePath;
        
        /// <summary>
        /// 夜のBGMのパス
        /// </summary>
        public string NightPath => _nightPath;
    }
}
