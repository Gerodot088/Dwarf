using UnityEngine;
using UnityEngine.UI; // Нужно для работы с Image
using UnityEngine.EventSystems;

public class ButtonZoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки спрайтов")]
    public Sprite normalSprite;
    public Sprite hoverSprite;  

    private Image buttonImage;
    private Vector3 defaultScale;
    private Vector3 targetScale;

    void Start()
    {
       
        buttonImage = GetComponent<Image>();

      
        if (normalSprite == null && buttonImage != null)
        {
            normalSprite = buttonImage.sprite;
        }
        else if (buttonImage != null)
        {
            buttonImage.sprite = normalSprite;
        }

        defaultScale = transform.localScale;
        targetScale = defaultScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 15f);
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
