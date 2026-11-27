using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("Слайдеры")]
    [SerializeField] private UnityEngine.UI.Slider yarkostSlider;
    [SerializeField] private UnityEngine.UI.Slider fonMusicSlider;
    [SerializeField] private UnityEngine.UI.Slider gameMusicSlider;

    [Header("Объекты, которыми управляют слайдеры")]
    [SerializeField] private Image brightnessOverlay;
    [SerializeField] private AudioSource fonMusicSource;  // Источник фоновой музыки (если уже есть)
    [SerializeField] private AudioSource gameMusicSource; // Источник игровой музыки (если уже есть)

    private void Start()
    {
        if (yarkostSlider != null) yarkostSlider.onValueChanged.AddListener(OnYarkostChanged);
        if (fonMusicSlider != null) fonMusicSlider.onValueChanged.AddListener(OnFonMusicChanged);
        if (gameMusicSlider != null) gameMusicSlider.onValueChanged.AddListener(OnGameMusicChanged);

        ApplyAllSettings();
    }

    private void OnEnable()
    {
        ApplyAllSettings();
    }

    // Применяет все сохраненные настройки при входе в аккаунт или открытии окна настроек
    public void ApplyAllSettings()
    {
        if (FirebaseSaveManager.Instance == null) return;
        UserData data = FirebaseSaveManager.Instance.currentData;

        if (yarkostSlider != null) yarkostSlider.value = data.brightness;
        if (fonMusicSlider != null) fonMusicSlider.value = data.fonMusicVolume;
        if (gameMusicSlider != null) gameMusicSlider.value = data.gameMusicVolume;

        UpdateBrightness(data.brightness);
        if (fonMusicSource != null) fonMusicSource.volume = data.fonMusicVolume;
        if (gameMusicSource != null) gameMusicSource.volume = data.gameMusicVolume;
    }

    private void OnYarkostChanged(float value)
    {
        UpdateBrightness(value);
        if (FirebaseSaveManager.Instance != null)
            FirebaseSaveManager.Instance.currentData.brightness = value;
    }

    private void OnFonMusicChanged(float value)
    {
        if (fonMusicSource != null) fonMusicSource.volume = value;
        if (FirebaseSaveManager.Instance != null)
            FirebaseSaveManager.Instance.currentData.fonMusicVolume = value;
    }

    private void OnGameMusicChanged(float value)
    {
        if (gameMusicSource != null) gameMusicSource.volume = value;
        if (FirebaseSaveManager.Instance != null)
            FirebaseSaveManager.Instance.currentData.gameMusicVolume = value;
    }

    private void UpdateBrightness(float value)
    {
        if (brightnessOverlay == null) return;
        Color color = brightnessOverlay.color;
        color.a = 1f - value;
        brightnessOverlay.color = color;
    }
}
