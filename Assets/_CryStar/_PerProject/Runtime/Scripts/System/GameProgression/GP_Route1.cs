using System;
using Cysharp.Threading.Tasks;
using iCON.System;

namespace CryStar.PerProject.GameProgression
{
    public class GP_Route1 : IDisposable
    {

        private readonly InGameManager _inGameManager;

        public GP_Route1(InGameManager inGameManager)
        {
            _inGameManager = inGameManager;
            _inGameManager.OnFinishedPlay += HandleFinishPlay;
        }

        public void Dispose()
        {
            _inGameManager.OnFinishedPlay -= HandleFinishPlay;
        }

        public async UniTask Play()
        {
            _inGameManager.TimeManager.SetForcedPause(true);
            await Opening();
        }
        
        // ==================================================//
        // Helper Methods
        // ==================================================//

        private void HandleFinishPlay(int storyId)
        {
            switch (storyId)
            {
                case 3:
                    AdvanceTime(10);
                    _inGameManager.LocationManager.ForcedCharacterEntry(LocationType.WestLab_Center, CharacterType.Khalil); // 中央にカリルを強制登場
                    break;
                case 4:
                    AdvanceTime(10); // 10分進めて9:40
                    _inGameManager.LocationManager.ForcedCharacterEntry(LocationType.WestLab_Center, CharacterType.Filou, true); // 中央にフィルウを強制登場
                    break;
                case 5:
                    _inGameManager.LocationManager.ForcedCharacterEntry(LocationType.WestLab_Center, CharacterType.Khalil, true); // 中央にカリルを強制登場
                    AdvanceTime(20); // 20分進めて10:00
                    break;
                case 6:
                    _inGameManager.LocationManager.ForcedCharacterEntry(LocationType.EastLab_Front, CharacterType.Isha, true); // 東手前にイーシャを強制登場
                    AdvanceTime(10); // 10分進めて10:10
                    break;
                case 7:
                    _inGameManager.LocationManager.ForcedCharacterEntry(LocationType.WestLab_Center, CharacterType.Khalil, true); // 中央にカリルを強制登場
                    AdvanceTime(20); // 20分進めて10:30
                    break;
                case 8:
                    _inGameManager.LocationManager.ForcedCharacterEntry(LocationType.EastLab_RearRight, CharacterType.Yule, true); // 東右奥にユールを強制登場
                    AdvanceTime(10); // 10分進めて10:40
                    break;
                case 9:
                    _inGameManager.LocationManager.ForcedCharacterEntry(LocationType.WestLab_Center, CharacterType.Khalil, true); // 中央にカリルを強制登場
                    AdvanceTime(20); // 10分進めて11:00
                    break;
                case 10:
                    _inGameManager.TimeManager.SetForcedPause(false);// 時間を通常通り進める
                    AdvanceTime(10); // 10分進めて11:10
                    Day1LunchEvent(); // 13:00のランチイベントを登録
                    break;
                case 11:
                    Episode13(); // 終わったら90分進めて14:30
                    break;
                case 12:
                    break;
            }
        }

        private void AdvanceTime(int minutes)
        {
            _inGameManager.TimeManager.AdvanceTime(minutes);
        }

        // ==================================================//
        // 進行用
        // ==================================================//

        private async UniTask Opening()
        {
            // ストーリーの事前プリロードを先に行っておく
            await _inGameManager.PreloadStoryAsync(new int[3] { 1, 2, 3 });
            _inGameManager.PlayStory(1, Episode1);
        }

        private void Episode1()
        {
            _inGameManager.PlayStory(2, Day1MorningEvent);
        }

        private void Day1MorningEvent()
        {
            AudioManager.Instance.PlayBGMWithFadeIn("Assets/AssetStoreTools/Sounds/BGM/BGM_RainAndHydrangeas.mp3", 0.5f).Forget();
            _inGameManager.TimeManager.SetTime(9, 20);
            _inGameManager.PlayStory(3); 
            _inGameManager.PreloadStoryAsync(new int[2] { 4, 5 }).Forget();
        }

        private void Day1LunchEvent()
        {
            _inGameManager.TimeManager.OnLunchTimeEvent += HandleDay1LunchEvent;
        }

        private void HandleDay1LunchEvent()
        {
            _inGameManager.TimeManager.SetForcedPause(true);// ストーリー用強制ポーズ
            _inGameManager.TimeManager.OnLunchTimeEvent -= HandleDay1LunchEvent;
        }

        private void Episode13()
        {
            _inGameManager.PlayStory(12, () => AdvanceTime(90));
        }
    }
}