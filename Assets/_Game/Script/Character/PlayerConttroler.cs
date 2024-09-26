using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class PlayerConttroler : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] Transform player;
    [SerializeField] GameObject Brick;
    [SerializeField] Animator Animator;
    List<GameObject> list_Brick = new List<GameObject> ();
    float jumpHight = 0.3f;
    float stackCount = 0;
    float objectHight = 0.3f;
    public float speed = 1.0f;
    private Vector2 startPos, endPos;
    private Touch touch;
    private IEnumerator coroutine;
    private bool moveAllow = true;
    private Vector3 targetPos;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }
        if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended && moveAllow)
        {
            endPos = touch.position;
            if (endPos.y > startPos.y && Mathf.Abs(touch.deltaPosition.y) >= Mathf.Abs(touch.deltaPosition.x)) {
                if (CheckTargetPos(new Vector3(0f, 0f, 1f), ref targetPos))
                {
                    coroutine = Move(targetPos);
                    StartCoroutine(coroutine);
                }

            }

            else if (endPos.y < startPos.y && Mathf.Abs(touch.deltaPosition.y) >= Mathf.Abs(touch.deltaPosition.x))
            {
                if (CheckTargetPos(new Vector3(0f, 0f, -1f), ref targetPos))
                {
                    coroutine = Move(targetPos);
                    StartCoroutine(coroutine);
                }

            }
            else if (endPos.x < startPos.x && Mathf.Abs(touch.deltaPosition.y) < Mathf.Abs(touch.deltaPosition.x))
            {
                if (CheckTargetPos(new Vector3(-1f, 0f, 0f), ref targetPos))
                {
                    coroutine = Move(targetPos);
                    StartCoroutine(coroutine);
                }

            }
            else if (endPos.x > startPos.x && Mathf.Abs(touch.deltaPosition.y) < Mathf.Abs(touch.deltaPosition.x))
            {
                if (CheckTargetPos(new Vector3(1f, 0f, 0f), ref targetPos))
                {
                    coroutine = Move(targetPos);
                    StartCoroutine(coroutine);
                }

            }
            
        }
        if(GameManagement.Instance.currentState == GameManagement.GameState.Victory)
            {
                moveAllow = false;
                
            }
    }
    private IEnumerator Move(Vector3 targetPos)
    {

        moveAllow = false;
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        moveAllow = true;

    }

    private bool CheckTargetPos(Vector3 direction, ref Vector3 targetPos)
    {
        RaycastHit hit;
        targetPos = transform.position;
        while (true)
        {

            Physics.Raycast(targetPos, Vector3.down, out hit, 5f, groundLayer);

            if (hit.collider == null)
            {
                targetPos -= direction;
                break;
            }
            targetPos += direction;
        }
        return (targetPos == transform.position) ? false : true;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Brick"))
    //    {
    //        Vector3 newPos = player.position;
    //        newPos.y += jumpHight;
    //        player.position = newPos;

    //        Transform t = collision.transform;
    //        gameObject.tag = "Untagged";
    //        t.SetParent(this.transform);
    //        t.localPosition = new Vector3(0, stackCount * objectHight, 0);
    //        stackCount++;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Vector3 newPos = player.position;
            newPos.y += jumpHight;
            player.position = newPos;
            Animator.SetTrigger("Take");
            //Transform t = other.transform;
            //gameObject.tag = "Untagged";
            //t.SetParent(this.transform);
            //t.localPosition = new Vector3(0, stackCount * objectHight, 0);
            //Debug.Log("HIT");
            AddBrick();
            Animator.SetTrigger("Take");

        }
       
    }
    private void AddBrick()
    {
        stackCount++;
        GameObject brick = Instantiate(Brick, new Vector3(transform.position.x,transform.position.y + stackCount * jumpHight,transform.position.z),Quaternion.Euler(-90,0,0),transform);
        list_Brick.Add(brick);
    }
    public void RemoveBrick()
    {
        stackCount--;
        GameObject objRemove = list_Brick[list_Brick.Count - 1];
        list_Brick.Remove(objRemove);
        Destroy(objRemove);
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y - jumpHight,transform.position.z);
    }

    public void ClearBrick()
    {
        foreach(GameObject obj in list_Brick)
            Destroy(obj);
        list_Brick.Clear();
        Animator.SetTrigger("Win");
        Vector3 newPos = player.transform.position;
        newPos.y -= stackCount*objectHight;
        player.transform.position = newPos;
        
    }
}
