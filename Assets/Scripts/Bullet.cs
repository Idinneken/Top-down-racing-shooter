using UnityEngine;
using Extensions;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;
    internal GameObject explosionObject;
    internal Explosion explosion;

    public List<GameObject> specialIgnoredObjects = new();
    public GameObject sourceObject;

    public int damage;    
    public float speed;
    public float decayTime;

    private bool initialised = false;

    private void Start()
    {
        specialIgnoredObjects.Add(sourceObject);
        foreach (Transform child in sourceObject.transform)
        {
            specialIgnoredObjects.Add(child.gameObject);
        }

        

        initialised = true;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        //print("objects in ignored objects:");
        //foreach (GameObject gamObject in specialIgnoredObjects)
        //{
        //    print(gamObject);
        //}


        print("hit object: " + collider.gameObject);

        if (specialIgnoredObjects.Contains(collider.gameObject))
        {
            print("hit object is in the specially ignored objects");
        }
        else
        {
            print("hit object is not in the specially ignored objects");
        }

        if (collider.gameObject.GetComponent<LetsProjectilesPassThrough>() == null)
        {
            print("hit object doesn't have the special component which lets through bullets");
        }
        else
        {
            print("hit object has the special component which lets through bullets");
        }

        
        if (!specialIgnoredObjects.Contains(collider.gameObject) && collider.gameObject.GetComponent<LetsProjectilesPassThrough>() == null)
        {
            print("bullet should be exploded");
        }

        if (!specialIgnoredObjects.Contains(collider.gameObject))
        {

        }

        //if (/*initialised && (*/specialIgnoredObjects.Contains(collider.gameObject) == false | collider.gameObject.GetComponent<LetsProjectilesPassThrough>() == null /* || collider.gameObject != sourceObject*//*)*/)
        //{                        
        //    this.InstantiatePrefabAndGetComponent(explosionPrefab, out explosionObject, out explosion);
        //    explosionObject.transform.position = transform.position;


        //    if (collider.gameObject.GetComponent<AffectedByBullets>()?.DestroyOnHit == true)
        //    {
        //        Destroy(collider.gameObject);
        //    }

        //    //print("destroyed");

        //    print("SourceObject: " + sourceObject.name);
        //    print("hit object: " + collider.gameObject.name);

            

        //    //print("If true, the special ignored objects doesn't contain the hit object: " + !specialIgnoredObjects.Contains(collider.gameObject));
        //    //print("If true, the special ignored objects contains, the hit object: " + specialIgnoredObjects.Contains(collider.gameObject));
        //    //print("If true, the hit object has the projectilesPassComponent " + collider.gameObject.GetComponent<LetsProjectilesPassThrough>() != null);
        //    //print("If true, the hit object doesn't have the projectilesPassComponent: " + collider.gameObject.GetComponent<LetsProjectilesPassThrough>() == null);

        //    Destroy(gameObject);
        //}        
    }

}
