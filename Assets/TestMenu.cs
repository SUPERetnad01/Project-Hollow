using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.LoadGame();
    }
}
