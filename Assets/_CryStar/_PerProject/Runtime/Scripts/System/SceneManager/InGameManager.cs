using System;
using CryStar.Attribute;
using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.Data.Scene;
using CryStar.MasterData;
using CryStar.PerProject;
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

        private TimeBasedEventManager _timeBasedEventManager; // 時間区切りのイベントを管理しているクラス
        private SceneLoader _sceneLoader; // シーン遷移を管理しているクラス
        
        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            
            ServiceLocator.Register(this, ServiceType.Local);
        }

        public override async UniTask OnBind()
        {
            await base.OnBind();
            _timeBasedEventManager = ServiceLocator.GetLocal<TimeBasedEventManager>();
            _sceneLoader = ServiceLocator.GetGlobal<SceneLoader>();
        }
        
        public override async UniTask OnStart()
        {
            await base.OnStart();
            await MasterDataManager.Instance.PreloadAsync(typeof(MasterAreaData), typeof(MasterCharacter));
            
            // ストーリー再生時以外はゲームオブジェクトを非アクティブにしておく
            _storyOrchestrator.gameObject.SetActive(false);
            
            // 朝のイベントを開始する　TODO: 設計的な観点で、仮置き
            _timeBasedEventManager.ExecuteMorningEvent();
        }

        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                await _sceneLoader.LoadSceneAsync(new SceneTransitionData(SceneType.Title));
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