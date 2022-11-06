using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _GAME_MANAGER;

    [SerializeField]
    private int gamePoints = 0;
    private void Awake()
    {
        if (_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(_GAME_MANAGER);
        }
        else
        {
            _GAME_MANAGER = this;
            DontDestroyOnLoad(_GAME_MANAGER);
        }
    }

    public void UpdatePoints()
    {
        gamePoints++;
    }
}
