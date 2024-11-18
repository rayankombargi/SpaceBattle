using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    public float defaultSpeed;
    public float health;

    public bool DamageWait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            health = 0;
        }
    }

    public void TakeDamage(float n)
    {
        if (health - n < 0)
        {
            health = 0;
        }
        else
            health -= n;
        
    }

    public IEnumerator DamageOccure()
    {
        DamageWait = true;
        TakeDamage(health);
        yield return new WaitForSecondsRealtime(1f);
        DamageWait = false;
        StopCoroutine("DamageOccure");
    }

    public void GainHealth(float n)
    {
        if (health + n > 100)
        {
            health = 100;
        }
        else
            health += n;
    }
}
