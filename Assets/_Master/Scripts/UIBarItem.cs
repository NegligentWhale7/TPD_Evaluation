using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBarItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentValueText;
    [SerializeField] private TextMeshProUGUI maxValueText;
    [SerializeField] private Image uIBar;
    [SerializeField] private float _lerpSpeed = 2;

    public void DisplayBarValue(float currentValue, float maxValue)
    {
        currentValueText.text = currentValue.ToString() + "/";
        maxValueText.text = maxValue.ToString();
        StartCoroutine(LerpBar((float)currentValue / maxValue));
    }

    IEnumerator LerpBar(float targetValue)
    {
        float currentValue = uIBar.fillAmount;
        float newValue = targetValue;
        float elapsedTime = 0;

        while (elapsedTime < _lerpSpeed)
        {
            uIBar.fillAmount = Mathf.Lerp(currentValue, newValue, (elapsedTime / _lerpSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        uIBar.fillAmount = newValue;
    }
}
