using UnityEngine;

public class SimpleObjectCuller : MonoBehaviour
{
    // The main camera, which is following the player.
    private Transform mainCameraTransform;

    // The distance at which objects will become active.
    // Set this to a value slightly larger than your camera's view.
    public float activationDistance = 17.71f;

    // A reference to the object's components to enable/disable.
    private Renderer objRenderer;
    private Collider2D objCollider;

    void Start()
    {
        // Find the main camera in the scene.
        mainCameraTransform = Camera.main.transform;

        // Get references to the renderer and collider.
        objRenderer = GetComponent<Renderer>();
        objCollider = GetComponent<Collider2D>();

        // If we can't find the necessary components, disable the script.
        if (mainCameraTransform == null || objRenderer == null)
        {
            enabled = false;
        }

        // Initially check the distance to set the correct state.
        CheckDistance();
    }

    void Update()
    {
        // This check runs every frame. We can make this less frequent if needed.
        // For now, this is a good starting point.
        CheckDistance();
    }

    void CheckDistance()
    {
        // Calculate the distance between this object and the camera.
        float distance = Vector2.Distance(mainCameraTransform.position, transform.position);

        // Check if the object is within the activation distance.
        if (distance < activationDistance)
        {
            // If it's close enough, make sure it's active.
            if (!objRenderer.enabled)
            {
                objRenderer.enabled = true;
                if (objCollider != null)
                {
                    objCollider.enabled = true;
                }
            }
        }
        else
        {
            // If it's too far away, deactivate it.
            if (objRenderer.enabled)
            {
                objRenderer.enabled = false;
                if (objCollider != null)
                {
                    objCollider.enabled = false;
                }
            }
        }
    }
}