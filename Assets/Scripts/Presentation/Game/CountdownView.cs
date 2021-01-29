using TMPro;
using UnityEngine;

public class CountdownView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameCountdownTimer _gameCountdownTimer;

    private void Awake()
    {
        _gameCountdownTimer.OnCountdownUpdated += UpdateTime;
    }

    private void UpdateTime(float timeLeft)
    {
        _timeText.SetText(timeLeft.ToString("F"));
    }
}
