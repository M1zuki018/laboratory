using System;
using CryStar.Attribute;
using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.Data.Scene;
using CryStar.Story.Orchestrators;
using CryStar.Utility;
using CryStar.Utility.Enum;
using Cysharp.Threading.Tasks;
using iCON.UI;
using UnityEngine;

namespace iCON.System
{
    /// <summary>
    /// インゲームのGameManager
    /// </summary>
    public class InGameManager : CustomBehaviour
    {
        [Header("ストーリー機能の設定")]
        [SerializeField, HighlightIfNull]
        private StoryOrchestrator _storyOrchestrator;

        [SerializeField]
        private PackSample_CanvasController_StorySelect _canvasController;

        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            ServiceLocator.Register(this, ServiceType.Local);
        }
        
        public override async UniTask OnStart()
        {
            await base.OnStart();
            
            // ストーリー再生時以外はゲームオブジェクトを非アクティブにしておく
            _storyOrchestrator.gameObject.SetActive(false);
        }

        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                await ServiceLocator.GetGlobal<SceneLoader>().LoadSceneAsync(new SceneTransitionData(SceneType.Title));
            }
        }
        
        public void PlayStory(int storyId, Action endAction = null)
        {
            _storyOrchestrator.gameObject.SetActive(true);
            _storyOrchestrator.PlayStoryAsync(storyId,
                () =>
                {
                    _storyOrchestrator.gameObject.SetActive(false);
                    _canvasController.Setup();
                    endAction?.Invoke();
                }).Forget();
        }
        
        /// <summary>
        /// ストーリーの事前ロードを行う
        /// </summary>
        public async UniTask PreloadStoryAsync(int[] storyIdArray)
        {
            foreach (var storyId in storyIdArray)
            {
                await _storyOrchestrator.LoadSceneDataAsync(storyId);
            }
            LogUtility.Info($"{storyIdArray.Length}件 ストーリーのプリロードを行いました", LogCategory.System);
        }
    }
}