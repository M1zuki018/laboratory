using System;
using CryStar.Attribute;
using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.Data.Scene;
using CryStar.MasterData;
using CryStar.PerProject;
using CryStar.PerProject.GameProgression;
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
        public event Action<int> OnFinishedPlay;
        
        [Header("ストーリー機能の設定")]
        [SerializeField, HighlightIfNull]
        private StoryOrchestrator _storyOrchestrator;

        [SerializeField]
        private PackSample_CanvasController_StorySelect _canvasController;

        [SerializeField] 
        private bool _storySkip;
        
        private TimeBasedEventManager _timeBasedEventManager; // 時間区切りのイベントを管理しているクラス
        private SceneLoader _sceneLoader; // シーン遷移を管理しているクラス
        private TimeManager _timeManager; // 時間帯を管理するクラス
        private CharacterLocationManager _locationManager; // キャラクター配置を管理しているクラス
        
        private GP_Route1 _gameProgression;
        
        public TimeBasedEventManager TimeBasedEventManager => _timeBasedEventManager;
        public TimeManager TimeManager => _timeManager;
        public CharacterLocationManager LocationManager => _locationManager;
        
        #region Life cycle
        
        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            
            ServiceLocator.Register(this, ServiceType.Local);
            _gameProgression = new GP_Route1(this);
        }

        public override async UniTask OnBind()
        {
            await base.OnBind();
            _timeBasedEventManager = ServiceLocator.GetLocal<TimeBasedEventManager>();
            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            _sceneLoader = ServiceLocator.GetGlobal<SceneLoader>();
            _locationManager = ServiceLocator.GetLocal<CharacterLocationManager>();
        }
        
        public override async UniTask OnStart()
        {
            await base.OnStart();
            await MasterDataManager.Instance.PreloadAsync(typeof(MasterAreaData), typeof(MasterCharacter), typeof(MasterAreaTalk));
            
            // ストーリー再生時以外はゲームオブジェクトを非アクティブにしておく
            _storyOrchestrator.gameObject.SetActive(false);
            _storyOrchestrator.OnFinishedPlay += OnFinishedPlay;

            if (_storySkip)
            {
                // ストーリースキップの場合は即座に朝のイベントを開始する　TODO: 設計的な観点で、仮置き
                _timeBasedEventManager.ExecuteMorningEvent();
            }
            else
            {
                await _gameProgression.Play();
            }
        }

        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                await _sceneLoader.LoadSceneAsync(new SceneTransitionData(SceneType.Title));
            }
        }

        private void OnDestroy()
        {
            _storyOrchestrator.OnFinishedPlay -= OnFinishedPlay;
        }
        
        #endregion
        
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