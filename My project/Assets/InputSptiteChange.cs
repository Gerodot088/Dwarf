using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputSpriteChanger : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("Картинки для поля ввода")]
    public Sprite normalSprite;
    public Sprite activeSprite;

    [Header("Сдвиг активной картинки по вертикали (в пикселях)")]
    public float activeOffsetY = -5f;

    [Header("Контейнер текста внутри InputField (Text Area)")]
    public RectTransform textArea;

    private Image inputImage;
    private RectTransform rectTransform;
    private Vector2 defaultPos;
    private Vector2 defaultTextPos;

    void Start()
    {
        inputImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        defaultPos = rectTransform.anchoredPosition;

        if (textArea == null && transform.childCount > 0)
            textArea = transform.GetChild(0).GetComponent<RectTransform>();

        if (textArea != null)
            defaultTextPos = textArea.anchoredPosition;

        if (normalSprite == null && inputImage != null)
        {
            normalSprite = inputImage.sprite;
        }
        else if (inputImage != null)
        {
            inputImage.sprite = normalSprite;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (inputImage != null && activeSprite != null)
        {
            inputImage.sprite = activeSprite;
            rectTransform.anchoredPosition = defaultPos + new Vector2(0, activeOffsetY);

            if (textArea != null)
                textArea.anchoredPosition = defaultTextPos - new Vector2(0, activeOffsetY);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (inputImage != null && normalSprite != null)
        {
            inputImage.sprite = normalSprite;
            rectTransform.anchoredPosition = defaultPos;

            if (textArea != null)
                textArea.anchoredPosition = defaultTextPos;
        }
    }
}
