using System.Collections.Generic;
using CryStar.Utility;
using UnityEngine;

namespace CryStar.MasterData
{
    /// <summary>
    /// エリア会話のマスターデータ
    /// </summary>
    public class MasterAreaTalk : AddressableJsonMasterBase<string, List<int>>
    {
        private Dictionary<string, List<int>> _locationKeyBaseData = new Dictionary<string, List<int>>();
        private Dictionary<string, List<int>> _characterIDBaseData = new Dictionary<string, List<int>>();
        
        public override LoadPriority Priority => LoadPriority.Cached;
        protected override string AddressOrLabel => MasterDataAddresses.AREA_TALK_001;
        protected override void LoadFromJson(string json)
        {
            var areaTalkJson = JsonUtility.FromJson<AreaTalkJson>(json);
        
            // Jsonから読み込むことができたデータで辞書を作成
            
            // ロケーションキーを元にした複数キャラクターの会話
            _locationKeyBaseData = new Dictionary<string, List<int>>(areaTalkJson.locationKeyBaseInfo.talkDatas.Count);
            foreach (var character in areaTalkJson.locationKeyBaseInfo.talkDatas)
            {
                _locationKeyBaseData[character.key] = character.idList;
            }
            
            // キャラクターIDを元にしたキャラクター単体との会話
            _characterIDBaseData = new Dictionary<string, List<int>>(areaTalkJson.characterIDBaseInfo.talkDatas.Count);
            foreach (var character in areaTalkJson.characterIDBaseInfo.talkDatas)
            {
                _characterIDBaseData[character.key] = character.idList;
            }
    
            LogUtility.Verbose($"[{typeof(MasterAreaTalk)}] Loaded " +
                               $"\\n LocationKeyBase{_locationKeyBaseData.Count} データ" +
                               $"\\n CharacterIDBase{_characterIDBaseData} データ");
        }

        /// <summary>
        /// ロケーションキーを元にトークデータを取得する
        /// 取得できなかった場合はnull
        /// </summary>
        public static IReadOnlyList<int> GetLocationKeyBaseTalkData(string key)
        {
            var master = MasterDataManager.Instance.Get<MasterAreaTalk>();
            return master._locationKeyBaseData.ContainsKey(key) ? master._locationKeyBaseData[key] : null;
        }
        
        /// <summary>
        /// キャラクターIDを元にトークデータを取得する
        /// 取得できなかった場合はnull
        /// </summary>
        public static IReadOnlyList<int> GetCharacterIDBaseTalkData(string key)
        {
            var master = MasterDataManager.Instance.Get<MasterAreaTalk>();
            return master._characterIDBaseData.ContainsKey(key) ? master._characterIDBaseData[key] : null;
        }
    }
}