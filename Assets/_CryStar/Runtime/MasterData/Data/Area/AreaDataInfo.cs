using System;

/// <summary>
/// MasterAreaData - インゲーム中の背景用のマスタ
/// </summary>
[Serializable]
public class AreaDataInfo
{
    public int id; // ID
    public string variableName; // 変数名
    public string displayName; // 表示名
    public TimeVariants timeVariants; // 時間帯ごとのPath
}