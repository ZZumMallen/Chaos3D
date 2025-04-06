using UnityEngine;

namespace Chaos.UI
{
    public class WinConditionManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _uiManager.OnWinConditionsMet();
            }
        }

    }
}

