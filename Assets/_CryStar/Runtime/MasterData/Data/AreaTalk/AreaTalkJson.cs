using System;

/// <summary>
/// MasterAreaTalk
/// </summary>
[Serializable]
public class AreaTalkJson
{
    /// <summary>
    /// 場所キーを元にした会話データ
    /// </summary>
    public AreaTalkInfo locationKeyBaseInfo;
    
    /// <summary>
    /// キャラクターIDを元にした会話データ
    /// </summary>
    public AreaTalkInfo characterIDBaseInfo;
}
