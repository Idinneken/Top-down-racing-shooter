using UnityEngine;
using Extensions;

public class Bullet : MonoBehaviour
{
    public LayerMask destroyLayer;
    
    internal GameObject sourceObject;
    internal CharacterController sourceObjectController;

    public int damage;
    
    internal Vector3 movement;
    public float speed;

    void Start()
    {
        transform.position = sourceObject.transform.position;
        transform.forward = sourceObject.transform.forward;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (speed) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {        
        if (collider.gameObject.IsOnLayer_(destroyLayer))
        {
            Destroy(gameObject);
        }
    }

}
