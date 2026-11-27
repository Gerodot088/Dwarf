using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider brightnessSlider;
    [SerializeField] private Image brightnessOverlay;

    private void Start()
    {
        if (brightnessSlider != null)
        {
            brightnessSlider.onValueChanged.AddListener(OnSliderChanged);
        }
        ApplySavedBrightness();
    }

    private void OnEnable()
    {
        ApplySavedBrightness();
    }

    public void ApplySavedBrightness()
    {
        if (FirebaseSaveManager.Instance == null || brightnessSlider == null) return;

        float savedValue = FirebaseSaveManager.Instance.currentData.brightness;
        brightnessSlider.value = savedValue;
        UpdateOverlayAlpha(savedValue);
    }

    private void OnSliderChanged(float value)
    {
        UpdateOverlayAlpha(value);
        if (FirebaseSaveManager.Instance != null)
        {
            FirebaseSaveManager.Instance.currentData.brightness = value;
        }
    }

    private void UpdateOverlayAlpha(float value)
    {
        if (brightnessOverlay == null) return;

        Color color = brightnessOverlay.color;
        color.a = 1f - value;
        brightnessOverlay.color = color;
    }
}
