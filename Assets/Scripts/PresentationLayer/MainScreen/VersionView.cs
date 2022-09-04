using TMPro;
using UnityEngine;

namespace PresentationLayer.MainScreen
{
    public class VersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;

        private void Awake()
        {
            SetVersion();
        }

        private void SetVersion()
        {
            var version = Application.version;
            _textField.SetText(version);
        }
    }
}