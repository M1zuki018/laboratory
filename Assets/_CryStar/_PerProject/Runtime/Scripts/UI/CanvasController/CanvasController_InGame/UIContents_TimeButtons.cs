using CryStar.Attribute;
using CryStar.Core;
using CryStar.PerProject;
using UnityEngine;
using UnityEngine.UI;

namespace iCON.UI
{
    /// <summary>
    /// 時間管理に関連するボタンを管理するクラス
    /// </summary>
    public class UIContents_TimeButtons : MonoBehaviour
    {
        [SerializeField, HighlightIfNull] private Button _pauseButton;
        [SerializeField, HighlightIfNull] private Button _fastUpdateButton;
        [SerializeField, HighlightIfNull] private Button _skipButton;

        private TimeManager _timeManager; // 時間管理を行うクラス

        /// <summary>
        /// Setup
        /// </summary>
        private void Awake()
        {
            // イベント登録
            if(_pauseButton != null) _pauseButton.onClick.AddListener(HandleClickPauseButton);
            if(_fastUpdateButton != null) _fastUpdateButton.onClick.AddListener(HandleClickFastUpdateButton);
            if(_skipButton != null) _skipButton.onClick.AddListener(HandleClickSkipButton);
        }

        private void OnDestroy()
        {
            if(_pauseButton != null) _pauseButton.onClick?.RemoveAllListeners();
            if(_fastUpdateButton != null) _fastUpdateButton.onClick?.RemoveAllListeners();
            if(_skipButton != null) _skipButton.onClick?.RemoveAllListeners();
        }
        
        /// <summary>
        /// ポーズボタンを押したときの処理
        /// </summary>
        private void HandleClickPauseButton()
        {
            if (IsTimeManagerAvailable())
            {
                _timeManager.TogglePause();
            }
        }

        /// <summary>
        /// 早送りボタンを押したときの処理
        /// </summary>
        private void HandleClickFastUpdateButton()
        {
            if (IsTimeManagerAvailable())
            {
                _timeManager.ToggleFastForward();
            }
        }

        /// <summary>
        /// スキップボタンを押したときの処理
        /// </summary>
        private void HandleClickSkipButton()
        {
            if (IsTimeManagerAvailable())
            {
                _timeManager.SkipToNextEvent();
            }
        }

        /// <summary>
        /// TimeManagerが利用可能かどうかを確認する
        /// </summary>
        private bool IsTimeManagerAvailable()
        {
            if (_timeManager != null)
            {
                return true;
            }
    
            _timeManager = ServiceLocator.GetLocal<TimeManager>();
            return _timeManager != null;
        }
    }
}
