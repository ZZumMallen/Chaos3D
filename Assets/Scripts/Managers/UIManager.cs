using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Chaos.UI
{
    public class UIManager : MonoBehaviour
    {

        [Header("In-game UI elements")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        

        private int _scoreNumber;
       

        [Header("Conditional Windows")]
        [SerializeField] private GameObject _winUI;
        [SerializeField] private GameObject _pauseUI;
        [SerializeField] private GameObject _loseUI;

        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;

            if(_scoreText == null)
            {
                Debug.LogError("Score Text reference not set in UIManager");
                _scoreText = GameObject.Find("InGameScore")?.GetComponent<TextMeshProUGUI>();
            }
        }

        private void Start()
        {
            _winUI.SetActive(false);
            _pauseUI.SetActive(false);
            _loseUI.SetActive(false);
            _scoreNumber = 0;
           
            UpdateScoreDisplay();
        }

        public void OnResetPress()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            _winUI.SetActive(false);
            _pauseUI.SetActive(false);
            _loseUI.SetActive(false);
            _scoreNumber = 0;
          
            UpdateScoreDisplay();
        }

        public void OnResumeGamePress()
        {
            _pauseUI.SetActive(false);
        }

        public void OnExitGamePress()
        {
           Application.Quit(); // triggers game exit after compiled
        }

        public void OnEnterPausePress()
        {
            _pauseUI.SetActive(true);
        }

        public void OnWinConditionsMet()
        {
            _winUI.SetActive(true);
        }

        public void OnLoseConditionsMet()
        {
            _loseUI.SetActive(true);
        }

        public void OnEnemyDeath(int points)
        {
            _scoreNumber += points;
            UpdateScoreDisplay();            

        }

        private void UpdateScoreDisplay()
        {
            if (_scoreText != null)
            {
                _scoreText.text = "Score: " + _scoreNumber.ToString();
            }

        }



    }
}

