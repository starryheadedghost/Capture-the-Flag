using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header ("Flag Stats")]
    public bool hasFlag;
    public bool flagPlaced;

    public int scoreToWin;
    public int curScore;

    public bool gamePaused;

    private static GameManager instance;

    void Awake()
    {
       instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hasFlag = false;
        flagPlaced = false;

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(flagPlaced)
        {
            WinGame();
        }
    }
     void ToggleGamePause()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused == true ? 0.0f : 1.0f;

        Cursor.lockState = gamePaused == true ? CursorLockMode.None : CursorLockMode.Locked;
    }
    void WinGame()
    {
        Debug.Log("you won");

    }
    
     void PlaceFlag()
    {
        flagPlaced = true;
    }
    }
    
