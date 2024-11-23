using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private GameObject[] _allScreens;
    private GameObject _startScreen;
    private GameObject _pauseMenuScreen;
    private GameObject _currentScreen = null;
    //private bool _gamePlay = false;
    private bool _isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        _allScreens = GameObject.FindGameObjectsWithTag("ScreensUI");
        if (SceneManager.GetActiveScene().buildIndex == 0)
            InitMainMenu();
        else
            InitPauseMenu();  
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause(!_isPaused);
            /*if (_gamePlay)
            {
                //CheckGameOverConditions();

                if (Input.GetKeyDown(KeyCode.Escape))
                    TogglePause(!_isPaused);
            }*/
        }
    }

    void InitMainMenu()
    {
        _startScreen = GameObject.Find("MainMenu");
        foreach (GameObject screen in _allScreens)
            screen.SetActive(false);
        _startScreen.SetActive(true);
        _currentScreen = _startScreen;
    }

    void InitPauseMenu()
    {
        _pauseMenuScreen = GameObject.Find("MenuScreen");
        _pauseMenuScreen.SetActive(false);
        _currentScreen = null;
    }

    void TogglePause(bool pause)
    {
        //_gameMenuAudio.Play();
        StateMachine(pause ? _pauseMenuScreen : null);
        Time.timeScale = pause ? 0f : 1f;
        _isPaused = pause;
    }

    public void PlayContinue()
    {
        StateMachine(null);
        _isPaused = false;
    }

    public void StateMachine(GameObject targetScreen)
    {
        _currentScreen?.SetActive(false);
        if (targetScreen != null)
        {
            targetScreen.SetActive(true);
            _currentScreen = targetScreen;
        }
        else
        {
            _currentScreen = null;
        }
    }
}
