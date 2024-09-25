using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManage : MonoBehaviour
{
    [SerializeField] GameObject level;
    //[SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera camera;
    //protected override void Awake()
    //{
    //    base.Awake();
    //}
    private void Start()
    {
        PlayerOnInnit();
        if(camera != null && player != null)
        {
            camera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        }
        //GameManagement.Instance.ChangState(GameManagement.GameState.Playing);
    }
    void PlayerOnInnit()
    {
        Transform startPos = GameObject.FindGameObjectWithTag("Start").transform;
        
        Instantiate(player,new Vector3( startPos.transform.position.x,startPos.transform.position.y+3f,startPos.transform.position.z),Quaternion.Euler(0,0,0));
    }
   

}
