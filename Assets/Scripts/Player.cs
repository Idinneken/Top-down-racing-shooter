using UnityEngine;
using Extensions;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    private CharacterController characterController;

    public float acceleration, lostToGripRate, deceleration, targetForwardSpeed, targetBackwardSpeed;
    public float moveSpeed, currentSpeed; 
    
    public float minRotateAmount, maxRotateAmount, minRotateForwardSpeed, maxRotateForwardSpeed, maxRotateBackwardSpeed, minRotateBackwardSpeed;
    private float rotateAmount, rotateSum;    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // currentSpeed = 
        
        
        rotateSum = 0;
        GetRotateAmount();        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bulletObject;
            Bullet bullet;
            this.InstantiatePrefabAndGetComponent(bulletPrefab, out bulletObject, out bullet);
            bullet.sourceObject = gameObject; 
            bullet.sourceObjectController = characterController;                   
        }

        characterController.transform.Rotate(0, rotateSum, 0);   
        // Vector3 move = new Vector3(0, 0, Input.GetAxis("Vertical"));
        // move = new Vector3(move.x * transform.forward.x, move.y * transform.forward.y, move.z * transform.forward.z);        

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (moveSpeed < targetForwardSpeed)
            {
                moveSpeed += acceleration;
            }
            else
            {
                moveSpeed = targetForwardSpeed;
            }
        }
        
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            if (moveSpeed > targetBackwardSpeed)
            {
                moveSpeed -= deceleration;
            }
            else
            {
                moveSpeed = targetBackwardSpeed;
            }
        }

        if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W)) || (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)))
        {
            if (moveSpeed > 0)
            {
                moveSpeed -= lostToGripRate;
            }
            else if (moveSpeed < 0)
            {
                moveSpeed += lostToGripRate;
            }
        }

        currentSpeed = characterController.velocity.z;

        characterController.Move(transform.forward * moveSpeed * Time.deltaTime);

        // moveSpeed = currentSpeed;

        // if (currentSpeed.Absolute_() < moveSpeed.Absolute_())
        // {
        //     moveSpeed = currentSpeed;
        // }
    }

    void GetRotateAmount()
    {        
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rotateSum = 0;
            return;
        }

        if (moveSpeed >= 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotateSum -= maxRotateAmount;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rotateSum += maxRotateAmount;
            }
        }
        else if (moveSpeed < 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotateSum += maxRotateAmount;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rotateSum -= maxRotateAmount;
            }
        }

        // if (currentSpeed > maxRotateForwardSpeed)
        // {
        //     print("currentSpeed > maxRotateForwardSpeed");

        //     if (Input.GetKey(KeyCode.D))
        //     {
        //         rotateSum += maxRotateAmount;
        //     }

        //     if (Input.GetKey(KeyCode.A))
        //     {
        //         rotateSum -= maxRotateAmount;
        //     }
        // }

        // if (currentSpeed <= maxRotateForwardSpeed && currentSpeed >= minRotateForwardSpeed)
        // {
        //     print("currentSpeed <= maxRotateForwardSpeed && currentSpeed >= minRotateForwardSpeed");

        //     float deltaRotateForwardSpeed = maxRotateForwardSpeed - minRotateForwardSpeed; //16-4 = 8
        //     float deltaForwardSpeed = maxRotateForwardSpeed - currentSpeed; //16-12 = 4
        //     float deltaRotateAmount = maxRotateAmount - minRotateAmount; //0.6-0.2 = 0.4
            
        //     // (4/8 * 0.4) = 0.2. 0.2 + minRotateAmount = 0.4

        //     if (Input.GetKey(KeyCode.D))
        //     {
        //         rotateSum += (deltaForwardSpeed / deltaRotateForwardSpeed) + minRotateAmount;
        //     }

        //     if (Input.GetKey(KeyCode.A))
        //     {
        //         rotateSum -= (deltaForwardSpeed / deltaRotateForwardSpeed) + minRotateAmount;
        //     }
        // }

        // if (currentSpeed > minRotateForwardSpeed && currentSpeed > 0)
        // {
        //     print("currentSpeed > minRotateForwardSpeed && currentSpeed > 0");

        //     if (Input.GetKey(KeyCode.D))
        //     {
        //         rotateSum += minRotateAmount;
        //     }

        //     if (Input.GetKey(KeyCode.A))
        //     {
        //         rotateSum -= minRotateAmount;
        //     }
        // }
    }


}
