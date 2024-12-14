using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Image _fadePanel = null;
    [SerializeField] float _fadeSpeed = 1f;
    bool _isLoadStarted = false;
    bool _fadeIn = true;
    string _sceneName;

    void Start()
    {
        _fadePanel.gameObject.gameObject.SetActive(true);
        _fadeIn = true;
    }

    void Update()
    {
        if (_fadeIn)
        {
            FadeIn();
        }

        if (_isLoadStarted)// ロードが開始されたら
        {
            FadeOut();
        }
    }

    /// <summary>
    /// 名前を指定してシーンをロードする
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        _fadePanel.gameObject.gameObject.SetActive(true);
        _isLoadStarted = true;
        _sceneName = sceneName;
    }

    void FadeIn()
    {
        Color panelColor = _fadePanel.color;
        panelColor.a -= _fadeSpeed * Time.deltaTime;
        _fadePanel.color = panelColor;

        if (panelColor.a < 0.01f)
        {
            _fadePanel.gameObject.SetActive(false);
            _fadeIn = false;
        }
    }

    void FadeOut()
    {
        if (_fadePanel)
        {
            Color panelColor = _fadePanel.color;
            panelColor.a += _fadeSpeed * Time.deltaTime;
            _fadePanel.color = panelColor;

            if (panelColor.a > 0.99f)
            {
                SceneManager.LoadScene(_sceneName);
                _isLoadStarted = false;
            }
        }
    }
}
