using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrdgeManager : MonoBehaviour
{
    public GameObject[] Bridgeprefab;
    private Transform playertransform;
    private float spawnZ = 0.0f;
    private float bridgeLength = 10.0f;
    private int amntofBridge = 7;
    private float safeZone = 15.0f;
    private List<GameObject> ActiveBridges;
    private int lastPrefabIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        ActiveBridges = new List<GameObject>();
        playertransform = GameObject.FindGameObjectWithTag("Player").transform;

         for(int i = 0; i <amntofBridge; i++)
        {
            if(i < 3)
            {
                spawnBridge(0);
            }
            else
            {
                spawnBridge();
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playertransform.position.z - safeZone > (spawnZ - amntofBridge * bridgeLength))
        {
            spawnBridge();
            deleteBridge();
        }
    }

    void spawnBridge(int prefabIndex = -1)
    {

        GameObject go;

        if(prefabIndex == -1)
        {
            go = Instantiate(Bridgeprefab[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(Bridgeprefab[prefabIndex]) as GameObject;
        }
        
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += bridgeLength;
        ActiveBridges.Add(go);
    }

   void deleteBridge()
    {
        Destroy(ActiveBridges[0]);
        ActiveBridges.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if(Bridgeprefab.Length <= 1)
        {
           return 0;
        }

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, Bridgeprefab.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

}
