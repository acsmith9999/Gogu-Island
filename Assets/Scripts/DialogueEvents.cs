using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEvents : MonoBehaviour
{
    public GameObject overlay;

    // Start is called before the first frame update
    void Start()
    {
        //foreach (RawImage r in overlay)
        //{
        //    r.enabled = false;
        //}
        overlay.SetActive(false);
    }

    public void ToggleOverlay()
    {
        //foreach(RawImage r in overlay)
        //{
        //    r.enabled = !r.isActiveAndEnabled;
        //}
        overlay.SetActive(!overlay.activeInHierarchy);
    }
}
