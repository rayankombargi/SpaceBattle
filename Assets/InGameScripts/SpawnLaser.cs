using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaser : MonoBehaviour
{
    public GameObject Pickup;
    public float WaitTime;
    [SerializeField] public int count;
    public int MaxCount;
    [SerializeField] public float px;
    [SerializeField] public float py;
    public ArrayList PickupList;

    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < MaxCount/2; i++)
        // {
        //     Spawn(Pickup);
        // }
        PickupList = new ArrayList();
        StartCoroutine("InitiateSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        //CleanUpList();
    }
    public void Spawn(GameObject obj)
    {
        StartCoroutine("NewCoords");

        GameObject newPickup = Instantiate(obj, new Vector2(px,py), Quaternion.Euler(0,0,0));
        
        IncreaseCount(newPickup);
    }
    IEnumerator InitiateSpawn()
    {
        while (true)
        {
            if (count == MaxCount)
            {
                GameObject currentPickup = GameObject.Find("PUL");
                if (currentPickup != null)
                {
                    DecreaseCount(currentPickup);
                    Destroy(currentPickup);
                }
            }

            Spawn(Pickup);


            yield return new WaitForSeconds(WaitTime);


        }

    }

    public void IncreaseCount(GameObject obj)
    {
        count++;
        PickupList.Add(obj);
        //Debug.Log(count);
    }
    public void DecreaseCount(GameObject obj)
    {
        if (count > 0)
        {
            count--;
            PickupList.Remove(obj);

        }
        //Debug.Log(count);
    }
    public bool ExistingPickup(float x, float y)
    {
        foreach (GameObject p in PickupList)
        {
            if (p.transform.position.x == x && p.transform.position.y == y)
                return true;
        }
        return false;
    }
    IEnumerator NewCoords()
    {
        do
        {
            px = 20 * (int)(Random.Range(-25f,25f));
            py = 20 * (int)Random.Range(-7.5f, 7.5f);
            yield return null;
        } while (!(ExistingPickup(px,py)));

        StopCoroutine("NewCoords");
    }

    public void CleanUpList()
    {
        for (int i = 0; i < PickupList.Count; i++)
        {
            if (PickupList[i] == null)
                PickupList.RemoveAt(i);
        }
    }
}
