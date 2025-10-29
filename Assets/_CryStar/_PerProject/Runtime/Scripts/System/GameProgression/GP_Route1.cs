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
            
            // 1日目分のDispose
            _inGameManager.TimeManager.OnLunchTimeEvent -= HandleDay1LunchEvent;
            _inGameManager.TimeManager.OnEveningEvent -= HandleDay1EveningEvent;
            _inGameManager.TimeManager.OnFinishDay -= HandleDay1FinishDayEvent;
            
            // 2日目分のDispose
            _inGameManager.TimeManager.OnLunchTimeEvent -= HandleDay2LunchEvent;
            _inGameManager.TimeManager.OnEveningEvent -= HandleDay2EveningEvent;
            _inGameManager.TimeManager.OnFinishDay -= HandleDay2FinishDayEvent;
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
                    _inGameManager.TimeManager.SetPause(false);
                    AdvanceTime(10); // 10分進めて11:10
                    break;
                case 11:
                    AdvanceTime(90);
                    _inGameManager.PlayStory(12);
                    _inGameManager.LocationManager.ForcedCharacterEntry(LocationType.WestLab_Center, CharacterType.Khalil, true); // 中央にカリルを強制登場
                    break;
                case 12:
                    AdvanceTime(10);
                    break;
                case 13:
                    _inGameManager.TimeManager.SetForcedPause(false); // 更新停止状態を解除
                    _inGameManager.TimeManager.SetPause(false);
                    break;
                case 1002:
                    _inGameManager.PlayStory(14);
                    _inGameManager.TimeManager.SetNightTime(); // 夜に進める
                    break;
                case 14:
                    AdvanceTime(10);
                    _inGameManager.TimeManager.SetForcedPause(false); // 強制更新解除
                    _inGameManager.TimeManager.SetPause(false);
                    break;
                // 夜のイベントが実行（ここで強制更新停止）
                case 1003:
                    _inGameManager.TimeManager.SetNextDayTime(); // 次の日に進める
                    HandelDay2MorningEvent(); // 朝のイベントを実行する
                    break;
                case 1001:
                    _inGameManager.TimeManager.SetTime(8, 50);
                    _inGameManager.PlayStory(15);
                    break;
                case 15:
                    _inGameManager.PlayStory(16);
                    AdvanceTime(10); // 10分進めて9:00
                    break;
                case 16:
                    AdvanceTime(30); // 30分進めて9:30
                    _inGameManager.PlayStory(17);
                    break;
                case 17:
                    AdvanceTime(30); // 30分進めて10:00
                    _inGameManager.PlayStory(18);
                    break;
                case 18:
                    AdvanceTime(30); // 30分進めて10:30
                    _inGameManager.PlayStory(19);
                    break;
                case 19:
                    AdvanceTime(30); // 30分進めて11:00
                    _inGameManager.PlayStory(20);
                    break;
                case 20:
                    AdvanceTime(60); // 60分進めて12:00
                    _inGameManager.PlayStory(21);
                    break;
                case 21:
                    AdvanceTime(10); // 10分進めて12:10
                    _inGameManager.TimeManager.SetForcedPause(false); // 強制更新停止解除
                    _inGameManager.TimeManager.SetPause(false);
                    break;
                case 22: // 昼食イベント
                    _inGameManager.TimeManager.SetForcedPause(false);
                    _inGameManager.TimeManager.SetPause(false);
                    AdvanceTime(90);
                    break;
                case 23: // 夕方のイベント
                    _inGameManager.TimeManager.SetNightTime(); // 夜に変更
                    _inGameManager.TimeManager.SetForcedPause(false);
                    _inGameManager.TimeManager.SetPause(false);
                    break;
                case 24: // 夜のイベント
                    _inGameManager.TimeManager.SetForcedPause(true); // 強制更新停止
                    _inGameManager.PlayStory(25);
                    break;
                case 25:
                    _inGameManager.TimeManager.SetNextDayTime(); // 翌朝に日付をセット
                    _inGameManager.TimeManager.SetTime(4, 50); // 時刻設定 4:50
                    _inGameManager.PlayStory(26);
                    break;
                case 26:
                    AdvanceTime(10); // 5:00
                    _inGameManager.PlayStory(27);
                    break;
                case 27:
                    _inGameManager.TimeManager.SetTime(8, 0); // 時刻設定 8:00
                    _inGameManager.PlayStory(28);
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
            
            _inGameManager.TimeManager.OnLunchTimeEvent += HandleDay1LunchEvent;
            _inGameManager.TimeManager.OnEveningEvent += HandleDay1EveningEvent;
            _inGameManager.TimeManager.OnFinishDay += HandleDay1FinishDayEvent;
        }

        private void HandleDay1LunchEvent()
        {
            _inGameManager.TimeManager.SetForcedPause(true);// ストーリー用強制ポーズ
            _inGameManager.PlayStory(11);
            _inGameManager.TimeManager.OnLunchTimeEvent -= HandleDay1LunchEvent;
        }

        private void HandleDay1EveningEvent()
        {
            _inGameManager.TimeManager.SetForcedPause(true);// ストーリー用強制ポーズ
            _inGameManager.PlayStory(1002); // 共通夕方イベントストーリーを実行
            
            _inGameManager.TimeManager.OnEveningEvent -= HandleDay1EveningEvent;
        }

        private void HandleDay1FinishDayEvent()
        {
            _inGameManager.TimeManager.SetForcedPause(true);// ストーリー用強制ポーズ
            _inGameManager.PlayStory(1003); // 共通1日終了イベントストーリーを実行
            
            _inGameManager.TimeManager.OnEveningEvent -= HandleDay1FinishDayEvent;
            
            // 次の日のイベントを登録しておく
            _inGameManager.TimeManager.OnLunchTimeEvent += HandleDay2LunchEvent;
            _inGameManager.TimeManager.OnEveningEvent += HandleDay2EveningEvent;
            _inGameManager.TimeManager.OnFinishDay += HandleDay2FinishDayEvent;
        }

        private void HandelDay2MorningEvent()
        {
            _inGameManager.PlayStory(1001);
        }

        private void HandleDay2LunchEvent()
        {
            _inGameManager.PlayStory(22);   
            _inGameManager.TimeManager.OnLunchTimeEvent -= HandleDay2LunchEvent;
        }

        private void HandleDay2EveningEvent()
        {
            _inGameManager.PlayStory(23); 
            _inGameManager.TimeManager.OnEveningEvent -= HandleDay2EveningEvent;
        }

        private void HandleDay2FinishDayEvent()
        {
            _inGameManager.TimeManager.SetForcedPause(true);// ストーリー用強制ポーズ
            _inGameManager.PlayStory(24);   
            _inGameManager.TimeManager.OnFinishDay -= HandleDay2FinishDayEvent;
        }
    }
}