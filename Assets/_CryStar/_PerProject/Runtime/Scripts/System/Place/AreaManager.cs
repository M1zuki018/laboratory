using System;
using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CryStar.PerProject
{
    /// <summary>
    /// 場所の情報を管理するクラス
    /// </summary>
    public class AreaManager : CustomBehaviour
    {
        /// <summary>
        /// エリアが変更されたときに呼び出されるコールバック
        /// </summary>
        public event Action<AreaType> OnChangedArea;
        
        private AreaType _currentArea = AreaType.WestLab; // 現在いるエリア
        private FlickInputDetector _flickInputDetector; // フリック入力を管理するクラス

        private const int USE_AREA_COUNT = 3; // 使用するエリア数
        
        /// <summary>
        /// 現在いるエリア
        /// </summary>
        public AreaType CurrentArea => _currentArea;

        #region Life cycle

        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            ServiceLocator.Register(this, ServiceType.Local);
        }
        
        public override async UniTask OnBind()
        {
            await base.OnBind();
            
            _flickInputDetector = ServiceLocator.GetLocal<FlickInputDetector>();

            if (_flickInputDetector == null)
            {
                LogUtility.Error($"[{typeof(AreaManager)}] {typeof(FlickInputDetector)} がサービスロケーターから取得できませんでした");
                return;
            }

            // 次のエリアへ移動
            _flickInputDetector.OnLeftFlick += MoveNextArea;
            
            // 前のエリアへ移動
            _flickInputDetector.OnRightFlick += MovePreviousArea;
        }

        private void OnDestroy()
        {
            _flickInputDetector.OnLeftFlick -= MoveNextArea;
            _flickInputDetector.OnRightFlick -= MovePreviousArea;
        }
        
        #endregion

        /// <summary>
        /// 引数でエリアを指定して移動処理を呼び出す
        /// </summary>
        public void ChangeArea(AreaType targetArea)
        {
            if (targetArea == _currentArea)
            {
                LogUtility.Verbose($"同じエリアに移動しようとしています。処理をスキップします");
                return;
            }
            
            // エリア変更。コールバックの呼び出しを行う
            _currentArea = targetArea;
            OnChangedArea?.Invoke(targetArea);
            
            Debug.Log($"エリア移動: {_currentArea}");
        }

        #region Private Methods
        
        /// <summary>
        /// 次のエリアへ移動する（左フリック時）
        /// </summary>
        private void MoveNextArea()
        {
            // 移動したいエリアの範囲は1~3
            var nextAreaIndex = (int)_currentArea + 1;
            if (nextAreaIndex > USE_AREA_COUNT)
            {
                LogUtility.Verbose($"[{typeof(AreaManager)}] これ以上移動できません");
                return;
            }
            
            ChangeArea((AreaType)nextAreaIndex);
        }

        /// <summary>
        /// 前のエリアへ移動する（右フリック時）
        /// </summary>
        private void MovePreviousArea()
        {
            // 移動したいエリアの範囲は1~3
            var previousAreaIndex = (int)_currentArea - 1;
            if (previousAreaIndex <= 0)
            {
                LogUtility.Verbose($"[{typeof(AreaManager)}] これ以上移動できません");
                return;
            }
            
            ChangeArea((AreaType)previousAreaIndex);
        }
        
        #endregion
    }
}
