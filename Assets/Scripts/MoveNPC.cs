using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    public GameObject NPCToMove;
    public Transform MoveTo, resetPos;
    public bool following;
    [SerializeField]
    private float moveSpeed;
    private Rigidbody rb;
    Coroutine smoothMove = null;
    private CheckIfMoving check;

    private void Start()
    {
        check = FindObjectOfType<CheckIfMoving>();
        rb = GetComponent<Rigidbody>();
        if(MoveTo == null)
        {
            MoveTo = GameObject.FindGameObjectWithTag("Player").transform.Find("MoveTo").gameObject.transform;
        }
    }
    public void ILikeToMoveIt()
    {
        NPCToMove.transform.position = MoveTo.position;
        //NPCToMove.transform.position = Vector3.Lerp(NPCToMove.transform.position, MoveTo.position, moveSpeed);
    }

    public void NPCLookAt()
    {
        //NPCToMove.transform.LookAt(MoveTo);
        float time = 0.7f;

        Vector3 lookAt = MoveTo.transform.position;
        lookAt.y = transform.position.y;

        //Start new look-at coroutine
        if (smoothMove == null)
            smoothMove = StartCoroutine(LookAtSmoothly(transform, lookAt, time));
        else
        {
            //Stop old one then start new one
            StopCoroutine(smoothMove);
            smoothMove = StartCoroutine(LookAtSmoothly(transform, lookAt, time));
        }
    }
    IEnumerator LookAtSmoothly(Transform objectToMove, Vector3 worldPosition, float duration)
    {
        Quaternion currentRot = objectToMove.rotation;
        Quaternion newRot = Quaternion.LookRotation(worldPosition -
            objectToMove.position, objectToMove.TransformDirection(Vector3.up));

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectToMove.rotation =
                Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
    }
    public void FollowPlayer()
    {
        //NPCToMove.transform.position = Vector3.Lerp(NPCToMove.transform.position, MoveTo.position, moveSpeed);
        //rb.velocity = (MoveTo.position - NPCToMove.transform.position).normalized * moveSpeed;
        following = true;
        
        //rb.AddForce((MoveTo.position - NPCToMove.transform.position) * moveSpeed * Time.deltaTime);

    }
    private void Update()
    {
        if (following)
        {
            if (check.isMoving)
            {
                //rb.MovePosition(MoveTo.position * moveSpeed);  
                NPCToMove.transform.position = Vector3.Lerp(NPCToMove.transform.position, MoveTo.position, moveSpeed);
            }

        }
    }

    public void Reset()
    {
        transform.position = resetPos.position;
        following = false;
    }
}
