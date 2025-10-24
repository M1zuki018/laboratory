using System;
using CryStar.Attribute;
using CryStar.Core;
using CryStar.Core.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CryStar.PerProject
{
    /// <summary>
    /// フリック入力を判定するクラス
    /// </summary>
    public class FlickInputDetector : CustomBehaviour
    {
        /// <summary>
        /// 左側へのフリックを検知した時のコールバック
        /// </summary>
        public event Action OnLeftFlick;

        /// <summary>
        /// 右側へのフリックを検知した時のコールバック
        /// </summary>
        public event Action OnRightFlick;

        [SerializeField, Comment("フリックしたと判定する距離(画面に対する割合)")]
        private float _minFlickPercentage = 0.1f;

        private Vector3 _startTouchPos; // タッチし始めた位置
        private Vector3 _endTouchPos; // 離した位置
        private float _minFlickDistance; // フリックしたと判定する距離（計算用）

        private bool _useTracking = true; // 入力の追跡を行うか（判定の取得をオフにしたい時はこの変数をfalseにする）

        #region Life cycle

        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            
            ServiceLocator.Register(this, ServiceType.Local);
            
            // 画面に対する割合で、フリックしたと判定する距離を計算する
            _minFlickDistance = Screen.width * _minFlickPercentage;
        }

        private void Update()
        {
            if (!_useTracking)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // フリック開始位置を記録
                _startTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            }

            if (Input.GetMouseButtonUp(0))
            {
                // 終了位置を記録
                _endTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

                // フリックしたかの判定を行う
                var flickDirection = CalculateFlickVector();
                ProcessFlick(flickDirection);
            }
        }

        #endregion

        /// <summary>
        /// 入力の追跡を行うかの切り替えを行う
        /// </summary>
        public void ToggleTracking()
        {
            _useTracking = !_useTracking;
        }

        /// <summary>
        /// 入力の追跡を行うか、引数で指定する
        /// </summary>
        public void SetTracking(bool useTracking)
        {
            _useTracking = useTracking;
        }

        #region フリック判定用のPrivate Methods

        /// <summary>
        /// フリックベクトルを計算
        /// </summary>
        private Vector3 CalculateFlickVector()
        {
            return _endTouchPos - _startTouchPos;
        }

        /// <summary>
        /// フリックした方向に合わせてコールバックを呼び出す
        /// </summary>
        private void ProcessFlick(Vector3 flickDirection)
        {
            if (flickDirection.magnitude < _minFlickDistance)
            {
                // フリックの最小距離を越えていなければフリック判定とみなさないので、早期return
                return;
            }

            // フリックした方向が右側であればx座標は正の数になっているはず
            if (flickDirection.x > 0)
            {
                OnRightFlick?.Invoke();
                Debug.Log("Right Flick"); // TODO: とる
            }
            else
            {
                OnLeftFlick?.Invoke();
                Debug.Log("Left Flick"); // TODO: とる
            }
        }

        #endregion
    }
}