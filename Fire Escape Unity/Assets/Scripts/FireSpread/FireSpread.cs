// Author: Jacob Biles
// Contact: Sleepless9115 @ discord

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireSpread : MonoBehaviour
{

    private Tilemap fireSpreadTilemap;
    private GameObject attachedGameObject;
    private Transform attachedTransform;
    private List<Vector3> tilePosList;
    private GameObject[] fireList;
    public GameObject firePrefab;
    public float waitTime;
    public float spreadChance;
    void Start()
    {
        fireList = GameObject.FindGameObjectsWithTag("Fire");

        fireSpreadTilemap = GameObject.FindGameObjectWithTag("meta").GetComponent<Tilemap>();

        attachedGameObject = gameObject;

        attachedTransform = attachedGameObject.transform;

        tilePosList = new List<Vector3>
        {
            new(attachedTransform.position.x - 1, attachedTransform.position.y, 0),
            new(attachedTransform.position.x + 1, attachedTransform.position.y, 0),
            new(attachedTransform.position.x, attachedTransform.position.y - 1, 0),
            new(attachedTransform.position.x, attachedTransform.position.y + 1, 0)
        };

        StartCoroutine(spreadLoop());

    }

    void spawnFiresWhereMissing()
    {

        fireList = GameObject.FindGameObjectsWithTag("Fire");

        for (int i = 0; i < tilePosList.Count; i++)
        {

            Vector3 worldPos = tilePosList[i];
            Vector3Int cellPos = fireSpreadTilemap.WorldToCell(worldPos);
            TileBase tile = fireSpreadTilemap.GetTile(cellPos);

            if (tile == null) continue;
            bool isFireThere = false;
            foreach (GameObject f in fireList)
            {
                Vector3Int firePos = fireSpreadTilemap.WorldToCell(f.transform.position);
                if (firePos == cellPos)
                {
                    isFireThere = true;
                    break;
                }
            }
            if (!isFireThere)
            {
                float rng = Random.Range(1,101);
                if (rng <= spreadChance)
                {
                    Debug.Log("Creating fire");
                    Instantiate(gameObject, worldPos, Quaternion.identity);
                    fireList = GameObject.FindGameObjectsWithTag("Fire");
                }
            }

        }

    }

    IEnumerator spreadLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            spawnFiresWhereMissing();
        }
       
    }


}
