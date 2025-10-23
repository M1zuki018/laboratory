using iCON.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace iCON.UI
{
    /// <summary>
    /// 開始シーンを選択するためのドロップダウンコンポーネント
    /// </summary>
    [RequireComponent(typeof(Dropdown))]
    public class SceneSelectionDropdown : MonoBehaviour
    {
        private int _selectedSceneIndex = KSceneManagement.SYSTEM_SCENE_COUNT;
        private Dropdown _dropdown;
        
        /// <summary>
        /// 現在選択されているシーンのビルドインデックス
        /// </summary>
        public int SelectedSceneIndex => _selectedSceneIndex;

        private void Awake()
        {
            _dropdown = GetComponent<Dropdown>();
            
            // ドロップダウンの値変更アクションを登録
            _dropdown.onValueChanged.RemoveAllListeners();
            _dropdown.onValueChanged.AddListener(ChangeSelectedSceneIndex);
        }

        /// <summary>
        /// 開始するシーンが変更された通知を受け取り選択中のIndexを更新
        /// </summary>
        private void ChangeSelectedSceneIndex(int index)
        {
            // NOTE: シーンの番号と合わせるために開発シーンの個数分Indexを追加する
            _selectedSceneIndex = index + KSceneManagement.SYSTEM_SCENE_COUNT;
        }
    }
}