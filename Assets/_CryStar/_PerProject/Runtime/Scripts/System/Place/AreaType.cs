namespace CryStar.PerProject
{
    /// <summary>
    /// 移動可能なエリアの種類
    /// </summary>
    public enum AreaType
    {
        /// <summary>
        /// 研究室東（イーシャ/ルイーズ側）
        /// </summary>
        EastLab = 1,
        
        /// <summary>
        /// 研究室西（カリル/フィルウ側）
        /// </summary>
        WestLab = 2,
        
        /// <summary>
        /// 廊下 研究室を出てすぐ
        /// </summary>
        HallwayEntrance = 3,
        
        /// <summary>
        /// 廊下 真ん中あたり
        /// </summary>
        HallwayMiddle = 4,
        
        /// <summary>
        /// 廊下 さらに奥
        /// </summary>
        HallwayFarSection = 5,
        
        /// <summary>
        /// 廊下 サーバールーム前
        /// </summary>
        ServerRoomFront = 6,
    }
}
