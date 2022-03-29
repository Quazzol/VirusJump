using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Manager
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _txtScore;
        [SerializeField]
        private Image _imgInfection;
        [SerializeField]
        private Image _imgBlow;

        public void UpdateScore(int score)
        {
            _txtScore.text = score.ToString();
        }

        public void ResetImages()
        {
            _imgInfection.fillAmount = 0;
            _imgBlow.fillAmount = 0;
        }

        public void UpdateInfection(float percent)
        {
            _imgInfection.fillAmount = percent;
        }

        public void UpdateBlow(float percent)
        {
            _imgBlow.fillAmount = percent;
        }
    }
}