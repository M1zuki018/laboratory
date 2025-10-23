using System;
using CryStar.Attribute;
using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.PerProject;
using CryStar.Story.Orchestrators;
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
        [Header("インゲームの設定")] 
        [SerializeField] private float _updateInterval = 2f;
        
        [Header("ストーリー機能の設定")]
        [SerializeField, HighlightIfNull]
        private StoryOrchestrator _storyOrchestrator;

        [SerializeField]
        private PackSample_CanvasController_StorySelect _canvasController;
        
        private TimeController _timeController;

        /// <summary>
        /// ゲーム内時間の管理クラス
        /// </summary>
        public TimeController TimeController => _timeController;
        
        public override async UniTask OnStart()
        {
            await base.OnStart();

            // コンストラクタを呼び出し。ゲーム内時間を更新するインターバルを渡す
            _timeController = new TimeController(_updateInterval);
            
            ServiceLocator.Register(this, ServiceType.Local);
            
            // ストーリー再生時以外はゲームオブジェクトを非アクティブにしておく
            _storyOrchestrator.gameObject.SetActive(false);
        }

        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                await ServiceLocator.GetGlobal<SceneLoader>().LoadSceneAsync(new SceneTransitionData(SceneType.Title));
            }
            
            _timeController.Update();
        }
        
        public void PlayStory(int storyId)
        {
            _storyOrchestrator.gameObject.SetActive(true);
            _storyOrchestrator.PlayStoryAsync(storyId,
                () =>
                {
                    _storyOrchestrator.gameObject.SetActive(false);
                    _canvasController.Setup();
                }).Forget();
        }
    }
}