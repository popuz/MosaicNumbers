using TMPro;
using UnityEngine;

namespace MosaicNumbers
{
    public class ResultsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _triesTMP;
        [SerializeField] private TMP_Text _timeTMP;

        private const string _triesText = "Tries: ";
        private const string _timeText = "Time: ";


        private void Awake() => Hide();

        public void Hide()
        {
            if (_panel != null) _panel.SetActive(false);
        }

        public void Show(int tries, float time)
        {
            if (_panel != null) _panel.SetActive(true);

            _triesTMP.text = $"{_triesText}{tries}";
            _timeTMP.text = $"{_timeText}{time:F2}s";
        }
    }
}