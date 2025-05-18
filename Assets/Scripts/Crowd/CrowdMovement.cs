using UnityEngine;

public class CrowdMovement : MonoBehaviour
{

    [SerializeField] private float jumpHeight = 0.3f; 
    [SerializeField] private float jumpSpeed = 2f;   
    [SerializeField] private float idleTimeMin = 1f;  
    [SerializeField] private float idleTimeMax = 3f;  

    private float initialY;   
    private float jumpProgress = 0f;
    private bool isJumping = false;  
    private float idleTimer = 0f;    

    void Start()
    {
        initialY = transform.position.y;
        ResetIdleTimer();
    }

    void Update()
    {
        if (isJumping)
        {
            jumpProgress += Time.deltaTime * jumpSpeed;

            float offset = Mathf.Sin(jumpProgress * Mathf.PI) * jumpHeight;
            transform.position = new Vector3(transform.position.x, initialY + offset, transform.position.z);

            if (jumpProgress >= 1f)
            {
                isJumping = false;
                ResetIdleTimer();
            }
        }
        else
        {
            idleTimer -= Time.deltaTime;

            if (idleTimer <= 0f)
            {
                isJumping = true;
                jumpProgress = 0f; 
            }
        }
    }
    private void ResetIdleTimer()
    {
        idleTimer = Random.Range(idleTimeMin, idleTimeMax);
    }
}
