using System;
using System.Collections.Generic;
using System.Linq;
using CryStar.Core;
using CryStar.PerProject;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace iCON.UI
{
    /// <summary>
    /// キャラクターの立ち絵表示を管理するクラス
    /// </summary>
    public class UIContents_Characters : CustomBehaviour
    {
        [SerializeField] private List<ViewData> _characterButtonDataList = new List<ViewData>();
        
        [Header("Debug")]
        [SerializeField] private List<Sprite> _characterSpriteList = new List<Sprite>();
        
        private LocationManager _locationManager;
        
        #region Life cycle

        public override async UniTask OnBind()
        {
            await base.OnBind();
            
            _locationManager = ServiceLocator.GetLocal<LocationManager>();
            if (_locationManager == null)
            {
                LogUtility.Error($"[{typeof(UIContents_Characters)}] ローカルサービスから{typeof(LocationManager)}が取得できませんでした");
                return;
            }
            
            _locationManager.OnMoveCharacter += HandleMoveCharacter;
        }

        private void OnDestroy()
        {
            if (_locationManager != null)
            {
                _locationManager.OnMoveCharacter -= HandleMoveCharacter;
            }
        }

        #endregion
        
        /// <summary>
        /// キャラクターが移動した時にViewを更新する
        /// </summary>
        private void HandleMoveCharacter(LocationType location, CharacterType character)
        {
            var targetData = _characterButtonDataList.FirstOrDefault(data => data.LocationType == location);

            if (targetData == null)
            {
                return;
            }
            
            if (character == CharacterType.None)
            {
                // 非表示
                targetData.Button.image.enabled = false;
                return;
            }
            
            // TODO: 正しい実装に変える　targetData.Button.image.sprite
            targetData.Button.image.sprite = _characterSpriteList[(int)character];
            targetData.Button.image.enabled = true;
        }
    }

    [Serializable]
    public class ViewData
    {
        [SerializeField] private LocationType _locationType;
        [SerializeField] private CustomButton _button;
        
        public LocationType LocationType => _locationType;
        public CustomButton Button => _button;
    }
}
