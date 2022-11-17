using UnityEngine;
using Extensions;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;
    internal GameObject explosionObject;
    internal Explosion explosion;
        
    internal GameObject sourceObject;
    internal CharacterController sourceObjectController;

    public string movesThroughComponent;

    public int damage;    
    public float speed;
    public float decayTime;

    private bool destroy;

    void Start()
    {
        transform.position = sourceObject.transform.position;
        transform.forward = sourceObject.transform.forward;        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {        
        if (GetComponent(movesThroughComponent) == null && collider.gameObject != sourceObject)
        {            
            this.InstantiatePrefabAndGetComponent(explosionPrefab, out explosionObject, out explosion);
            explosionObject.transform.position = transform.position;
            Destroy(gameObject);
        }        
    }

}
