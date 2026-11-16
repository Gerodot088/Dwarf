using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonZoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки спрайтов")]
    public Sprite normalSprite;
    public Sprite hoverSprite;

    [Header("Визуальный объект кнопки (если скрипт на хитбоксе)")]
    public RectTransform targetObject;

    private Image buttonImage;
    private Vector3 defaultScale;
    private Vector3 targetScale;

    void Start()
    {
        if (targetObject == null)
            targetObject = GetComponent<RectTransform>();

        buttonImage = targetObject.GetComponent<Image>();

        if (normalSprite == null && buttonImage != null)
        {
            normalSprite = buttonImage.sprite;
        }
        else if (buttonImage != null)
        {
            buttonImage.sprite = normalSprite;
        }

        defaultScale = targetObject.localScale;
        targetScale = defaultScale;
    }

    void Update()
    {
        targetObject.localScale = Vector3.Lerp(targetObject.localScale, targetScale, Time.deltaTime * 15f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = defaultScale * 1.15f;

        if (buttonImage != null && hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = defaultScale;

        if (buttonImage != null && normalSprite != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }
}
