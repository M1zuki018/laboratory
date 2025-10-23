using System;
using CryStar.Core;
using CryStar.Core.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CryStar.PerProject
{
    /// <summary>
    /// ゲーム内時間を管理するクラス
    /// </summary>
    public class TimeController : CustomBehaviour
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
        
        [SerializeField] private float _updateInterval = 2f;
        
        private const int WORK_START_HOUR = 9; // 1日の行動開始時間
        private const int WORK_END_HOUR = 18; // 夕方の行動終了時間
        private const int EVENING_BREAK_END_HOUR = 20; // 夜の行動開始時間
        private const int DAY_END_HOUR = 23; // 夜の行動終了時間
        private const int MINUTES_PER_UPDATE = 10; // 1更新ごとに進む時間。単位は分
        
        private float _elapsedTime; // 経過時間
        private DateTime _currentTime; // 現在の時間

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

        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            ServiceLocator.Register(this, ServiceType.Local);
            _currentTime = new DateTime(2027, 2, 17, 9, 0, 0);
        }
        
        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            
            if (_elapsedTime >= _updateInterval)
            {
                AdvanceTime();
                _elapsedTime = 0;
            }
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