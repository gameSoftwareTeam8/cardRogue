using UnityEngine;

public class NodeEffect : MonoBehaviour
{
    public float scaleMultiplier = 1.1f;  
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale = originalScale * scaleMultiplier;  
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;  
    }
}
