using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRoll : MonoBehaviour
{
    public void Activate()
    {
        StartCoroutine(WaitTime(3));
        gameObject.SetActive(true);
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
    }
}
