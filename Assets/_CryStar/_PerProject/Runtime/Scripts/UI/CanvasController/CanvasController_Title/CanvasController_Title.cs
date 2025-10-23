using System;
using CryStar.Attribute;
using CryStar.Core;
using Cysharp.Threading.Tasks;
using iCON.System;
using UnityEngine;
using UnityEngine.UI;

namespace iCON.UI
{
    /// <summary>
    /// CanvasController_Title
    /// </summary>
    public partial class CanvasController_Title : WindowBase
    {
        [SerializeField, HighlightIfNull] private CustomButton _startButton;
        [SerializeField] private Text _version;
        
        [Header("データ")]
        [SerializeField] private ProductDataSO _productDataSO;
        
        public event Action OnStartButtonClicked;
        
        public override UniTask OnAwake()
        {
            // イベント登録
            if(_startButton != null) _startButton.onClick.AddListener(HandleStartButtonClicked);

            if (_version != null)
            {
                // バージョンテキスト書き換え
                _version.text = $"version {_productDataSO.GetFullVersionString()}";
            }
            
            return base.OnAwake();
        }

        private void HandleStartButtonClicked()
        {
            OnStartButtonClicked?.Invoke();
        }

        private void OnDestroy()
        {
            if(_startButton != null) _startButton.onClick?.RemoveAllListeners();
        }
    }
}