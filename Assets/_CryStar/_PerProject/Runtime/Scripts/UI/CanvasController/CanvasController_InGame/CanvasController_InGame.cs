using CryStar.Core;
using CryStar.PerProject;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using NotImplementedException = System.NotImplementedException;

namespace iCON.UI
{
    /// <summary>
    /// CanvasController_InGame
    /// </summary>
    public class CanvasController_InGame : WindowBase
    {
        [SerializeField] private Text _date;
        [SerializeField] private CustomImage _background;

        [Header("デバッグ用")] 
        [SerializeField] private Text _areaText;
        
        private TimeManager _timeManager; // 時間管理
        private AreaManager _areaManager; // 場所の管理

        public override async UniTask OnBind()
        {
            await base.OnBind();
            
            InitializeTimeView();
            await InitializeBackground();
        }
        
        #region 初期化メソッド

        /// <summary>
        /// 時間関連のViewの初期化を行う
        /// </summary>
        private void InitializeTimeView()
        {
            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            if (_timeManager == null)
            {
                // 正常に取得できなかった場合はエラーログを出したうえで
                // テキストが表示されないようにする
                LogUtility.Error($"[{nameof(CanvasController_InGame)}]{nameof(_timeManager)} が取得できませんでした");
                _date.enabled = false;
                return;
            }

            // 取得できている場合のみバインドを行い、テキストを初期化
            _timeManager.OnTimeChanged += UpdateDateText;
            _date.text = _timeManager.GetTimeText;
        }
        
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

        #endregion

        /// <summary>
        /// 移動したエリアに合わせて背景素材を変更する
        /// </summary>
        private void ChangeBackgroundSprite(AreaType areaType)
        {
            // TODO: 変更処理を作成
            
            _areaText.text = areaType.ToString(); // TODO: デバッグ用　後で消す
        }
        
        /// <summary>
        /// 日時の表記を更新する
        /// </summary>
        private void UpdateDateText()
        {
            if (_date != null && _timeManager != null)
            {
                _date.text = _timeManager.GetTimeText;
            }
        }
        
        private void OnDestroy()
        {
            if(_timeManager != null) _timeManager.OnTimeChanged -= UpdateDateText;
            if(_areaManager != null) _areaManager.OnChangedArea -= ChangeBackgroundSprite;
        }
    }
}