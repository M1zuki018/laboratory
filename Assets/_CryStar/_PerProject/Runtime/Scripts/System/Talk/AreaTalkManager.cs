using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using iCON.System;

namespace CryStar.PerProject
{
    /// <summary>
    /// エリアでの会話を管理するクラス
    /// </summary>
    public class AreaTalkManager : CustomBehaviour
    {
        private CharacterLocationManager _locationManager; // キャラクターの位置データを管理するクラス
        private TimeManager _timeManager; // ゲーム内時間を管理するクラス
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
            
            _inGameManager = ServiceLocator.GetLocal<InGameManager>();
            if (_inGameManager == null)
            {
                LogUtility.Error($"[{typeof(AreaTalkManager)}] {typeof(InGameManager)} がローカルサービスから取得できませんでした");
            }
        }

        #endregion

        /// <summary>
        /// Viewからタップされたキャラクターの位置情報が渡されるため
        /// それを元にエリア会話を再生する
        /// </summary>
        public void PlayAreaTalk(LocationType location)
        {
            // 会話開始時にゲーム内時間の進行を止める
            _timeManager.TogglePause();
            
            // TODO: クリックされた位置情報とそのキャラクターを元に適切なストーリーIDをマスタデータから検索して流すようにする
            // 会話を再生する
            // 会話終了時にはゲーム内時間の進行を再開できるようにActionを渡す
            _inGameManager.PlayStory(2, _timeManager.TogglePause);
        }
    }
}