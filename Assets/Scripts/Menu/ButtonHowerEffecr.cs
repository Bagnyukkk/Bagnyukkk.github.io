using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.2f; 
    public float scaleSpeed = 5f;

    private Vector3 originalScale;
    private Vector3 targetScale;



    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.unscaledDeltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        targetScale = originalScale * scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        targetScale = originalScale;
    }


}
