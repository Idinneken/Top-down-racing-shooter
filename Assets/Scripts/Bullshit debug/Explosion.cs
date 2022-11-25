using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public string affectsThisCompont;

    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb;

            if (hit.TryGetComponent(out rb) && hit.gameObject.GetComponent(affectsThisCompont) != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }
        }

        Destroy(gameObject);
    }
}
