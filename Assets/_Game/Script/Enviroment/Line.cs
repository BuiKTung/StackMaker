using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] GameObject brick;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hit");
            brick.SetActive(true);
            other.GetComponent<PlayerConttroler>().RemoveBrick();
        }
    }
}
