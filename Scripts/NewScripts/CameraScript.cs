using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Transform player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private GameObject endPoint;
    private Transform endPointPos;
    private GameObject playerGameObject;
    private GameObject yPoint;
    private Transform yPointPos;

    private void Awake()
    {
        playerGameObject = GameObject.Find("Player");
        player = playerGameObject.transform;
        endPoint = GameObject.Find("EndPoint");
        endPointPos = endPoint.transform;
        yPoint = GameObject.Find("YPoint");
        yPointPos = yPoint.transform;
    }

    private void LateUpdate()
    {
        if (player.position.y < yPointPos.position.y)
        {
            Vector3 fallDesiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 fallSmoothedPosition = Vector3.Lerp(transform.position, fallDesiredPosition, smoothSpeed);
            transform.position = fallSmoothedPosition;
        } 
        else if (player.position.x > endPointPos.position.x)
        {
            Vector3 xDesiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 xSmoothedPosition = Vector3.Lerp(transform.position, xDesiredPosition, smoothSpeed);
            transform.position = xSmoothedPosition;
        }
        else
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }




    }

}
