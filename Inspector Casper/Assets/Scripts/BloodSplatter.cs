using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BloodSplatter : MonoBehaviour
{
    public GameObject bloodDrop;
    public bool splash = false;
    public int amount;
    public float  destroyAfter = 3.0f;
    public bool dripper = false;
    public float dripPerSec = 1.0f;
    

    private float _timer = 0f;
    private List<GameObject> _bloodList;


    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            spawnBlood();
        }
        else if (dripper)
        {
            if (_timer <= 0)
            {
                dripSpawner();
                _timer = dripPerSec;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    private Vector2 get_direction()
    {
        float randomx = Random.Range(-5, 5);
        float randomy = Random.Range(-5, 5);
        return new Vector2(randomx, randomy);
    }

    private void dripSpawner()
    {
  
        GameObject cell = Instantiate(bloodDrop);
        if (splash)
        {
            cell.GetComponent<Rigidbody2D>().velocity = (get_direction());
        }
        StartCoroutine(destroyCell(cell));
    }

    private void spawnBlood()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject cell = Instantiate(bloodDrop);
            if (splash)
            {
                cell.GetComponent<Rigidbody2D>().velocity = (get_direction());
            }
            StartCoroutine(destroyCell(cell));
        }
    }

    private IEnumerator destroyCell(GameObject cell)
    {
        yield return new WaitForSeconds(destroyAfter);
        Destroy(cell);
    }
}
