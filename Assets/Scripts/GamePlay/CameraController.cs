using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Ensure the camera does not go below a minimum size
    [SerializeField] float minOrthographicSize = 5;
    Camera camera;

    float focusSize = 5;
    Vector3 focusPosition;
    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    public void SetCameraPositionAndSize(int width, int height)
    {
        // Set the camera's position to the center position, preserving the original z position
        focusPosition = new Vector3(width / 2 , (height / 2)+0.5f, transform.position.z);

        // This assumes the camera is orthographic
        // Adjust the factor to fit your specific needs
        float sizeFactor = 1.5f; // Adjust this factor as needed
        //camera.orthographicSize = width / 2 * sizeFactor;
        focusSize = width / 2 * sizeFactor;
        //camera.orthographicSize = Mathf.Max(camera.orthographicSize, minOrthographicSize);
    }
    [SerializeField] float offset = 0.5f;
    public void UnfocusCamera()
    {
        if(focusSize < minOrthographicSize)
            camera.DOOrthoSize(minOrthographicSize + 1f, 0.4f);
        else
            camera.DOOrthoSize(Mathf.Max((focusSize + 1f),minOrthographicSize),0.4f);
        transform.DOMove(new Vector3(focusPosition.x,focusPosition.y - offset,focusPosition.z),0.5f);
    }
    public void FocusCamera()
    {
        camera.DOOrthoSize(Mathf.Max(focusSize,minOrthographicSize) , 0.4f);
        transform.DOMove(focusPosition, 0.5f);
    }
}
