using System;
using CryStar.Attribute;
using CryStar.Core;
using CryStar.Core.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
        /// 時間帯変更のコールバック
        /// </summary>
        public event Action<TimeZoneType> OnTimeZoneChanged;

        /// <summary>
        /// ポーズ状態が切り替わったときのコールバック
        /// </summary>
        public event Action<bool> OnPause;
        
        [SerializeField, Comment("更新インターバル")] private float _defalutUpdateInterval = 2f;
        [SerializeField, Comment("早送り時の更新インターバル")]　private float _fastUpdateInterval = 1f;
        
        private const int WORK_START_HOUR = 9; // 1日の行動開始時間
        private const int AFTERNOON_HOUR = 11; // 昼の時間帯の切り替わりタイミング
        private const int EVENING_HOUR = 16; // 夕方の時間帯の切り替わりタイミング
        private const int WORK_END_HOUR = 18; // 夕方の行動終了時間
        private const int NIGHT_HOUR = 20; // 夜の行動開始時間
        private const int DAY_END_HOUR = 23; // 夜の行動終了時間
        private const int MINUTES_PER_UPDATE = 10; // 1更新ごとに進む時間。単位は分
        
        private float _updateInterval; // 更新インターバル
        private float _elapsedTime; // 経過時間
        private DateTime _currentTime; // 現在の時間
        private TimeZoneType _currentTimeZone; // 現在のタイムゾーン

        private bool _isPausing = true; // ポーズ中 最初は朝のイベントから始まるため、ポーズ状態にしておく
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
        
        /// <summary>
        /// 時間帯
        /// </summary>
        public TimeZoneType CurrentTimeZone => _currentTimeZone;

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
        /// ポーズ状態をトグルする
        /// </summary>
        public void TogglePause()
        {
            SetPause(!_isPausing);
        }

        /// <summary>
        /// ポーズ状態を設定する
        /// </summary>
        public void SetPause(bool isPause)
        {
            _isPausing = isPause;
            OnPause?.Invoke(isPause);
        }

        /// <summary>
        /// 早送り状態をトグルする
        /// </summary>
        public void ToggleFastForward()
        {
            _isFastUpdate = !_isFastUpdate;
            
            // 早送り中であれば早送り中の更新インターバルを、早送り中でなければデフォルトの更新インターバルを適用
            _updateInterval = _isFastUpdate ? _fastUpdateInterval : _defalutUpdateInterval;
        }

        /// <summary>
        /// 次のイベント時刻までスキップする
        /// </summary>
        public void SkipToNextEvent()
        {
            _currentTime = GetNextEventTime();
            CheckTimeZone();
        }
        
        /// <summary>
        /// 夜の時間に設定する
        /// </summary>
        public void SetNightTime()
        {
            _currentTime = new DateTime(_currentTime.Year, _currentTime.Month, _currentTime.Day, NIGHT_HOUR, 0, 0);
            SetTimeZone(TimeZoneType.Night);
        }

        /// <summary>
        /// 翌朝の時刻を設定する
        /// </summary>
        public void SetNextDayTime()
        {
            _currentTime = _currentTime.Date.AddDays(1).AddHours(WORK_START_HOUR);
            SetTimeZone(TimeZoneType.Morning);
        }

        #region Private Methods

        /// <summary>
        /// 時間帯を設定する
        /// </summary>
        private void SetTimeZone(TimeZoneType timeZoneType)
        {
            _currentTimeZone = timeZoneType;
            OnTimeZoneChanged?.Invoke(timeZoneType);
        }
        
        /// <summary>
        /// 時間を更新
        /// </summary>
        private void AdvanceTime()
        {
            _currentTime = _currentTime.AddMinutes(MINUTES_PER_UPDATE);
            OnTimeChanged?.Invoke();
            
            CheckTimeZone();
        }

        /// <summary>
        /// 時間帯の切り替わりを確認
        /// </summary>
        private void CheckTimeZone()
        {
            if (_currentTime.Minute != 0)
            {
                return;
            }
            
            if (_currentTime.Hour == AFTERNOON_HOUR)
            {
                // お昼の時間帯
                SetTimeZone(TimeZoneType.Afternoon);
            }
            if (_currentTime.Hour == EVENING_HOUR)
            {
                // 夕方の時間帯
                SetTimeZone(TimeZoneType.Evening);
            }
            else if (_currentTime.Hour == WORK_END_HOUR)
            {
                // コールバック呼び出しと、時間停止
                OnEveningEvent?.Invoke();
                SetPause(true);
            }
            else if (_currentTime.Hour >= DAY_END_HOUR)
            {
                OnFinishDay?.Invoke();
                SetPause(true);
            }
        }
        
        /// <summary>
        /// 次のイベント発生時刻を取得する
        /// </summary>
        private DateTime GetNextEventTime()
        {
            // 現在時刻が作業時間内であれば夕方イベントまで
            if (_currentTime.Hour < WORK_END_HOUR)
            {
                // NOTE: 夕方の時間変更がスキップ可能性があるため呼び出しておく
                SetTimeZone(TimeZoneType.Evening);
                return new DateTime(_currentTime.Year, _currentTime.Month, _currentTime.Day, WORK_END_HOUR, 0, 0);
            }
        
            // それ以外は1日の終了まで
            if (_currentTime.Hour < DAY_END_HOUR)
            {
                return new DateTime(_currentTime.Year, _currentTime.Month, _currentTime.Day, DAY_END_HOUR, 0, 0);
            }
        
            // 既に1日が終了している場合
            return default;
        }
        
        #endregion
    }
}