using System;
using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using iCON.System;

namespace CryStar.PerProject
{
    /// <summary>
    /// 時間をベースにしたイベントを管理するクラス
    /// </summary>
    public class TimeBasedEventManager : CustomBehaviour
    {
        private TimeManager _timeManager; // 時間帯を管理するクラス
        private InGameManager _inGameManager;

        #region Life cycle

        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            ServiceLocator.Register(this, ServiceType.Local);
        }
        
        public override async UniTask OnBind()
        {
            await base.OnBind();
            
            InitializeTimeManager();
            _inGameManager = ServiceLocator.GetLocal<InGameManager>();
        }

        private void OnDestroy()
        {
            if (_timeManager != null)
            {
                _timeManager.OnEveningEvent -= HandleEveningEvent;
                _timeManager.OnFinishDay -= HandleFinishDay;
            }
        }

        #endregion

        #region Initialize

        /// <summary>
        /// TimeManager関連の初期化
        /// </summary>
        private void InitializeTimeManager()
        {
            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            if (_timeManager == null)
            {
                LogUtility.Error($"[{typeof(TimeBasedEventManager)}] {typeof(TimeManager)}がローカルサービスから取得できませんでした)]");
                return;
            }

            _timeManager.OnEveningEvent += HandleEveningEvent;
            _timeManager.OnFinishDay += HandleFinishDay;
        }

        #endregion

        /// <summary>
        /// 朝のイベントを実行する
        /// </summary>
        public void ExecuteMorningEvent()
        {
            if (_inGameManager != null)
            {
                // 朝のシナリオを再生。再生後にポーズ状態を解除する
                // TODO: ID取得を行えるように
                _inGameManager.PlayStory(9, DayStart);
            }
        }
        
        /// <summary>
        /// 夕方のイベント用のハンドラー
        /// </summary>
        private void HandleEveningEvent()
        {
            if (_inGameManager != null)
            {
                // 夕方のシナリオを再生。再生後に夜の時間を始める
                // TODO: ID取得を行えるように
                _inGameManager.PlayStory(7, SetNightTime);
            }
        }

        /// <summary>
        /// 1日が終了したときのイベント用のハンドラー
        /// </summary>
        private void HandleFinishDay()
        {
            if (_inGameManager != null)
            {
                // 夕方のシナリオを再生。再生後に夜の時間を始める
                // TODO: ID取得を行えるように
                _inGameManager.PlayStory(8, SetNextDayTime);
            }
        }
        
        /// <summary>
        /// 夜の時間にする時の処理
        /// </summary>
        private void SetNightTime()
        {
            if (_timeManager != null)
            {
                // TODO: 切り替え演出やライティング変化などの処理を追加する
                _timeManager.SetNightTime();
                _timeManager.SetPause(false);
            }
        }

        /// <summary>
        /// 翌朝の時間を設定する
        /// </summary>
        private void SetNextDayTime()
        {
            if (_timeManager != null)
            {
                // TODO: 演出の処理を追加する
                _timeManager.SetNextDayTime();
                
                // 朝のイベントを実行
                ExecuteMorningEvent();
            }
        }
        
        /// <summary>
        /// 1日の時間の進行を始める
        /// </summary>
        private void DayStart()
        {
            if (_timeManager != null)
            {
                // ポーズ状態解除
                _timeManager.SetPause(false);
            }
        }
    }
}
