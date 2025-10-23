using System;
using CryStar.Attribute;
using CryStar.Core;
using CryStar.PerProject;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace iCON.UI
{
    /// <summary>
    /// CanvasController_InGame
    /// </summary>
    public partial class CanvasController_InGame : WindowBase
    {
        [SerializeField, HighlightIfNull] private CustomButton _button;
        [SerializeField] private Text _date;
        
        /// <summary>
        /// 時間管理クラス
        /// </summary>
        private TimeController _timeController;
        
        public event Action OnButtonClicked;
                
        public override UniTask OnAwake()
        {
            // イベント登録
            if(_button != null) _button.onClick.AddListener(Temporary);
            
            return base.OnAwake();
        }

        public override async UniTask OnBind()
        {
            await base.OnBind();
            _timeController = ServiceLocator.GetLocal<TimeController>();
            if (_timeController == null)
            {
                // 正常に取得できなかった場合はエラーログを出したうえで
                // テキストが表示されないようにする
                LogUtility.Error($"[{nameof(CanvasController_InGame)}]{nameof(_timeController)} が取得できませんでした");
                _date.enabled = false;
                return;
            }

            // 取得できている場合のみバインドを行い、テキストを初期化
            _timeController.OnTimeChanged += UpdateDateText;
            _date.text = _timeController.GetTimeText;
        }
        
        
        private void Temporary()
        {
        }

        /// <summary>
        /// 日時の表記を更新する
        /// </summary>
        private void UpdateDateText()
        {
            if (_date != null && _timeController != null)
            {
                _date.text = _timeController.GetTimeText;
            }
        }
        
        private void OnDestroy()
        {
            if(_button != null) _button.onClick?.RemoveAllListeners();
            if(_timeController != null) _timeController.OnTimeChanged -= UpdateDateText;
        }
    }
}