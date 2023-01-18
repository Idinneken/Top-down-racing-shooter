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
    internal float startTime; 
    public float decayTime;

    private void Start()
    {
        startTime = Time.fixedTime;
        decayTime += startTime;

        specialIgnoredObjects.Add(sourceObject);
        foreach (Transform child in sourceObject.transform)
        {
            specialIgnoredObjects.Add(child.gameObject);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Time.fixedTime >= decayTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!specialIgnoredObjects.Contains(collider.gameObject) && collider.gameObject.GetComponent<LetsProjectilesPassThrough>() == null)
        {
            this.InstantiatePrefabAndGetComponent(explosionPrefab, out explosionObject, out explosion);
            explosionObject.transform.position = transform.position;

            if (collider.gameObject.GetComponent<AddStatOnHitByBullets>() != null && collider.gameObject.GetComponent<AffectsStat>() != null && sourceObject.GetComponent<Stats>() != null && sourceObject.GetComponent<Stats>().HasStat(collider.gameObject.GetComponent<AffectsStat>().statName))
            {
                sourceObject.GetComponent<Stats>().stats[collider.gameObject.GetComponent<AffectsStat>().statName].ChangeValue(collider.gameObject.GetComponent<AffectsStat>().value);
            }

            if (collider.gameObject.GetComponent<ExplodeOnHitByBullets>() != null)
            {
                collider.gameObject.GetComponent<ExplodeOnHitByBullets>().Explode();
            }

            if (collider.gameObject.GetComponent<AffectStatOnHitByBullets>() != null && collider.gameObject.GetComponent<AffectsStat>() != null && sourceObject.GetComponent<Stats>() != null)
            {
                sourceObject.GetComponent<Stats>().stats[collider.gameObject.GetComponent<AffectsStat>().statName].ChangeValue(collider.gameObject.GetComponent<AffectsStat>().value);
            }

            if (collider.gameObject.GetComponent<SendNotificationOnHitByBullets>() != null && collider.gameObject.GetComponent<NotifiesPlayer>() != null && sourceObject.GetComponent<Stats>() != null)
            {
                collider.gameObject.GetComponent<NotifiesPlayer>().SendNotification(sourceObject.GetComponent<Player>().notification);
            }

            if (collider.gameObject.GetComponent<DestroyedByBullets>() != null)
            {
                Destroy(collider.gameObject);
            }

            Destroy(gameObject);
        }        
    }
}
