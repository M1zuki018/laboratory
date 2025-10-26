using System;

/// <summary>
/// MasterCharacter
/// </summary>
[Serializable]
public class CharacterInfo
{
    public int id;
    public string displayName;
    public string fullName;
    public string englishName;
    public int internalId;
    public int permissionLevel;
    public string characterColor;
    public float textSpeed;
    public FacialExpressionVariants facialExpressionVariants;
}
