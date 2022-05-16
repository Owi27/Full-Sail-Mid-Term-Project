using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int keyCards = 0;
    public Text KeyCardCountText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (KeyCardCountText == null)
        {
            GameObject gameObject = GameObject.Find("KeyCardCount");
            if (gameObject != null) KeyCardCountText = GameObject.Find("KeyCardCount").GetComponent<Text>();

        }
     
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        if (HUDParent == null)
        {
            HUDParent = GameObject.Find("HUD");
        }
    }

    #region //Player
    public GameObject Player { get; private set; }
    public void SetPlayer(GameObject _player)
    {
        Player = _player;
    }
    #endregion

    #region //HUD Parent object
    GameObject hudParent;
    public GameObject HUDParent
    {
        get
        {
            return hudParent;
        }
        private set
        {
            hudParent = value;
        }
    }

    public void SetHUDParent(GameObject _hudparent)
    {
        HUDParent = _hudparent;
    }
    #endregion

    #region //MainMenu

    GameObject mainMenu;
    public GameObject MainMenu
    {
        get
        {
            return mainMenu;
        }
        private set
        {
            mainMenu = value;
        }
    }

    public void SetMainMenu(GameObject _mainMenu)
    {
        MainMenu = _mainMenu;
    }
    #endregion

    #region Fader Object

    GameObject fader;
    public GameObject Fader
    {
        get
        {
            return fader;
        }
        private set
        {
            fader = value;
        }
    }

    public void SetFader(GameObject _fader)
    {
        Fader = _fader;
    }

    #endregion

    #region Paused
    public bool isPaused = false;
    bool IsPaused
    {
        get
        {
            return isPaused;
        }
        set
        {
            isPaused = value;
        }
    }
    #endregion
}
