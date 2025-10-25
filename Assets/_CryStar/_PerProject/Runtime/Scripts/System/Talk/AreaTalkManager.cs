using System;
using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using iCON.System;
using UnityEngine;

namespace CryStar.PerProject
{
    /// <summary>
    /// エリアでの会話を管理するクラス
    /// </summary>
    public class AreaTalkManager : CustomBehaviour
    {
        private CharacterLocationManager _locationManager; // キャラクターの位置データを管理するクラス
        private TimeManager _timeManager; // ゲーム内時間を管理するクラス
        private FlickInputDetector _flickDetector; // フリック入力を管理するクラス
        private AreaManager _areaManager; // プレイヤーの現在位置を管理するクラス
        private InGameManager _inGameManager;
        
        #region Life cycle

        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            ServiceLocator.Register(this, ServiceType.Local);
        }

        public override async UniTask OnBind()
        {
            await base.OnBind();
            _locationManager = ServiceLocator.GetLocal<CharacterLocationManager>();
            if (_locationManager == null)
            {
                LogUtility.Error($"[{typeof(AreaTalkManager)}] {typeof(CharacterLocationManager)} がローカルサービスから取得できませんでした");
            }
            
            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            if (_timeManager == null)
            {
                LogUtility.Error($"[{typeof(AreaTalkManager)}] {typeof(TimeManager)} がローカルサービスから取得できませんでした");
            }

            _flickDetector = ServiceLocator.GetLocal<FlickInputDetector>();
            if (_flickDetector == null)
            {
                LogUtility.Error($"[{typeof(AreaTalkManager)}] {typeof(FlickInputDetector)} がローカルサービスから取得できませんでした");
            }
            
            _areaManager = ServiceLocator.GetLocal<AreaManager>();
            if (_areaManager == null)
            {
                LogUtility.Error($"[{typeof(AreaTalkManager)}] {typeof(AreaManager)} がローカルサービスから取得できませんでした");
            }
            else
            {
                _areaManager.OnChangedArea += HandleChangeArea;
            }
            
            _inGameManager = ServiceLocator.GetLocal<InGameManager>();
            if (_inGameManager == null)
            {
                LogUtility.Error($"[{typeof(AreaTalkManager)}] {typeof(InGameManager)} がローカルサービスから取得できませんでした");
            }
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
        /// Viewからタップされたキャラクターの位置情報が渡されるため
        /// それを元にエリア会話を再生する
        /// </summary>
        public void PlayAreaTalk(LocationType location, Action endAction = null)
        {
            // TODO: クリックされた位置情報とそのキャラクターを元に適切なストーリーIDをマスタデータから検索して流すようにする
            // クリックされた位置にいるキャラクター情報を取得
            var characterId = _locationManager.GetCharacterId(location);
            
            // 現在のキャラクターの配置状況を取得　TODO: 仮
            var currentStateId = _locationManager.GetCurrentStateID();
            
            // TODO: 仮
            ExecuteAreaTalk(characterId + 2, endAction);
        }

        /// <summary>
        /// 会話イベント実行
        /// </summary>
        private void ExecuteAreaTalk(int playStoryId, Action endAction = null)
        {
            // 会話開始時にゲーム内時間の進行を止める、フリック入力を受け付けないようにする
            _timeManager.TogglePause();
            _flickDetector.SetTracking(false);
            
            // 会話を再生する
            _inGameManager.PlayStory(playStoryId, () =>
            {
                HandleTalkFinished();
                endAction?.Invoke();
            });
        }

        /// <summary>
        /// 会話イベント終了時の処理
        /// </summary>
        private void HandleTalkFinished()
        {
            _timeManager.TogglePause();
            _flickDetector.SetTracking(true);
        }

        /// <summary>
        /// エリアを移動したときの処理
        /// </summary>
        private void HandleChangeArea(AreaType newArea)
        {
            if (newArea == AreaType.HallwayEntrance)
            {
                // 廊下に移動した場合に会話を発生させたい
                // TODO: 仮の処理
                ExecuteAreaTalk(2);
            }
        }
    }
}