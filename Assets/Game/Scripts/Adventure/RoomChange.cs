using UnityEngine;

public class RoomChange : MonoBehaviour
{
    [SerializeField] SpriteRenderer _background;
    [SerializeField] GameObject[] _rooms;
    [SerializeField] Sprite[] _backgrounds;

    public void RoomMove(int index)
    {
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            _rooms[i].gameObject.SetActive(false);
        }
        _rooms[index].gameObject.SetActive(true);
        _background.sprite = _backgrounds[index];
    }
}
