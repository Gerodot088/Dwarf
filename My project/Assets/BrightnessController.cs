using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Image darkOverlay;

    [Range(0.5f, 0.95f)]
    [SerializeField] private float maxDarkness = 0.85f;

    private void Start()
    {
        float savedBrightness = PlayerPrefs.GetFloat("GameBrightness", 1f);

        brightnessSlider.value = savedBrightness;
        SetBrightness(savedBrightness);

        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    public void SetBrightness(float value)
    {
        Color color = darkOverlay.color;
        color.a = (1f - value) * maxDarkness;

        darkOverlay.color = color;

        PlayerPrefs.SetFloat("GameBrightness", value);
    }
}
