using CryStar.Attribute;
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
        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField, ExpandableSO] private InGameBGMPathSO _pathSO;
        [SerializeField] private bool _debugMode = false;
        private TimeManager _timeManager;

        #region Life cycle

        public override async UniTask OnBind()
        {
            await base.OnBind();
            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            if (_timeManager == null)
            {
                LogUtility.Error($"[{typeof(InGameBGMHandler)}] {typeof(TimeManager)}がローカルサービスから取得できませんでした");
                return;
            }

            _timeManager.OnTimeZoneChanged += HandleChangeArea;
            
            if (_pathSO == null)
            {
                LogUtility.Error($"[{typeof(InGameBGMHandler)}] BGMパスのスクリプタブルオブジェクトが設定されていません");
            }
        }

        private void Start()
        {
            if (_debugMode)
            {
                AudioManager.Instance.PlayBGMWithFadeIn(_pathSO.DaytimePath, _fadeTime).Forget();
            }
        }

        private void OnDestroy()
        {
            if (_timeManager != null)
            {
                _timeManager.OnTimeZoneChanged += HandleChangeArea;
            }
        }

        #endregion
        
        /// <summary>
        /// 時間帯が変更されたときにBGMを変更する
        /// </summary>
        private void HandleChangeArea(TimeZoneType newTimeZone)
        {
            if (newTimeZone == TimeZoneType.Morning)
            {
                // 朝になった時に日中のBGMの再生を始める
                AudioManager.Instance.CrossFadeBGM(_pathSO.DaytimePath, _fadeTime).Forget();
            }
            else if (newTimeZone == TimeZoneType.Night)
            {
                AudioManager.Instance.CrossFadeBGM(_pathSO.NightPath, _fadeTime).Forget();
            }
        }
    }
}
