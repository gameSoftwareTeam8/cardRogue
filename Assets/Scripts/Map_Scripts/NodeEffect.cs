using UnityEngine;

public class NodeEffect : MonoBehaviour
{
    public float scaleMultiplier = 1.1f;  
    private Vector3 originalScale, colliderScale;
    new private BoxCollider2D collider;

    private void Start()
    {
        originalScale = transform.localScale;
        collider = GetComponent<BoxCollider2D>();
        colliderScale = collider.size;
    }

    private void OnMouseOver()
    {
        transform.localScale = originalScale * scaleMultiplier;
        collider.size = colliderScale / scaleMultiplier;
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale; 
        collider.size = colliderScale; 
    }
}
