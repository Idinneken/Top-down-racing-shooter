using UnityEngine;
using Extensions;

public class Player : MonoBehaviour
{
    [Header("Objects")]
    
    public GameObject notificationObject;
    internal TextElement notification;
    public LayerMask groundLayer;
    
    public Transform raycastSource;
    
    public GameObject bulletPrefab;
    public Transform bulletSource;

    public CharacterController characterController;

    public GameObject groundCheckerObject;
    internal GeoChecker groundChecker;

    public GameObject frontCheckerObject;
    internal GeoChecker frontChecker;

    public GameObject backCheckerObject;
    internal GeoChecker backChecker;

    [Header("Gravity")]
    public float gravityStrength;
    public float verticalSpeed;
    public float verticalMove;

    [Space]
    [Header("Movement")]
    public float jumpHeight;

    public float acceleration;
    public float deceleration;
    public float turningLostToGripRate;
    public float normalLostToGripRate;
    public float lostToGripRate;
    public float inAirModifier;

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

    Vector3 moveSum;

    void Start()
    {        
        characterController = GetComponent<CharacterController>();   
        notification = notificationObject.GetComponent<TextElement>();
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
            bullet.sourceObject = gameObject;
            
            bulletObject.transform.position = bulletSource.position;
            bulletObject.transform.forward = transform.forward;
        }
        

        #region Rotation
      
        if (turningLeft)
        {
            rotateSum -= maxRotateAmount * Time.deltaTime;
        }

        if (turningRight)
        {
            rotateSum += maxRotateAmount * Time.deltaTime;
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
            else if (moveSpeed < 0)
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

        moveSum = transform.forward * moveSpeed;

        if (moveSpeed < 0.5 && moving == false)
        {
            moveSpeed = 0;
        }

        // print(moveSpeed);

        characterController.Move(AdjustVelocityToSlope(moveSum * Time.deltaTime));

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        #endregion

        Gravity();

        if (groundChecker.isTouching && Input.GetKeyDown(KeyCode.Space))
        {            
            verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravityStrength);                         
        } 

        // if (groundChecker.isTouching)
        // {
        //    print("touching the ground"); 
        // }

        

        


        #region Gravity

        

        // verticalSpeed = characterController.velocity.y;
        // // verticalMove = (verticalMove + gravityStrength) * Time.deltaTime;
        // verticalMove += verticalSpeed + gravityStrength;

        // if (groundChecker.isTouching)
        // {
        //     verticalMove = 0;
        // }
        
        // #endregion

        // #region Jumping

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     print("should be jumping");
        // }

        // if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isTouching)
        // {
        //     verticalMove = 10f; 
        // }
        
        #region clarity
        
        // if (Input.GetKeyDown(KeyCode.Space) && groundChecker.previousFrameIsTouching)
        // {
        //     // characterController.Move(new Vector3(0, -characterController.velocity.y, 0));
        //     print("is jumping");
        //     // print(Mathf.Sqrt(jumpHeight * -2f * gravityStrength) + -characterController.velocity.y);
        //     // characterController.Move(new Vector3(0,0,0));
        //     print(characterController.velocity);
        //     // characterController.Move(new Vector3(0f,Mathf.Sqrt(jumpHeight * -2f * gravityStrength), 0f));
        //     print(characterController.velocity);
        //     groundChecker.isTouching = false;
        //     groundChecker.DisableCheckerFor(0.02f);
        // }

        // // characterController.Move(new Vector3(0, verticalMove, 0));

        #endregion        
        
        #endregion
    }

    

    private void Gravity()
    {
        verticalSpeed += gravityStrength * Time.deltaTime;                
        characterController.Move(new Vector3(0, verticalSpeed * Time.deltaTime, 0));
        
        if (characterController.isGrounded)
        {      
            verticalSpeed = 0;
        }
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

        // print(moveSum);
        // print(characterController.velocity);
        // print(characterController.velocity.magnitude);

        // print("characterController.velocity.magnitude: " + characterController.velocity.magnitude + "Mathf.Round(new Vector3(characterController.velocity.x, characterController.velocity.z).magnitude) > 0: " + (Mathf.Round(new Vector3(characterController.velocity.x, characterController.velocity.z).magnitude) > 0));

        if (Mathf.Round(moveSum.magnitude) > 0)
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

            
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity_)
    {
        var ray = new Ray(raycastSource.position, Vector3.down); //Cast a ray to the floor

        if (Physics.Raycast(ray, out RaycastHit hit, 0.2f) && hit.collider.gameObject.IsOnLayer_(groundLayer)) //If the ray hits
        {
            //print(hit.collider.gameObject.name);

            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal); //Get the rotation of the slope             
            var adjustedVelocity = slopeRotation * velocity_; //Adjusted velocity = Rotation of the slope * the velocity
            if (adjustedVelocity.y < 0) //If the y of the velocity is less than 0 (if going down the slope)
            {
                // print(adjustedVelocity);
                return adjustedVelocity; //Return the calculated velocity
            }
        }
        return velocity_; //Otherwise, return the velocity already given
    }

}
