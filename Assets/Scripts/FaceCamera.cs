using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    new private Camera camera;
    
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(camera.transform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
