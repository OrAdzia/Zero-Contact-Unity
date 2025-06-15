using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f;
    public float scaleSpeed = 10f;
    public float selectedScale = 1.1f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private Button button;
    private bool isHovered = false;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
        button = GetComponent<Button>();
    }

    void Update()
    {
        bool isSelected = EventSystem.current.currentSelectedGameObject == gameObject;

        if (isHovered || isSelected)
            targetScale = originalScale * selectedScale;
        else
            targetScale = originalScale;

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}