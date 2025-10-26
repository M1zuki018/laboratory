using System.Collections.Generic;
using CryStar.PerProject;
using CryStar.Utility;
using UnityEngine;

namespace CryStar.MasterData
{
    /// <summary>
    /// インゲーム中のエリアデータのマスターデータ
    /// </summary>
    public class MasterAreaData : AddressableJsonMasterBase<int, AreaDataInfo>
    {
        public override LoadPriority Priority => LoadPriority.Cached;
        protected override string AddressOrLabel => MasterDataAddresses.AreaData;
        protected override void LoadFromJson(string json)
        {
            var backgroundJson = JsonUtility.FromJson<AreaDataJson>(json);
        
            // Jsonから読み込むことができたデータで辞書を作成
            _data = new Dictionary<int, AreaDataInfo>(backgroundJson.areaDatas.Count);
            foreach (var character in backgroundJson.areaDatas)
            {
                _data[character.id] = character;
            }
    
            LogUtility.Verbose($"[{typeof(MasterAreaData)}] Loaded {_data.Count} データ");
        }

        /// <summary>
        /// 背景Pathをタイムゾーンを指定して取得する
        /// </summary>
        public static string GetBackgroundPath(int backgroundId, TimeZoneType timeZone)
        {
            var master = MasterDataManager.Instance.Get<MasterAreaData>();
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
            var master = MasterDataManager.Instance.Get<MasterAreaData>();
            return master._data[backgroundId].displayName;
        }
    }
}
