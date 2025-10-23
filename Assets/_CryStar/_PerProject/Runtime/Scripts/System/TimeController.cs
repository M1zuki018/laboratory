using System;
using CryStar.Attribute;
using CryStar.Core;
using CryStar.Core.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace CryStar.PerProject
{
    /// <summary>
    /// ゲーム内時間を管理するクラス
    /// </summary>
    public class TimeManager : CustomBehaviour
    {
        /// <summary>
        /// 時間が変わったタイミングで呼び出されるコールバック
        /// </summary>
        public event Action OnTimeChanged;
        
        /// <summary>
        /// 夕方のイベントのコールバック
        /// </summary>
        public event Action OnEveningEvent;
        
        /// <summary>
        /// 1日終了を通知するコールバック
        /// </summary>
        public event Action OnFinishDay;

        /// <summary>
        /// ポーズ状態が切り替わったときのコールバック
        /// </summary>
        public event Action<bool> OnPause;
        
        [SerializeField, Comment("更新インターバル")] private float _defalutUpdateInterval = 2f;
        [SerializeField, Comment("早送り時の更新インターバル")]　private float _fastUpdateInterval = 1f;
        
        private const int WORK_START_HOUR = 9; // 1日の行動開始時間
        private const int WORK_END_HOUR = 18; // 夕方の行動終了時間
        private const int EVENING_BREAK_END_HOUR = 20; // 夜の行動開始時間
        private const int DAY_END_HOUR = 23; // 夜の行動終了時間
        private const int MINUTES_PER_UPDATE = 10; // 1更新ごとに進む時間。単位は分
        
        private float _updateInterval; // 更新インターバル
        private float _elapsedTime; // 経過時間
        private DateTime _currentTime; // 現在の時間

        private bool _isPausing; // ポーズ中
        private bool _isFastUpdate; // 早送り中
        
        /// <summary>
        /// 年
        /// </summary>
        public int Year => _currentTime.Year;
        
        /// <summary>
        /// 月
        /// </summary>
        public int Month => _currentTime.Month;
        
        /// <summary>
        /// 日
        /// </summary>
        public int Day => _currentTime.Day;
        
        /// <summary>
        /// 時間
        /// </summary>
        public int Hour => _currentTime.Hour;
        
        /// <summary>
        /// 分
        /// </summary>
        public int Minute => _currentTime.Minute;
        
        /// <summary>
        /// 整形された文字列で日時と時刻を取得する
        /// </summary>
        public string GetTimeText => _currentTime.ToString("yyyy/MM/dd HH:mm");

        #region Life cycle

        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            ServiceLocator.Register(this, ServiceType.Local);
            _currentTime = new DateTime(2027, 2, 17, 9, 0, 0);
            
            // 更新インターバルは初期値はデフォルトで設定
            _updateInterval = _defalutUpdateInterval;
        }
        
        private void Update()
        {
            if (_isPausing)
            {
                // ポーズ中であればreturn
                return;
            }

            _elapsedTime += Time.deltaTime;
            
            if (_elapsedTime >= _updateInterval)
            {
                AdvanceTime();
                _elapsedTime = 0;
            }
        }

        #endregion

        /// <summary>
        /// ポーズ。現在ポーズ中であれば、ポーズ状態を解除する
        /// </summary>
        public void Pause()
        {
            _isPausing = !_isPausing;
            OnPause?.Invoke(_isPausing);
        }

        /// <summary>
        /// 早送り。現在早送り中であれば、早送り状態を解除する
        /// </summary>
        public void FastForward()
        {
            _isFastUpdate = !_isFastUpdate;
            
            // 早送り中であれば早送り中の更新インターバルを、早送り中でなければデフォルトの更新インターバルを適用
            _updateInterval = _isFastUpdate ? _fastUpdateInterval : _defalutUpdateInterval;
            
            Debug.Log(_updateInterval);
        }

        /// <summary>
        /// スキップ
        /// </summary>
        public void Skip()
        {
            // 夕方イベントの発生時刻よりも現在時刻が早い場合は、夕方イベントまでの時間をスキップする
            if (_currentTime.Hour < EVENING_BREAK_END_HOUR)
            {
                _currentTime = new DateTime(_currentTime.Year, _currentTime.Month, _currentTime.Day, WORK_END_HOUR, 0, 0);
                
                // イベントを呼び出すために、現在時刻の確認メソッドを呼び出し
                CheckTimeEvents();
                return;
            }
            
            // 1日の終了までスキップする
            _currentTime = new DateTime(_currentTime.Year, _currentTime.Month, _currentTime.Day, DAY_END_HOUR, 0, 0);
            CheckTimeEvents();
        }

        /// <summary>
        /// 時間を更新
        /// </summary>
        private void AdvanceTime()
        {
            _currentTime = _currentTime.AddMinutes(MINUTES_PER_UPDATE);
            OnTimeChanged?.Invoke();
            Debug.Log($"{GetTimeText}"); // TODO: とる
            
            CheckTimeEvents();
        }

        /// <summary>
        /// 夕方のイベント、1日の終了を確認
        /// </summary>
        private void CheckTimeEvents()
        {
            if (_currentTime.Hour == WORK_END_HOUR && _currentTime.Minute == 0)
            {
                OnEveningEvent?.Invoke();
                // 夕方休憩へジャンプ
                _currentTime = new DateTime(_currentTime.Year, _currentTime.Month, _currentTime.Day, EVENING_BREAK_END_HOUR, 0, 0);
            }
            else if (_currentTime.Hour >= DAY_END_HOUR)
            {
                OnFinishDay?.Invoke();
                // 翌日の朝へ
                _currentTime = _currentTime.Date.AddDays(1).AddHours(WORK_START_HOUR);
            }
        }
    }
}