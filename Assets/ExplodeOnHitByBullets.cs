using Extensions;
using UnityEngine;

public class ExplodeOnHitByBullets : MonoBehaviour
{
    public GameObject explosionPrefab;
    internal GameObject explosionObject;
    internal Explosion explosion;

    public void Explode()
    {
        this.InstantiatePrefabAndGetComponent(explosionPrefab, out explosionObject, out explosion);
        explosionObject.transform.position = transform.position;
    }
}
