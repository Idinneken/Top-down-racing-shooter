using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;    
    public float upwardsModifier = 2.0F;

    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb;

            if (hit.TryGetComponent(out rb) && hit.gameObject.GetComponent<AffectedByExplosions>() != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, upwardsModifier);
            }
        }

        Destroy(gameObject);
    }
}

