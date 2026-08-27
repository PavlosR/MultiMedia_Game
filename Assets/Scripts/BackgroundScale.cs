using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScaleBackgroundToCamera : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ScaleBackground();
    }

    void Update()
    {
        // Optional: Call this in Update if your orthographic size changes dynamically during gameplay
        ScaleBackground();
    }

    void ScaleBackground()
    {
        if (spriteRenderer.sprite == null) return;

        Camera cam = Camera.main;
        if (!cam.orthographic) return;

        // Get camera size bounds in world units
        float worldScreenHeight = cam.orthographicSize * 2.2f;
        float worldScreenWidth = worldScreenHeight / cam.aspect;

        // Get sprite size in world units (unscaled)
        Vector2 spriteSize = spriteRenderer.sprite.rect.size / spriteRenderer.sprite.pixelsPerUnit;

        // Calculate scale factors
        float scaleX = worldScreenWidth / spriteSize.x;
        float scaleY = worldScreenHeight / spriteSize.y;

        // Use Mathf.Max if you want to "cover" the screen (no blank space, might crop edges)
        // Use Mathf.Min if you want to "fit" the screen (entire image visible, might show borders)
        float finalScale = Mathf.Max(scaleX, scaleY);

        transform.localScale = new Vector3(finalScale, finalScale, 1f);
    }
}