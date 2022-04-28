using UnityEngine;

public class CheckIfMoving : MonoBehaviour
{
    private GameObject p;
    private Vector3 lastUpdatePos = Vector3.zero;
    public float timer = 3f;
    private CalculateDirection c;

    private void Start()
    {
        c = FindObjectOfType<CalculateDirection>();
        p = this.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if(c.target != null)
        {
            if (p.transform.position != lastUpdatePos)
            {
                timer = 3f;
            }
            else if (p.transform.position == lastUpdatePos && timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                c.GetDirection(1);
                timer = 3f;
            }
            lastUpdatePos = p.transform.position;
        }

    }
}
