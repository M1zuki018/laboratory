using System;
using CryStar.Core;
using CryStar.PerProject;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace iCON.UI
{
    /// <summary>
    /// インゲーム中の背景素材を管理するクラス
    /// </summary>
    public class UIContents_InGameBackground : CustomBehaviour
    {
        [SerializeField] private CustomImage _background;
        
        [Header("デバッグ用")] 
        [SerializeField] private Text _areaText;
        
        private AreaManager _areaManager; // 場所の管理
        private TimeManager _timeManager; // ゲーム内時間を管理
        private TimeBasedEventManager _timeBasedEventManager; // 時間帯を切り替えるイベントの管理
        
        public override async UniTask OnBind()
        {
            await base.OnBind();
            InitializeTimeManager();
            InitializeTimeBasedEvent();
            await InitializeBackground();
        }

        private void OnDestroy()
        {
            if(_areaManager != null) _areaManager.OnChangedArea -= ChangeBackgroundSprite;
        }

        #region Initialize
        
        /// <summary>
        /// 背景の初期化を行う
        /// </summary>
        private async UniTask InitializeBackground()
        {
            _areaManager = ServiceLocator.GetLocal<AreaManager>();
            if (_areaManager == null)
            {
                // 正常に取得できなかった場合はエラーログを出したうえで
                // テキストが表示されないようにする
                LogUtility.Error($"[{nameof(CanvasController_InGame)}]{nameof(_areaManager)} が取得できませんでした");
                return;
            }
            
            // TODO: _backgroundの画像を適切なエリアの画像に差し替える処理
            _areaText.text = _areaManager.CurrentArea.ToString(); // TODO: デバッグ用　後で消す
            
            // エリア移動時に背景素材を変更できるようにメソッドを登録
            _areaManager.OnChangedArea += ChangeBackgroundSprite;
        }

        private void InitializeTimeManager()
        {
            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            if (_timeManager == null)
            {
                LogUtility.Error($"[{nameof(CanvasController_InGame)}]{nameof(_timeManager)} が取得できませんでした");
            }
        }
        
        private void InitializeTimeBasedEvent()
        {
            _timeBasedEventManager = ServiceLocator.GetLocal<TimeBasedEventManager>();
            if (_timeBasedEventManager == null)
            {
                LogUtility.Error($"[{nameof(CanvasController_InGame)}]{nameof(_timeBasedEventManager)} が取得できませんでした");
                return;
            }
            
            // NOTE: TimeManagerからではなく、演出を挟んだタイミングで時間帯が変更されるため、
            // EventManagerのイベントのコールバックを購読して変更ができるようにする
            // TODO: 
            // _timeBasedEventManager
        }
        
        #endregion
        
        /// <summary>
        /// 移動したエリアに合わせて背景素材を変更する
        /// </summary>
        private void ChangeBackgroundSprite(AreaType areaType)
        {
            // TODO: 変更処理を作成。TImeManagerの時間帯も参考にする
            
            _areaText.text = areaType.ToString(); // TODO: デバッグ用　後で消す
        }
    }
}
