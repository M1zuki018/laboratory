using CryStar.Story.Attributes;
using CryStar.Story.Data;
using CryStar.Story.Enums;
using CryStar.Story.Execution;
using CryStar.Story.UI;
using DG.Tweening;

namespace iCON.System
{
    /// <summary>
    /// ResetTexts - オーダーの説明
    /// </summary>
    [OrderHandler(OrderType.ResetTexts)]
    public class ResetTextsOrderHandler : OrderHandlerBase
    {
        public override OrderType SupportedOrderType => OrderType.ResetTexts;
        
        public override Tween HandleOrder(OrderData data, StoryView view)
        {
            view.ResetTalk();
            view.ResetDescription();
            return null;
        }
    }
}