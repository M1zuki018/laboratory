using System;
using System.Collections.Generic;
using System.Linq;
using CryStar.Core;
using CryStar.Core.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CryStar.PerProject
{
    /// <summary>
    /// キャラクターが存在する位置を管理するクラス
    /// </summary>
    public class CharacterLocationManager : CustomBehaviour
    {
        public event Action<LocationType, CharacterType> OnMoveCharacter;
        
        [SerializeField] private int _movementDuration = 3; // 行動間隔

        private int _updateCount; // タイマー更新時から数えたカウント
        
        private TimeManager _timeManager; // 時間管理クラス
        private List<LocationData> _locationDataList = new List<LocationData>(); // キャラクターが存在できる位置データのリスト
        private Dictionary<CharacterType, int> _lastMovement = new Dictionary<CharacterType, int>(); // キャラクターと最終移動時間のkvp
        
        #region Life cycle

        /// <summary>
        /// Awake
        /// </summary>
        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            ServiceLocator.Register(this, ServiceType.Local);
        }
        
        /// <summary>
        /// Start
        /// </summary>
        public override async UniTask OnStart()
        {
            await base.OnStart();

            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            if (_timeManager != null)
            {
                _timeManager.OnTimeChanged += Lottery;
            }
            
            // Enumの定義分場所データを作成する
            for (int i = 0; i < Enum.GetValues(typeof(LocationType)).Length; i++)
            {
                var index = i;
                _locationDataList.Add(new LocationData((LocationType)index));
            }

            // Dictionaryに各キャラクターのkvpを追加
            _lastMovement.Add(CharacterType.Khalil, 0);
            _lastMovement.Add(CharacterType.Filou, 0);
            _lastMovement.Add(CharacterType.Isha, 0);
            _lastMovement.Add(CharacterType.Yule, 0);
        }

        /// <summary>
        /// Destroy
        /// </summary>
        private void OnDestroy()
        {
            if (_timeManager != null)
            {
                _timeManager.OnTimeChanged -= Lottery;
            }
        }

        #endregion

        /// <summary>
        /// 引数に渡されたLocationにキャラクターがいるか確認する
        /// </summary>
        public bool HasCharacter(LocationType targetLocation)
        {
            return _locationDataList[(int)targetLocation].HasCharacter;
        }

        /// <summary>
        /// 引数に渡されたキャラクターを取得する
        /// </summary>
        /// <param name="targetLocation"></param>
        /// <returns></returns>
        public CharacterType GetCharacter(LocationType targetLocation)
        {
            return _locationDataList[(int)targetLocation].CharacterType;
        }
        
        /// <summary>
        /// 抽選
        /// </summary>
        private void Lottery()
        {
            _updateCount++;
            
            for (int i = 1; i < 5; i++)
            {
                var character = (CharacterType)i;
                
                // 移動間隔が溜まっていなかったら次のキャラクターの抽選を行う
                if (_updateCount - _lastMovement[character] < _movementDuration)
                {
                    continue;
                }
                
                if (LotteryIsThere())
                {
                    // キャラクターが場面内にいる場合は抽選を行う
                    LotteryLocation(character);
                }
                else
                {
                    // 退場処理を呼び出す
                    ExitCharacter(character);
                }
            }
            
            // TODO: デバッグ用。とる
            Debug.Log($"西奥{_locationDataList[0].CharacterType}" +
                      $"西中央{_locationDataList[1].CharacterType}" +
                      $"西手前{_locationDataList[2].CharacterType}" +
                      $"東奥右{_locationDataList[3].CharacterType}" +
                      $"東奥左{_locationDataList[4].CharacterType}" +
                      $"東ドア前{_locationDataList[5].CharacterType}" +
                      $"東手前{_locationDataList[6].CharacterType}");
        }

        /// <summary>
        /// キャラクターの入退場をチェックする
        /// </summary>
        private bool LotteryIsThere()
        {
            return UnityEngine.Random.Range(0, 100) < 50;
        }
        
        /// <summary>
        /// 場所の抽選を行う
        /// </summary>
        private void LotteryLocation(CharacterType character)
        {
            var maxValue = _locationDataList.Count;
            var lotteryValue = UnityEngine.Random.Range(0, maxValue);

            if (_locationDataList[lotteryValue].CharacterType == character)
            {
                // もし自分がその場にいる場合はそのまま残留
                return;
            }
            
            // まだそこにキャラクターがいない場合はキャラクターを登録
            if (!_locationDataList[lotteryValue].HasCharacter)
            {
                // 現在いる場所で退場処理を呼び出し
                ExitCharacter(character);
                
                _locationDataList[lotteryValue].AssignCharacter(character, _updateCount);
                
                // 最終移動時間を更新
                _lastMovement[character] = _updateCount;
                
                // 決定した場所・キャラクターでコールバック呼び出し
                OnMoveCharacter?.Invoke((LocationType)lotteryValue, character);
            }
            else
            {
                // 既にキャラクターがいた場合は再度抽選を行う
                LotteryLocation(character);
            }
        }
        
        /// <summary>
        /// キャラクターを退場させる処理
        /// </summary>
        private void ExitCharacter(CharacterType character)
        {
            // キャラクターが退場する場合は、
            var targetData = _locationDataList
                .FirstOrDefault(data => data.CharacterType == character);

            if (targetData != null)
            {
                targetData.AssignCharacter(CharacterType.None, _updateCount);
                OnMoveCharacter?.Invoke(targetData.LocationType, CharacterType.None);
            }
            // 見つからなかった場合は何もしない
        }
    }
}
