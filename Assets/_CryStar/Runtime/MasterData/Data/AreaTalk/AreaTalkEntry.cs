using System;
using System.Collections.Generic;

/// <summary>
/// 場所キーと対応するストーリーIDのデータクラス
/// </summary>
[Serializable]
public class AreaTalkEntry
{
    /// <summary>
    /// 8桁のキー（キャラクター配置を表す7桁＋回数を表す末尾1桁）
    /// もしくはキャラクターID
    /// </summary>
    public string key;

    /// <summary>
    /// ストーリーIDリスト
    /// </summary>
    public List<int> idList;
}
