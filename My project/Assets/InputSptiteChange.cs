using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputSpriteChanger : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("Картинки для поля ввода")]
    public Sprite normalSprite;
    public Sprite activeSprite; 

    private Image inputImage;

    void Start()
    {
        inputImage = GetComponent<Image>();

        
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
        }
    }

    
    public void OnDeselect(BaseEventData eventData)
    {
        if (inputImage != null && normalSprite != null)
        {
            inputImage.sprite = normalSprite;
        }
    }
}
