using CryStar.Core;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using iCON.System;
using UnityEngine;

namespace CryStar.PerProject
{
    /// <summary>
    /// インゲームのBGMを管理するクラス
    /// </summary>
    public class InGameBGMHandler : CustomBehaviour
    {
        [SerializeField] private string _path;
        private AreaManager _areaManager;

        #region Life cycle

        public override async UniTask OnBind()
        {
            await base.OnBind();
            _areaManager = ServiceLocator.GetLocal<AreaManager>();
            if (_areaManager == null)
            {
                LogUtility.Error($"[{typeof(InGameBGMHandler)}] AreaManagerがローカルサービスから取得できませんでした");
                return;
            }

            _areaManager.OnChangedArea += HandleChangeArea;
        }

        private void Start()
        {
            // TODO: 仮
            AudioManager.Instance.PlayBGM(_path).Forget();
        }

        private void OnDestroy()
        {
            if (_areaManager != null)
            {
                _areaManager.OnChangedArea -= HandleChangeArea;
            }
        }

        #endregion
        
        /// <summary>
        /// エリア変更時にBGMを変更する
        /// </summary>
        private void HandleChangeArea(AreaType newArea)
        {
            // TODO: マスタデータから検索
        }
    }
}
