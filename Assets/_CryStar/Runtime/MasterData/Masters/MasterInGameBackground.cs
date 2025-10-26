using System.Collections.Generic;
using CryStar.PerProject;
using CryStar.Utility;
using UnityEngine;

namespace CryStar.MasterData
{
    /// <summary>
    /// インゲーム中の背景素材のマスターデータ
    /// </summary>
    public class MasterInGameBackground : AddressableJsonMasterBase<int, InGameBackgroundInfo>
    {
        public override LoadPriority Priority => LoadPriority.Cached;
        protected override string AddressOrLabel => MasterDataAddresses.INGAME_BACKGROUND;
        protected override void LoadFromJson(string json)
        {
            var backgroundJson = JsonUtility.FromJson<InGameBackgroundJson>(json);
        
            // Jsonから読み込むことができたデータで辞書を作成
            _data = new Dictionary<int, InGameBackgroundInfo>(backgroundJson.backgrounds.Count);
            foreach (var character in backgroundJson.backgrounds)
            {
                _data[character.id] = character;
            }
    
            LogUtility.Verbose($"[{typeof(MasterInGameBackground)}] Loaded {_data.Count} データ");
        }

        /// <summary>
        /// 背景Pathをタイムゾーンを指定して取得する
        /// </summary>
        public static string GetBackgroundPath(int backgroundId, TimeZoneType timeZone)
        {
            var master = MasterDataManager.Instance.Get<MasterInGameBackground>();
            var timeVariantsData = master._data[backgroundId].timeVariants;
            return timeZone switch
            {
                TimeZoneType.Morning => timeVariantsData.morning,
                TimeZoneType.Afternoon => timeVariantsData.afternoon,
                TimeZoneType.Evening => timeVariantsData.evening,
                TimeZoneType.Night => timeVariantsData.night,
                _ => timeVariantsData.morning,
            };
        }

        /// <summary>
        /// IDを元に表示名を取得する
        /// </summary>
        public static string GetDisplayName(int backgroundId)
        {
            var master = MasterDataManager.Instance.Get<MasterInGameBackground>();
            return master._data[backgroundId].displayName;
        }
    }
}
