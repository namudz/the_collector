using TMPro;
using UnityEngine;

public class CountdownView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    
    private IGameCountdownTimer _countdownTimer;

    private void Awake()
    {
        _countdownTimer = ServiceLocator.Instance.GetService<IGameCountdownTimer>();
        _countdownTimer.OnCountdownUpdated += UpdateTime;
    }

    private void OnDestroy()
    {
        _countdownTimer.OnCountdownUpdated -= UpdateTime;
    }

    private void UpdateTime(float timeLeft)
    {
        _timeText.SetText(timeLeft.ToString("F"));
    }
}
