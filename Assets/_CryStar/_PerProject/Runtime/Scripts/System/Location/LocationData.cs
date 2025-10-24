using CryStar.Utility;

namespace CryStar.PerProject
{
    /// <summary>
    /// キャラクターが配置される各場所のデータ
    /// </summary>
    public class LocationData
    {
        private readonly LocationType _locationType;
        private CharacterType _currentCharacter = CharacterType.None; // 現在この場所にいるキャラクター
        private int _lastAssignedTime = 0; // この場所にキャラクターが配置されたタイミング
        
        /// <summary>
        /// 場所の列挙型
        /// </summary>
        public LocationType LocationType => _locationType;

        /// <summary>
        /// 現在この場所にいるキャラクター
        /// </summary>
        public CharacterType CharacterType => _currentCharacter;
        
        /// <summary>
        /// 現在この場所にキャラクターがいるか
        /// </summary>
        public bool HasCharacter => _currentCharacter != CharacterType.None;
        
        /// <summary>
        /// この場所にキャラクターが配置されたタイミング
        /// </summary>
        public int LastAssignedTime => _lastAssignedTime;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LocationData(LocationType locationType)
        {
            _locationType = locationType;
        }

        /// <summary>
        /// この場所にキャラクターを配置する
        /// </summary>
        public void AssignCharacter(CharacterType character, int lastUpdate)
        {
            if (_currentCharacter != character)
            {
                _currentCharacter = character;
                _lastAssignedTime = lastUpdate;
            }
            else
            {
                LogUtility.Verbose($"{typeof(LocationData)}.{_locationType}: 同じキャラクターが指定されたため処理をスキップします {_currentCharacter}");
            }
        }
    }
}
