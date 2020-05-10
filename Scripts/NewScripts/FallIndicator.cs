using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIndicator : MonoBehaviour
{
    public AK.Wwise.Event fallIndicator = new AK.Wwise.Event();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fallIndicator.Post(gameObject);
    }
}
