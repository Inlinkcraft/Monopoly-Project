using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    public GameObject diceGraph;

    private Vector3 Rotation;
    private bool isRolling = true;
    private Vector3 TimeOfset;

    // Start is called before the first frame update
    void Awake()
    {
        TimeOfset = new Vector3(Random.value * 10f, Random.value * 10f, Random.value * 10f);
    }

    private void Update()
    {
        if (isRolling)
        {
            Rotation = new Vector3(Mathf.Sin(Time.time + TimeOfset.x) /2f, Mathf.Sin(Time.time + TimeOfset.y) / 2f, Mathf.Sin(Time.time + TimeOfset.z) / 2f);
            diceGraph.transform.rotation *= Quaternion.Euler(Rotation.x, Rotation.y, Rotation.z);
        }
        
    }

    public int Roll()
    {
        int num = Random.Range(1, 6);

        return num;
    }
}
