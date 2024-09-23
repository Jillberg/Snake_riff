using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Control")]
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    
    
    [Header("Snake Mode")]
    [SerializeField] private float speed=5.0f;
    private float moveTime;
    //[SerializeField] private float cellSize=0.3f;
    private Vector2 direction = Vector2.up;
    public ItemCounter foodCounter;
    List<Transform> body=new List<Transform>();
    public Transform bodyPrefab;
    public float padding=0.5f;


    //mode switching variables
    public StateControl isSnakeMode;
    private bool snakeMode = false;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        snakeMode = isSnakeMode.isInState;
        body.Add(this.transform);
        for (int i = 0; i < foodCounter.counter; i++)
        {
            GrowBody();
        }
    }

    // Update is called once per frame
    void Update()
    {

        ChangeDirection();
        MoveFreely();
        MoveLikeASnake();

    }

    /*private void FixedUpdate()
    {
        ChangeDirection();
        MoveFreely();
        MoveLikeASnake();
    }*/

    private void OnEnable()
    {
        Basic.BodyShouldGrow += GrowBody;
    }

    private void OnDisable()
    {
        Basic.BodyShouldGrow -= GrowBody;
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (!snakeMode)
        {
            animator.SetBool("isWalking", true);
        }
            if (context.canceled) 
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);

        }

        moveInput = context.ReadValue<Vector2>();


        animator.SetFloat("InputX",moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    private void MoveFreely()
    {
        if (!snakeMode)
        {
            rb.velocity = moveInput * moveSpeed;
            
        }
       
    }

    private void MoveLikeASnake()
    {
        if (snakeMode && Time.time > moveTime)
        {
            for(int i = body.Count - 1; i > 0; i--)
            {
                Vector3 directionToPrevious = (body[i - 1].position - body[i].position).normalized;

                // Move the current segment toward the previous segment while maintaining padding
                body[i].position = body[i - 1].position - directionToPrevious * padding;
            }
           /* if(body.Count > 0)
            {
                body[0].position=(Vector2)transform.position;
            }*/
            //transform.position += (Vector3)direction * cellSize;
            rb.velocity = direction * speed;
            moveTime =Time.time+1/speed;
        }
    }

    void GrowBody()
    {
        Vector2 position = transform.position;
        if (body.Count != 0)
        { 
            position=body[body.Count-1].position;
        }
        body.Add(Instantiate(bodyPrefab,position,Quaternion.identity).transform);
    }
    private void ChangeDirection()
    {
        if (snakeMode)
        {
            if (moveInput.y == -1.0)
            {
                direction = Vector2.down;
            }
            else if (moveInput.y == 1.0)
            {
                direction = Vector2.up;
            }
            else if (moveInput.x == 1.0)
            {
                direction = Vector2.right;
            }
            else if (moveInput.x == -1.0)
            {
                direction = Vector2.left;
            }
            rb.velocity = direction * speed;
        }

        
    }

}
