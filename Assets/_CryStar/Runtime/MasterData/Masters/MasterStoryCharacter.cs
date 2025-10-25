// ============================================================================
// AUTO GENERATED - DO NOT MODIFY
// Generated at: 2025-07-25 16:27:56
// ============================================================================

using System.Collections.Generic;
using CryStar.Story.Data;
using CryStar.Story.Enums;
using UnityEngine;

/// <summary>
/// ストーリーキャラクター情報の定数クラス
/// </summary>
public static class MasterStoryCharacter
{
    private static readonly Dictionary<int, CharacterData> _characterData = new Dictionary<int, CharacterData>
    {
        {
            1, new CharacterData(1, "カリル・ラセール", "カリル", 
                new Color(0.863f, 0.078f, 0.235f, 1.000f), 0.07f,
                new Dictionary<FacialExpressionType, string>
                {
                    { FacialExpressionType.Default, "Assets/AssetStoreTools/Images/Characters/Khalil.png" },
                })
        },
        {
            2, new CharacterData(2, "フィルウ・ブランシャール", "フィルウ", 
                new Color(0.294f, 0.000f, 0.510f, 1.000f), 0.06f,
                new Dictionary<FacialExpressionType, string>
                {
                    { FacialExpressionType.Default, "Assets/AssetStoreTools/Images/Characters/Filou.png" },
                })
        },
        {
            3, new CharacterData(3, "イーシャ・バラット", "イーシャ", 
                new Color(0.294f, 0.000f, 0.510f, 1.000f), 0.06f,
                new Dictionary<FacialExpressionType, string>
                {
                    { FacialExpressionType.Default, "Assets/AssetStoreTools/Images/Characters/Iash.png" },
                })
        },
        {
            4, new CharacterData(4, "ユール・ウォード", "ユール", 
                new Color(0.294f, 0.000f, 0.510f, 1.000f), 0.06f,
                new Dictionary<FacialExpressionType, string>
                {
                    { FacialExpressionType.Default, "Assets/AssetStoreTools/Images/Characters/Yule.png" },
                })
        },
    };

    /// <summary>
    /// IDからキャラクターデータを取得
    /// </summary>
    public static CharacterData GetCharacter(int id)
    {
        return _characterData.GetValueOrDefault(id, null);
    }

    /// <summary>
    /// フルネームからキャラクターデータを取得
    /// </summary>
    public static CharacterData GetCharacterByName(string fullName)
    {
        foreach (var kvp in _characterData)
        {
            if (kvp.Value.FullName == fullName)
                return kvp.Value;
        }
        return null;
    }

    /// <summary>
    /// キャラクターの表情パスを取得
    /// </summary>
    public static string GetExpressionPath(int characterId, FacialExpressionType expression)
    {
        var character = GetCharacter(characterId);
        if (character.HasExpression(expression))
        {
            return character.GetExpressionPath(expression);
        }
        return null;
    }

    /// <summary>
    /// 全キャラクターのIDリストを取得
    /// </summary>
    public static IEnumerable<int> GetAllCharacterIds()
    {
        return _characterData.Keys;
    }

    /// <summary>
    /// 全キャラクターデータを取得
    /// </summary>
    public static IEnumerable<CharacterData> GetAllCharacters()
    {
        return _characterData.Values;
    }
}
