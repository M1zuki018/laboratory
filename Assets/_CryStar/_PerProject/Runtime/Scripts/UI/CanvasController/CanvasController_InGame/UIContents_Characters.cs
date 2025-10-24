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
        [SerializeField] private CanvasGroup _eastLab;
        [SerializeField] private CanvasGroup _westLab;
        [SerializeField] private List<ViewData> _characterButtonDataList = new List<ViewData>();
        
        [Header("Debug")]
        [SerializeField] private List<Sprite> _characterSpriteList = new List<Sprite>();
        
        private CharacterLocationManager _characterLocationManager; // キャラクターの場所を管理するクラス
        private AreaManager _areaManager; // 現在のプレイヤーの居場所を管理するクラス
        
        #region Life cycle

        public override async UniTask OnBind()
        {
            await base.OnBind();
            InitializeCharacterLocation();
            InitializeArea();
        }

        private void OnDestroy()
        {
            if (_characterLocationManager != null)
            {
                _characterLocationManager.OnMoveCharacter -= HandleMoveCharacter;
            }

            if (_areaManager != null)
            {
                _areaManager.OnChangedArea -= HandleChangeArea;
            }
        }

        #endregion
        
        #region 初期化
        
        /// <summary>
        /// CharacterLocationManager関連の初期化
        /// </summary>
        private void InitializeCharacterLocation()
        {
            _characterLocationManager = ServiceLocator.GetLocal<CharacterLocationManager>();
            if (_characterLocationManager == null)
            {
                LogUtility.Error($"[{typeof(UIContents_Characters)}] ローカルサービスから{typeof(CharacterLocationManager)}が取得できませんでした");
                return;
            }
            
            _characterLocationManager.OnMoveCharacter += HandleMoveCharacter;
        }

        /// <summary>
        /// AreaManager関連の初期化
        /// </summary>
        private void InitializeArea()
        {
            _areaManager = ServiceLocator.GetLocal<AreaManager>();
            if (_areaManager == null)
            {
                LogUtility.Error($"[{typeof(UIContents_Characters)}] ローカルサービスから{typeof(AreaManager)}が取得できませんでした");
                
                // 万が一取得できなかった場合は西側のキャンバスを有効化しておく
                // TODO: 対応方法検討
                EnableCanvas(_westLab, true);
                EnableCanvas(_eastLab, false);
                return;
            }

            _areaManager.OnChangedArea += HandleChangeArea;
            HandleChangeArea(_areaManager.CurrentArea);
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
        
        /// <summary>
        /// プレイヤーがエリア移動したときに利用するCanvasGroupを切り替える処理
        /// </summary>
        private void HandleChangeArea(AreaType targetArea)
        {
            switch (targetArea)
            {
                case AreaType.WestLab:
                    EnableCanvas(_westLab, true);
                    EnableCanvas(_eastLab, false);
                    break;
                case AreaType.EastLab:
                    EnableCanvas(_westLab, false);
                    EnableCanvas(_eastLab, true);
                    break;
                default:
                    EnableCanvas(_westLab, false);
                    EnableCanvas(_eastLab, false);
                break;
            }
        }

        /// <summary>
        /// キャンバスグループの有効/無効を切り替える
        /// </summary>
        private void EnableCanvas(CanvasGroup canvasGroup, bool enable)
        {
            canvasGroup.alpha = enable ? 1 : 0;
            canvasGroup.interactable = enable;
            canvasGroup.blocksRaycasts = enable;
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
