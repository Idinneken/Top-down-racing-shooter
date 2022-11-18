using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCollectibles : MonoBehaviour
{
    public GameObject statsObject;
    internal Stats stats;

    void Start()
    {
        stats = statsObject.GetComponent<Stats>();
    }

    public void OnTriggerEnter(Collider other)
    {        
        CollectibleCollider collectibleCollider = other.gameObject.GetComponent<CollectibleCollider>();

        if (collectibleCollider != null)
        {
            Collectible collectible = collectibleCollider.collectible;

            if (collectible != null)
            {
                if (stats.HasStat(collectible.statName) && collectible.affectsStats)
                {
                    stats.stats[collectible.statName].ChangeValue(collectible.value);
                }
            }
            else if (stats == null && collectible.collectEvenIfNoMatchingStat)
            {
                Destroy(collectible.gameObject);
                return;
            }
            else if (stats == null && !collectible.collectEvenIfNoMatchingStat)
            {
                return;
            }

            print(collectible.statName + " " + stats.stats[collectible.statName].value);

            Destroy(collectible.gameObject);
        }
    }

}
