using CryStar.Core;
using Cysharp.Threading.Tasks;
using iCON.UI;
using UnityEngine;

namespace iCON.System
{
    /// <summary>
    /// Titleシーンの管理クラス
    /// </summary>
    public class TitleManager : WindowBase
    {
        [SerializeField] private CanvasController_Title _canvasController;

        #region Life cycle

        public override async UniTask OnStart()
        {
            await base.OnStart();
            
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        /// <summary>
        /// イベント購読
        /// </summary>
        private void SubscribeEvents()
        {
            _canvasController.OnStartButtonClicked += HandleStart;
        }

        /// <summary>
        /// イベントの購読解除
        /// </summary>
        private void UnsubscribeEvents()
        {
            _canvasController.OnStartButtonClicked -= HandleStart;
        }

        /// <summary>
        /// ゲーム開始処理
        /// </summary>
        private void HandleStart()
        {
            ServiceLocator.GetGlobal<SceneLoader>()
                .LoadSceneAsync(new SceneTransitionData(SceneType.InGame)).Forget();
        }
    }
}