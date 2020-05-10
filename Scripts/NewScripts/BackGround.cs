using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    //Floats
    private float length, startPos, height, startPosy;
    public float paraEffect;
    public float paraEffecty;
    //Gameobjects
    public GameObject mainCamera;
    public bool yEnabled;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        startPosy = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tempy = (mainCamera.transform.position.y * (1 - paraEffecty));
        float temp = (mainCamera.transform.position.x * (1 - paraEffect));
        float distance = (mainCamera.transform.position.x * paraEffect);
        float distancey = (mainCamera.transform.position.y * paraEffecty);

        transform.position = new Vector3(startPos + distance, startPosy + distancey, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        } else if (temp < startPos - length)
        {
            startPos -= length;
        }

        if (yEnabled && tempy > startPosy + height)
        {
            startPosy += height;
        } else if (tempy < startPosy - height)
        {
            startPosy -= height;
        }
    }
}
