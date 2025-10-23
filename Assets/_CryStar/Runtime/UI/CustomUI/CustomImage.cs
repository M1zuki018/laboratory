using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

/// <summary>
/// CustomImage
/// </summary>
[AddComponentMenu("Custom UI/Custom Image")]
public class CustomImage : Image
{
    [SerializeField] private string _assetName;
    private AsyncOperationHandle<Sprite> _loadHandle;
    private bool _isLoading = false;

    public bool HasAsset => sprite != null; 
    
    protected override void Awake()
    {
        base.Awake();
        
        if (_assetName != string.Empty)
        {
            LoadSpriteAsync().Forget();
        }
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void Show()
    {
        color = new Color(color.r, color.g, color.b, 1);
    }
    
    /// <summary>
    /// 非表示
    /// </summary>
    public void Hide()
    {
        color = new Color(color.r, color.g, color.b, 0);
    }

    /// <summary>
    /// スプライトを非同期で読み込む
    /// </summary>
    public async UniTask ChangeSpriteAsync(string filePath)
    {
        _assetName = filePath;
        await LoadSpriteAsync();
    }
    
    /// <summary>
    /// スプライトを非同期で読み込む
    /// </summary>
    private async UniTask LoadSpriteAsync()
    {
        if (_isLoading || string.IsNullOrEmpty(_assetName))
            return;

        _isLoading = true;

        try
        {
            // 既存のハンドルがあれば解放
            if (_loadHandle.IsValid())
            {
                Addressables.Release(_loadHandle);
            }

            // 新しいアセットを読み込み
            _loadHandle = Addressables.LoadAssetAsync<Sprite>(_assetName);
            var loadedSprite = await _loadHandle.ToUniTask();
            
            if (this != null)
            {
                sprite = loadedSprite;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load sprite: {_assetName}, Error: {e.Message}");
        }
        finally
        {
            _isLoading = false;
        }
    }
    
    /// <summary>
    /// オブジェクト破棄時にAddressableハンドルを解放
    /// </summary>
    protected override void OnDestroy()
    {
        if (_loadHandle.IsValid())
        {
            Addressables.Release(_loadHandle);
        }
        base.OnDestroy();
    }
}
