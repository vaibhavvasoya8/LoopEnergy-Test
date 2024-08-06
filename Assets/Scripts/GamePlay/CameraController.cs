using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Ensure the camera does not go below a minimum size")]
    [SerializeField] float minOrthographicSize = 5;
    [Tooltip("Set offset for y position")]
    [SerializeField] float offset = 0.5f;
    [Tooltip("Adjust this factor as needed the camera zoom multiplier")]
    [SerializeField] float sizeFactor = 1.5f;
    
    Camera camera;

    float focusSize = 5;
    Vector3 focusPosition;
    
    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    /// <summary>
    /// Set the Camera Position and size as per the game play.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void SetCameraPositionAndSize(int width, int height)
    {
        // Set the camera's position to the center position, preserving the original z position
        focusPosition = new Vector3((width-1) / 2f , (height / 2f)+0.2f, transform.position.z);
        transform.position = focusPosition;
        
        //camera.orthographicSize = width / 2 * sizeFactor;
        focusSize = (width+2) / 2f * sizeFactor;
        //camera.orthographicSize = Mathf.Max(camera.orthographicSize, minOrthographicSize);
    }

    /// <summary>
    /// Camera Zoom-Out on GamePlay
    /// </summary>
    public void UnfocusCamera()
    {
        if(focusSize < minOrthographicSize)
            camera.DOOrthoSize(minOrthographicSize + 1f, 0.4f);
        else
            camera.DOOrthoSize(Mathf.Max((focusSize + 1f),minOrthographicSize),0.4f);
        transform.DOMove(new Vector3(focusPosition.x,focusPosition.y - offset,focusPosition.z),0.5f);
    }

    /// <summary>
    /// Camera Zoom-In on GamePlay
    /// </summary>
    public void FocusCamera()
    {
        camera.DOOrthoSize(Mathf.Max(focusSize,minOrthographicSize) , 0.4f);
        transform.DOMove(focusPosition, 0.5f);
    }
}
