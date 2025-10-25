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

        public override async UniTask OnBind()
        {
            await base.OnBind();
            await InitializeBackground();
        }

        private void OnDestroy()
        {
            if(_areaManager != null) _areaManager.OnChangedArea -= ChangeBackgroundSprite;
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
        
        /// <summary>
        /// 移動したエリアに合わせて背景素材を変更する
        /// </summary>
        private void ChangeBackgroundSprite(AreaType areaType)
        {
            // TODO: 変更処理を作成
            
            _areaText.text = areaType.ToString(); // TODO: デバッグ用　後で消す
        }
    }

}
