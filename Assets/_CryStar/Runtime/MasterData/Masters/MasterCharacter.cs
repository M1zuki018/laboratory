using System.Collections.Generic;
using CryStar.Story.Enums;
using CryStar.Utility;
using UnityEngine;

namespace CryStar.MasterData
{
    /// <summary>
    /// キャラクターのマスターデータ
    /// </summary>
    public class MasterCharacter : AddressableJsonMasterBase<int, CharacterInfo>
    {
        public override LoadPriority Priority => LoadPriority.Cached;
        protected override string AddressOrLabel => MasterDataAddresses.CHARACTER;

        protected override void LoadFromJson(string json)
        {
            var characterJson = JsonUtility.FromJson<CharacterJson>(json);

            // Jsonから読み込むことができたデータで辞書を作成
            _data = new Dictionary<int, CharacterInfo>(characterJson.characters.Count);
            foreach (var character in characterJson.characters)
            {
                _data[character.id] = character;
            }

            LogUtility.Verbose($"[{typeof(MasterCharacter)}] Loaded {_data.Count} データ");
        }

        /// <summary>
        /// キャラクター名を取得する
        /// </summary>
        public static string GetCharacterName(int characterId)
        {
            var master = MasterDataManager.Instance.Get<MasterCharacter>();
            return master._data[characterId].displayName;
        }

        /// <summary>
        /// キャラクターの表情差分のAddressableパスを取得する
        /// </summary>
        public static string GetExpressionPath(int characterId, FacialExpressionType facialExpressionType)
        {
            var master = MasterDataManager.Instance.Get<MasterCharacter>();
            var variants = master._data[characterId].facialExpressionVariants;
            return facialExpressionType switch
            {
                FacialExpressionType.Default => variants.defaultFace,
                _ => variants.defaultFace
            };
        }

        /// <summary>
        /// キャラクター情報を取得する
        /// </summary>
        public static CharacterInfo GetCharacterInfo(int characterId)
        {
            var master = MasterDataManager.Instance.Get<MasterCharacter>();
            return master._data[characterId];
        }
    }
}
