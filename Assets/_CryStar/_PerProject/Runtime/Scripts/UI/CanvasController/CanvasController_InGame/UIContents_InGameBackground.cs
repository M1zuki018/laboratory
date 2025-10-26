using CryStar.Core;
using CryStar.MasterData;
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
        
        public override async UniTask OnBind()
        {
            await base.OnBind();
            InitializeTimeManager();
        }
        
        private async void Start()
        {
            // 背景マスタのプリロード
            await MasterDataManager.Instance.GetAsync<MasterAreaData>();
            InitializeBackground();
        }

        private void OnDestroy()
        {
            if(_areaManager != null) _areaManager.OnChangedArea -= ChangeBackgroundSprite;
            if (_timeManager != null) _timeManager.OnTimeZoneChanged -= HandleTimeZoneChanged;
        }

        #region Initialize
        
        /// <summary>
        /// 背景の初期化を行う
        /// </summary>
        private void InitializeBackground()
        {
            _areaManager = ServiceLocator.GetLocal<AreaManager>();
            if (_areaManager == null)
            {
                // 正常に取得できなかった場合はエラーログを出したうえで
                // テキストが表示されないようにする
                LogUtility.Error($"[{nameof(CanvasController_InGame)}]{nameof(_areaManager)} が取得できませんでした");
                return;
            }
            
            // エリア移動時に背景素材を変更できるようにメソッドを登録
            _areaManager.OnChangedArea += ChangeBackgroundSprite;
            
            var path = MasterAreaData.GetBackgroundPath((int)_areaManager.CurrentArea, _timeManager.CurrentTimeZone);
            ChangeBackground(path);
            
            _areaText.text = MasterAreaData.GetDisplayName((int)_areaManager.CurrentArea); // TODO: デバッグ用　とる
        }

        private void InitializeTimeManager()
        {
            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            if (_timeManager == null)
            {
                LogUtility.Error($"[{nameof(CanvasController_InGame)}]{nameof(_timeManager)} が取得できませんでした");
            }
            
            _timeManager.OnTimeZoneChanged += HandleTimeZoneChanged;
        }

        #endregion
        
        /// <summary>
        /// 移動したエリアに合わせて背景素材を変更する
        /// </summary>
        private void ChangeBackgroundSprite(AreaType areaType)
        {
            var path = MasterAreaData.GetBackgroundPath((int)areaType, _timeManager.CurrentTimeZone);
            ChangeBackground(path);
            
            _areaText.text = MasterAreaData.GetDisplayName((int)areaType); // TODO: デバッグ用　とる
        }
        
        /// <summary>
        /// 時間帯変更イベントに合わせて背景素材を変更する
        /// </summary>
        private void HandleTimeZoneChanged(TimeZoneType newTimeZone)
        {
            if (_timeManager != null)
            {
                var path = MasterAreaData.GetBackgroundPath((int)_areaManager.CurrentArea, newTimeZone);
                ChangeBackground(path);
            }
        }

        /// <summary>
        /// 背景を変更する
        /// </summary>
        private void ChangeBackground(string path)
        {
            _background.ChangeSpriteAsync(path).Forget();
        }
    }
}
