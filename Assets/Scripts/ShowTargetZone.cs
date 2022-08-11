using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTargetZone : MonoBehaviour
{
    public GameObject targetZone;

    public void showTarget()
    {
        targetZone.SetActive(true);
    }
}
