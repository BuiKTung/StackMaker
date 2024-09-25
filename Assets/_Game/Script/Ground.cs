using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] GameObject brick;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            Debug.Log(gameObject);
            brick.SetActive(false);
            //other.GetComponent<PlayerConttroler>().RemoveBrick();
        }
    }

}
