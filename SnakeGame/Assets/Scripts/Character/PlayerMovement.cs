using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Control")]
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    public VectorValue playerLoadingPosition;
    public float repulsionTimer;
    public int repulsionForce;



    [Header("Snake Mode")]
    [SerializeField] private float speed = 5.0f;
    private float moveTime;
    //[SerializeField] private float cellSize=0.3f;
    private Vector2 direction = Vector2.up;
    public ItemCounter foodCounter;
    List<GameObject> body = new List<GameObject>();
    public GameObject bodyPrefab;
    public float padding = 0.5f;
    public bool canChangeDirection = true;

    [Header("Character List")]
    public partyMembers party;
    [SerializeField] private List<GameObject> bodyList = new List<GameObject>();
    List<GameObject> bodyParts = new List<GameObject>();
    [SerializeField] private float distanceBetween = 0.2f;

    [Header("Enemy")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackCooldown =1f;
    private float attackTimer;
    private int totalEnemies = 0;

    float countUp = 0;

    //mode switching variables
    public StateControl isSnakeMode;
    private bool snakeMode = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        snakeMode = isSnakeMode.isInState;
        //body.Add(gameObject);
        transform.position = playerLoadingPosition.initialValue;
        /* for (int i = 0; i < foodCounter.counter; i++)
         {
             GrowBody();
         }*/
        bodyParts.Add(gameObject);

        for (int i = 0; i < party.members.Count; i++)
        {
            bodyList.Add(party.members[i]);
        }
        attackTimer = attackCooldown;

    }

    // Update is called once per frame
    /*void Update()
    {

        ChangeDirection();
        MoveFreely();
        MoveLikeASnake();

    }*/

    private void FixedUpdate()
    {

        ChangeDirection();
        MoveFreely();
        MoveLikeASnake();
        shouldAttack();
        if (bodyList.Count > 0 && snakeMode)
        {

            CreateParty();

        }


    }

    private void OnEnable()
    {
        Basic.BodyShouldGrow += GrowBody;
        //LevelManager.PartyMembersChanged += AddBodyList;
    }

    private void OnDisable()
    {
        Basic.BodyShouldGrow -= GrowBody;
        //LevelManager.PartyMembersChanged -= AddBodyList;
    }

    /*public void AddBodyList()
    {
        bodyList.Add(party.members[party.members.Count-1]);
    }*/
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


        animator.SetFloat("InputX", moveInput.x);
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

            /* for (int i = body.Count - 1; i > 0; i--)
         {
             Vector3 directionToPrevious = (body[i - 1].transform.position - body[i].transform.position).normalized;

             // Move the current segment toward the previous segment while maintaining padding
             body[i].transform.position = body[i - 1].transform.position - directionToPrevious * padding;
         }*/

            rb.velocity = direction * speed;
            moveTime = Time.time + 1 / speed;
        }
        if (bodyParts.Count > 1 && snakeMode)
        {
            for (int i = 1; i < bodyParts.Count; i++)
            {
                PathMemory pathM = bodyParts[i - 1].GetComponent<PathMemory>();
                bodyParts[i].transform.position = pathM.pathList[0].position;
                pathM.pathList.RemoveAt(0);
            }
        }

        if (body.Count > 1 && snakeMode)
        {
            for (int i = 1; i < body.Count; i++)
            {
                PathMemory pathM = body[i - 1].GetComponent<PathMemory>();
                body[i].transform.position = pathM.pathList[0].position;
                pathM.pathList.RemoveAt(0);
            }
        }
    }


    private void ChangeDirection()
    {
        if (snakeMode && canChangeDirection)
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
                FlipBody(moveInput.x);

            }
            else if (moveInput.x == -1.0)
            {
                direction = Vector2.left;
                FlipBody(moveInput.x);

            }
            rb.velocity = direction * speed;
        }


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collidable") && snakeMode)
        {
            Vector2 collisionNormal = collision.contacts[0].normal;
            StartCoroutine(RepulsionUponHitting(repulsionTimer, collisionNormal));
        }
    }

    private IEnumerator RepulsionUponHitting(float timer, Vector2 collisionNormal)
    {
        canChangeDirection = false;

        // Apply recoil force using collision normal
        rb.velocity = Vector2.zero;
        rb.AddForce(collisionNormal * repulsionForce);

        // Debug.Log("Recoil applied: " + collisionNormal);

        yield return new WaitForSeconds(timer);

        // After the repulsion time, allow direction changes again
        canChangeDirection = true;
    }

    private void CreateParty()
    {
        PathMemory pathM = bodyParts[bodyParts.Count - 1].GetComponent<PathMemory>();
        if (countUp == 0)
        {
            pathM.ClearPathList();
        }
        countUp += Time.deltaTime;
        if (countUp > distanceBetween)
        {
            GameObject temp = Instantiate(bodyList[0], pathM.pathList[0].position, Quaternion.identity, transform);
            bodyParts.Add(temp);
            bodyList.RemoveAt(0);
            temp.GetComponent<PathMemory>().ClearPathList();
            countUp = 0;
        }
    }

    private void FlipBody(float orientation)
    {
        if(bodyParts.Count>0 && orientation == bodyParts[bodyParts.Count-1].transform.localScale.x)
        {
            for (int i = 1; i < bodyParts.Count; i++)
            {
                Vector3 temp = bodyParts[i].transform.localScale;
                temp.x *= -1;
                bodyParts[i].transform.localScale = temp;
            }
        }
    }

    /*void GrowBody()
    {
        Vector2 position = transform.position;
        if (body.Count != 0)
        {
            position = body[body.Count - 1].position;
        }
        body.Add(Instantiate(bodyPrefab, position, Quaternion.identity).transform);
    }*/

    void GrowBody()
    {
        if (body.Count == 0)
        {
            body.Add(bodyParts[bodyParts.Count - 1]);
        }

        StartCoroutine(CreateItemBody(distanceBetween));



    }

    private IEnumerator CreateItemBody(float distanceBetween)
    {
        PathMemory pathM = body[body.Count - 1].GetComponent<PathMemory>();

        pathM.ClearPathList();
        yield return new WaitForSeconds(distanceBetween);
        GameObject temp = Instantiate(bodyPrefab, pathM.pathList[0].position, Quaternion.identity, transform);
        temp.GetComponent<PathMemory>().ClearPathList();
        body.Add(temp);

    }

    private void shouldAttack()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        attackTimer-=Time.deltaTime;
        if (totalEnemies != 0 && attackTimer<=0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int randomIndex = Random.Range(0, totalEnemies);
            GameObject targetEnemy = enemies[randomIndex];

            // Shoot projectile towards the selected enemy
            ShootProjectile(targetEnemy);
            attackTimer = attackCooldown;
            //shoot projectile
        }
    }

    private void ShootProjectile(GameObject target)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(target);
    }
}
