using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Biome : MonoBehaviour
{
    public GameObject biomeData;
    internal TreePrototype[] trees;
    
    public List<GameObject> terrainObjects;
    public bool set = false;

    private void OnGUI()
    {        
        if (!set)
        { 
            trees = biomeData.GetComponent<Terrain>().terrainData.treePrototypes;

            foreach(GameObject terrainObject in terrainObjects)
            {
                print("yuh");

                EditorUtility.SetDirty(terrainObject);
                terrainObject.GetComponent<Terrain>().terrainData.treePrototypes = trees;
                EditorUtility.ClearDirty(terrainObject);

            }
            
            EditorUtility.SetDirty(gameObject);
            set = true;
            EditorUtility.ClearDirty(gameObject);
        }
    }
}
