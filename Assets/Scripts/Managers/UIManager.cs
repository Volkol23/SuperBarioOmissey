using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _UI_MANAGER;

    private void Awake()
    {
        if(_UI_MANAGER != null && _UI_MANAGER != this)
        {
            Destroy(_UI_MANAGER);
        }
        else
        {
            _UI_MANAGER = this;
            DontDestroyOnLoad(_UI_MANAGER);
        }
    }
}
