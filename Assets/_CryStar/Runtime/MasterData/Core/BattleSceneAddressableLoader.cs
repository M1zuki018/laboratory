using CryStar.MasterData;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// アプリケーション起動時の初期化処理
/// </summary>
public static class BattleSceneAddressableLoader
{ 
    private static bool _isInitialized = false;
    private static UniTaskCompletionSource _initializationTask;

    /// <summary>
    /// マスターデータが初期化完了しているか
    /// </summary>
    public static bool IsInitialized => _isInitialized;

    /// <summary>
    /// マスターデータ初期化の完了を待機
    /// </summary>
    public static UniTask WaitForInitializationAsync()
    {
        if (_isInitialized)
        {
            return UniTask.CompletedTask;
        }

        _initializationTask ??= new UniTaskCompletionSource();
        return _initializationTask.Task;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        InitializeAsync().Forget();
    }

    private static async UniTaskVoid InitializeAsync()
    {
        if (_isInitialized)
        {
            return;
        }

        Debug.Log("[MasterDataBootstrap] マスターデータ読み込み開始...");

        try
        {
            // 必要なマスターデータを全て読み込む
            await MasterDataManager.Instance.GetAsync<MasterGrowthCharacter>();
            await MasterDataManager.Instance.GetAsync<MasterBattleCharacter>();

            _isInitialized = true;
            _initializationTask?.TrySetResult();

            Debug.Log("[MasterDataBootstrap] マスターデータ読み込み完了");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[MasterDataBootstrap] マスターデータ読み込み失敗: {e}");
            _initializationTask?.TrySetException(e);
        }
    }
}