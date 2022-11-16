using UnityEngine;
using Extensions;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    private CharacterController characterController;

    public GameObject groundCheckerObject;
    internal GeoChecker groundChecker;

    public GameObject frontCheckerObject;
    internal GeoChecker frontChecker;

    [Header("Gravity")]
    public float gravityStrength;
    public float downSpeed;
    public float downMove;

    [Space]
    [Header("Movement")]

    public float jumpHeight;

    public float acceleration;
    public float deceleration;
    public float turningLostToGripRate;
    public float normalLostToGripRate;
    public float lostToGripRate;
    public float normalTargetForwardSpeed;
    public float lowerTargetForwardSpeed;
    public float targetForwardSpeed;
    public float targetBackwardSpeed;
    public float moveSpeed;
    public float currentSpeed;

    [Space]
    [Header("Rotation")]
    public float minRotateAmount;
    public float maxRotateAmount;
    public float minRotateForwardSpeed;
    public float maxRotateForwardSpeed;
    public float maxRotateBackwardSpeed;
    public float minRotateBackwardSpeed;
    public float rotateAmount;
    public float rotateSum;

    [Space]
    [Header("State")]
    public bool turning;
    public bool turningLeft;
    public bool turningRight;

    public bool moving;
    public bool movingForward;
    public bool movingBackward;

    public bool grounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();   
        groundChecker = groundCheckerObject.GetComponent<GeoChecker>();
        frontChecker = frontCheckerObject.GetComponent<GeoChecker>();
    }

    void Update()
    {
        GetStates();

        rotateSum = 0;

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject bulletObject;
            Bullet bullet;
            this.InstantiatePrefabAndGetComponent(bulletPrefab, out bulletObject, out bullet);
            bullet.sourceObject = gameObject; 
            bullet.sourceObjectController = characterController;                   
        }
        
        #region Gravity

        downSpeed = characterController.velocity.y;
        downMove += downSpeed + gravityStrength;

        if (groundChecker.isTouching)
        {
            downMove = 0;
        }

        characterController.Move(new Vector3(0f, downMove, 0f) * Time.deltaTime);

        #endregion

        #region Rotation
      
        if (turningLeft)
        {
            rotateSum -= maxRotateAmount;
        }

        if (turningRight)
        {
            rotateSum += maxRotateAmount;
        }

        if (turning)
        {
            lostToGripRate = turningLostToGripRate;
        }
        else
        {
            lostToGripRate = normalLostToGripRate;
        }


        characterController.transform.Rotate(0, rotateSum, 0);

        #endregion

        #region Back + forth
        
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

        if (frontChecker.firstFrameTouching)
        {
            moveSpeed = 0;
        }

        if (frontChecker.isTouching)
        {
            targetForwardSpeed = lowerTargetForwardSpeed;
        }
        else
        {
            targetForwardSpeed = normalTargetForwardSpeed;
        }

        #endregion

        characterController.Move(transform.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isTouching)
        {
            characterController.Move(new Vector3(0f, Mathf.Sqrt(jumpHeight * -2f * gravityStrength), 0f));
        }
    }

    void GetStates()
    {        
        if (Input.GetKey(KeyCode.A))
        {
            turningLeft = true;
        }
        else
        {
            turningLeft = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            turningRight = true;
        }
        else
        {
            turningRight = false;
        }

        if (turningLeft || turningRight)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            turning = false;
            turningLeft = false;
            turningRight = false;
        }

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
