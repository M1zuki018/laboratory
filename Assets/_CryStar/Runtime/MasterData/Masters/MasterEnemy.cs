using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CryStar.MasterData
{
    public class MasterEnemy : AddressableJsonMasterBase<int, EnemyData>
    {
        public override LoadPriority Priority => LoadPriority.SceneLocal;
        
        private string _currentAddress;
        protected override string AddressOrLabel => _currentAddress;

        /// <summary>
        /// 特定バトルの敵データをロード
        /// </summary>
        public async UniTask LoadBattleAsync(int battleId)
        {
            _currentAddress = $"MasterData/Scenes/Battle_{battleId:000}/enemy_master";
            await LoadAsync();
        }

        protected override void LoadFromJson(string json)
        {
            var enemyJson = JsonUtility.FromJson<EnemyJson>(json);
            _data = new Dictionary<int, EnemyData>(enemyJson.enemies.Count);

            foreach (var enemy in enemyJson.enemies)
            {
                _data[enemy.enemyId] = enemy;
            }

            Debug.Log($"[{typeof(MasterEnemy)}] Loaded {_data.Count} enemies");
        }

        public static EnemyData GetEnemy(int enemyId)
        {
            var master = MasterDataManager.Instance.Get<MasterEnemy>();
            return master?.Get(enemyId);
        }
    }
   
}