using UnityEngine;

public class CollectibleCollider : MonoBehaviour
{
    public GameObject collectibleObject;
    internal Collectible collectible;

    public void Start()
    {
        collectible = collectibleObject.GetComponent<Collectible>();
    }
}
