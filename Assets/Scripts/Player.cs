using UnityEngine;
using Extensions;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSource;

    private CharacterController characterController;

    public GameObject groundCheckerObject;
    internal GeoChecker groundChecker;

    public GameObject frontCheckerObject;
    internal GeoChecker frontChecker;

    public GameObject backCheckerObject;
    internal GeoChecker backChecker;

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
    [Space]
    public float movingForwardTargetSpeed;
    public float normalMovingForwardTargetSpeed;
    public float lowerMovingForwardTargetSpeed;
    public float movingBackwardTargetSpeed;
    public float normalMovingBackwardTargetSpeed;
    public float lowerBackwardTargetSpeed;
    public float footOffGasTargetSpeed;
    public float targetSpeed;
    [Space]
    public float moveSpeed;
    public float currentSpeed;
    [Space]
    public float maxSpeed;
    public float maxNegativeSpeed;


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

    public bool travelling;
    public bool residualTravelling;

    void Start()
    {
        characterController = GetComponent<CharacterController>();   
        groundChecker = groundCheckerObject.GetComponent<GeoChecker>();
        frontChecker = frontCheckerObject.GetComponent<GeoChecker>();
        backChecker = backCheckerObject.GetComponent<GeoChecker>();        
    }

    void Update()
    {
        GetStates();

        rotateSum = 0;
        currentSpeed = new Vector3(characterController.velocity.x, characterController.velocity.z).magnitude;

        if (moveSpeed < 0)
        {
            currentSpeed *= -1;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject bulletObject;
            Bullet bullet;
            this.InstantiatePrefabAndGetComponent(bulletPrefab, out bulletObject, out bullet);
            bulletObject.transform.position = bulletSource.position;
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

        if (movingForward)
        {
            targetSpeed = movingForwardTargetSpeed;

            if (moveSpeed < targetSpeed)
            {                
                moveSpeed += acceleration;
            }
            else 
            {                
                moveSpeed = movingForwardTargetSpeed;
            }
        }
        
        if (movingBackward)
        {
            targetSpeed = movingBackwardTargetSpeed;

            if (moveSpeed > movingBackwardTargetSpeed)
            {         
                moveSpeed -= deceleration;         
            }
            else 
            {                
                moveSpeed = movingBackwardTargetSpeed;
            }
        }

        if (residualTravelling)
        {
            targetSpeed = footOffGasTargetSpeed;

            if (moveSpeed > 0)
            {
                moveSpeed -= lostToGripRate;
            }
            else if (currentSpeed < 0)
            {
                moveSpeed += lostToGripRate;
            }
        }

        if (frontChecker.firstFrameTouching || backChecker.firstFrameTouching)
        {
            moveSpeed = 0;
        }

        if (frontChecker.isTouching)
        {
            movingForwardTargetSpeed = lowerMovingForwardTargetSpeed;
        }
        else
        {
            movingForwardTargetSpeed = normalMovingForwardTargetSpeed;
        }

        if (backChecker.isTouching)
        {
            movingBackwardTargetSpeed = lowerBackwardTargetSpeed;
        }
        else
        {
            movingBackwardTargetSpeed = normalMovingBackwardTargetSpeed;
        }

        #endregion


        #region last minute checks

        if (moveSpeed > maxSpeed)
        {
            moveSpeed = maxSpeed;
        }

        if (moveSpeed < maxNegativeSpeed)
        {
            moveSpeed = maxNegativeSpeed;
        }
       
        Vector3 moveSum = transform.forward * moveSpeed * Time.deltaTime;

        if (moveSum.magnitude < 0.05 && moving == false)
        {
            moveSpeed = 0;
        }

        characterController.Move(moveSum);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        #endregion

        #region Jumping

        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isTouching)
        {
            characterController.Move(new Vector3(0f, Mathf.Sqrt(jumpHeight * -2f * gravityStrength), 0f));
        }

        #endregion
    }

    void GetStates()
    {
        #region Turning

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

        #endregion

        #region F+B

        if (Input.GetKey(KeyCode.W))
        {
            movingForward = true;
        }
        else
        {
            movingForward = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movingBackward = true;
        }
        else
        {
            movingBackward = false;
        }

        if (movingForward || movingBackward)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            moving = false;
            movingBackward = false;
            movingForward = false;
        }

        if (Mathf.Round(new Vector3(characterController.velocity.x, characterController.velocity.z).magnitude) > 0)
        {
            travelling = true;
        }
        else
        {
            travelling = false;
        }

        if (travelling && moving == false)
        {
            residualTravelling = true;
        }
        else
        {
            residualTravelling = false;
        }

        #endregion

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
