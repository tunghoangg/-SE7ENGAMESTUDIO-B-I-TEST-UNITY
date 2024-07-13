using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> balls;
    public Button kick;
    public GameObject goalLeft;
    public GameObject goalRight;
    public float kickForce = 10f;
    public Animator animator;
    public float speed;
    private Vector2 move;
    public Vector3 minBounds; 
    public Vector3 maxBounds; 
    public void OnMove(InputAction.CallbackContext context) { 
        move = context.ReadValue<Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }
    public void playerMove() {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            animator.SetBool("IsRun", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

      
        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime;

       
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

       
        transform.position = newPosition;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KickZone")) { 
            kick.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("KickZone"))
        {
            kick.gameObject.SetActive(false);
        }
    }
    public void Kick()
    {
        GameObject ball = FindNearestBall();
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        GameObject nearestGoal = FindNearestGoal(ball);
        Vector3 kickDirection = (nearestGoal.transform.position - ball.transform.position).normalized;
        rb.AddForce(kickDirection * kickForce, ForceMode.Impulse);
    }
    private GameObject FindNearestGoal(GameObject ball) {
        GameObject nearestGoal = null;
        Vector3 directionToGoalLeft = ball.transform.position - goalLeft.transform.position;
        Vector3 directionToGoalRight = ball.transform.position - goalRight.transform.position;
        if (directionToGoalLeft.sqrMagnitude < directionToGoalRight.sqrMagnitude)
        {
            nearestGoal = goalLeft;
        }
        else {
            nearestGoal = goalRight;
        }
        return nearestGoal;
    }
    private GameObject FindNearestBall()
    {
        GameObject nearestBall = null;
        float shortestDistanceSqr = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (GameObject ball in balls)
        {
            Vector3 directionToBall = ball.transform.position - playerPosition;
            float distanceSqrToBall = directionToBall.sqrMagnitude;

            if (distanceSqrToBall < shortestDistanceSqr)
            {
                shortestDistanceSqr = distanceSqrToBall;
                nearestBall = ball;
            }
        }

        return nearestBall;
    }
    public void AutoKick()
    {
        GameObject ball = FindFarthestBall();
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        GameObject nearestGoal = FindNearestGoal(ball);
        Vector3 kickDirection = (nearestGoal.transform.position - ball.transform.position).normalized;
        rb.AddForce(kickDirection * kickForce, ForceMode.Impulse);
    }
    private GameObject FindFarthestBall()
    {
        GameObject farthestBall = null;
        float farthestDistanceSqr = 0;
        Vector3 playerPosition = transform.position;

        foreach (GameObject ball in balls)
        {
            Vector3 directionToBall = ball.transform.position - playerPosition;
            float distanceSqrToBall = directionToBall.sqrMagnitude;

            if (distanceSqrToBall > farthestDistanceSqr)
            {
                farthestDistanceSqr = distanceSqrToBall;
                farthestBall = ball;
            }
        }

        return farthestBall;
    }
}
