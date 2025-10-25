using System;
using CryStar.Data.User;
using CryStar.Story.Attributes;
using CryStar.Story.Data;
using CryStar.Story.Enums;
using CryStar.Story.UI;
using CryStar.Utility;
using CryStar.Utility.Enum;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using iCON.System;

namespace CryStar.Story.Execution
{
    /// <summary>
    /// End - ストーリー終了処理
    /// </summary>
    [OrderHandler(OrderType.End)]
    public class EndOrderHandler : OrderHandlerBase, IDisposable
    {
        /// <summary>
        /// ストーリー終了を通知するコールバック
        /// </summary>
        private Action _endAction;
        
        public override OrderType SupportedOrderType => OrderType.End;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EndOrderHandler(Action endAction)
        {
            _endAction = endAction;
        }
        
        public override Tween HandleOrder(OrderData data, StoryView view)
        {
            // ログを流す
            LogUtility.Verbose("Story ended", LogCategory.System);

            // ストーリー読了を記録 TODO:
            //StoryUserData.AddData(data.SceneId);
            
            // NOTE: 0秒にすると処理が正常に行われないため、若干秒遅延させている
            return DOVirtual.DelayedCall(0.1f, () => HandleReset(view));
        }

        /// <summary>
        /// ストーリー終了時のアクションの登録を行う
        /// </summary>
        public void SetEndAction(Action endAction)
        {
            _endAction = endAction;
        }
        
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _endAction = null;
        }

        /// <summary>
        /// ストーリー終了時のリセット処理
        /// </summary>
        private void HandleReset(StoryView view)
        {
            // キャラクター立ち絵のリセット処理
            view.HideAllCharacters();
            view.ResetCharacters();

            // スチル非表示
            view.HideSteel(0);

            //ダイアログをリセット
            view.ResetTalk();
            view.ResetDescription();

            _endAction?.Invoke();
        }
    }
}
