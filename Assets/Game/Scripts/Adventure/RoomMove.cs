using UnityEngine;

public class RoomMove : MonoBehaviour
{
    [SerializeField] GameObject[] _rooms;
    int _index;

    void Start()
    {
        for(int i = 1; i < _rooms.Length; i++)
        {
            _rooms[i].SetActive(false);
        }
        _rooms[0].SetActive(true);
    }

    public void LeftArrow()
    {
        _rooms[_index].SetActive(false);
        if (_index == 0) _index = _rooms.Length - 1; //0‚¾‚Á‚½‚çÅ‘å’l‚Ö
        else _index--;
        _rooms[_index].SetActive(true);
    }

    public void RightArrow()
    {
        _rooms[_index].SetActive(false);
        if (_index >= _rooms.Length - 1) _index = 0; //Å‘å’l‚¾‚Á‚½‚ç0‚Ö
        else _index++;
        _rooms[_index].SetActive(true);
    }
}
