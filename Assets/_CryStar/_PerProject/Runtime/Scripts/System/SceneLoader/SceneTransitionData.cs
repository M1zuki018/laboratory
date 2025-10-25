using System;
using System.Collections.Generic;
using CryStar.Core.Enums;
using iCON.System;

namespace CryStar.Data.Scene
{
    /// <summary>
    /// シーン遷移のパラメータ
    /// </summary>
    [Serializable]
    public class SceneTransitionData
    {
        /// <summary>
        /// 遷移するシーン
        /// </summary>
        public SceneType TargetScene;
        
        /// <summary>
        /// ロードシーンを読み込むか
        /// </summary>
        public bool UseLoadingScreen;
        
        /// <summary>
        /// アセットのプリロードを行うか
        /// </summary>
        public bool PreloadAssets;
        
        /// <summary>
        /// 次のシーンに受け渡したいパラメーター
        /// </summary>
        public Dictionary<string, object> SceneParameters;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneTransitionData(SceneType scene, bool useLoadingScreen = false, bool preloadAssets = false, Dictionary<string, object> sceneParameters = null)
        {
            TargetScene = scene;
            UseLoadingScreen = useLoadingScreen;
            PreloadAssets = preloadAssets;
            SceneParameters = sceneParameters ?? new Dictionary<string, object>();
        }
    }
}
