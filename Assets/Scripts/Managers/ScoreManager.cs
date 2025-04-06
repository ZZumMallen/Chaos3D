using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Chaos
{
    public class ScoreManager : MonoBehaviour
    {
        private TextMeshProUGUI _myScoreText;

        private int _currentScore;

        private void Awake()
        {
            _currentScore = 0;
            _myScoreText = gameObject.GetComponent<TextMeshProUGUI>();      
        }

        private void OnEnable() => Collectible.OnAnyCollected += AddScore;
        private void OnDisable() => Collectible.OnAnyCollected -= AddScore;
        private void OnDestroy() => Collectible.OnAnyCollected -= AddScore;


        void AddScore(int points)
        {
            _currentScore += points;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            Debug.Log("UpdateScoreText(): Triggered");

            if (_myScoreText == null)
            {
                Debug.Log("_myScoreText is null");
            }
            else
            {
                //_myScoreText.text = "Score: " + _currentScore.ToString();
                _myScoreText.text = $"Score: {_currentScore}";
            }
            
           
        }

    }


}

