using TMPro;
using UnityEngine;

public class CountdownView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;

    private void Awake()
    {
        var countdownTimer = ServiceLocator.Instance.GetService<IGameCountdownTimer>();
        countdownTimer.OnCountdownUpdated += UpdateTime;
    }

    private void UpdateTime(float timeLeft)
    {
        _timeText.SetText(timeLeft.ToString("F"));
    }
}
