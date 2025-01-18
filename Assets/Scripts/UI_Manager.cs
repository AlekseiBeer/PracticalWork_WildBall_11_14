using UnityEngine;
using UnityEngine.SceneManagement;
using WildBall.Player;

namespace WildBall.Manager
{
    public class UI_Manager : MonoBehaviour
    {
        private GameObject[] _allScreens;
        private GameObject _startScreen;
        private GameObject _pauseMenuScreen;
        private GameObject _endGame;
        private GameObject _currentScreen = null;
        //private bool _gamePlay = false;
        private bool _isPaused = false;
        private bool _isEndGame = false;

        // Start is called before the first frame update
        void Start()
        {
            TogglePause(false);
            _allScreens = GameObject.FindGameObjectsWithTag("ScreensUI");
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                InitMainMenu();
            }
            else
            {
                InitPauseMenu();
                InitEndGameMenu();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                if (Input.GetKeyDown(KeyCode.Escape) && !_isEndGame)
                    TogglePause(!_isPaused);
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

        void InitEndGameMenu()
        {
            _isEndGame = false;
            _endGame = GameObject.Find("EndScreen");
            _endGame.SetActive(false);
            _currentScreen = null;
        }

        void TogglePause(bool pause)
        {
            //_gameMenuAudio.Play();
            StateMachine(pause ? _pauseMenuScreen : null);
            Time.timeScale = pause ? 0f : 1f;
            _isPaused = pause;
        }

        public void EndGame()
        {
            _isPaused = true;
            Time.timeScale = _isPaused ? 0f : 1f;
            StateMachine(_endGame);
            _isEndGame = true;

            int star = FindObjectOfType<PlayerController>()._coinCount;

            for (int i = 0; i < star; i++)
            {
                GameObject.Find($"Star_off_{i+1}").transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        public void PlayContinue()
        {
            StateMachine(null);
            TogglePause(false);
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
}
