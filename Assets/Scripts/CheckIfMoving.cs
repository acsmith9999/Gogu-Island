using UnityEngine;

public class CheckIfMoving : MonoBehaviour
{
    public bool isMoving = false;
    public GameObject p;
    private Vector3 lastUpdatePos = Vector3.zero;
    public float timer = 3f;
    private CalculateDirection c;

    private void Start()
    {
        c = FindObjectOfType<CalculateDirection>();
    }
    // Update is called once per frame
    void Update()
    {
        if(p.transform.position != lastUpdatePos)
        {
            isMoving = true;
            timer = 3f;
        }
        else if (p.transform.position == lastUpdatePos && timer >0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            isMoving = false;
            c.Triangulate(c.sceneName);
            timer = 3f;
        }
        lastUpdatePos = p.transform.position;
    }
}
