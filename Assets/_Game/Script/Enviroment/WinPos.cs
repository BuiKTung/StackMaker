using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    [SerializeField] GameObject chestOpen;
    [SerializeField] GameObject chestClose;
    [SerializeField] ParticleSystem win1,win2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit");
            chestOpen.SetActive(true);
            chestClose.SetActive(false);
            other.GetComponent<PlayerConttroler>().ClearBrick();
            win1.Play();
            win2.Play();
            GameManagement.Instance.ChangState(GameManagement.GameState.Victory);
        }
    }
}
