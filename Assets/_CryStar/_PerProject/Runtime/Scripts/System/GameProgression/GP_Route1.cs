using Cysharp.Threading.Tasks;
using iCON.System;

namespace CryStar.PerProject.GameProgression
{
    public class GP_Route1
    {
        private readonly InGameManager _inGameManager;

        public GP_Route1(InGameManager inGameManager)
        {
            _inGameManager = inGameManager;
        }
        
        public async UniTask Progression()
        {
            await Opening();
        }

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
            _inGameManager.TimeManager.SetTime(9, 20);
            _inGameManager.PlayStory(3);
            _inGameManager.PreloadStoryAsync(new int[2] { 4, 5 }).Forget();
        }

        private void Episode4()
        {
            
        }
    }
}
