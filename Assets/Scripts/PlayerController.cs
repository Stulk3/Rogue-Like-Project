using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public bool isMoving;
    // Аниматор для контроля анимаций
    public Animator spriteAnimator;
    // Показатель горизонтального инпута
    float horizontalMovementInput;
    // Показатель вертикального инпута
    float verticalMovementInput;
    // С какой скоростью будет передвигаться персонаж.
    public float moveSpeed = 5f;
    public bool noObstacleAhead;

    // Точка перемещения.
    public Transform movePoint;

    // Общедоступная маска слоя, которая будет называть, то что останавливает движение
    public LayerMask whatStopsMovement;

    private void Awake() 
    {
        spriteAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Точка перемещения преобразована - больше нет родителя.
        movePoint.parent = null;
        

    }

    // Update is called once per frame
    void Update()
    {


        horizontalMovementInput = Input.GetAxisRaw("Horizontal");
        verticalMovementInput = Input.GetAxisRaw("Vertical");

        spriteAnimator.SetFloat("InputVertical", verticalMovementInput);
        spriteAnimator.SetFloat("InputHorizontal", horizontalMovementInput);



        if(horizontalMovementInput < 0 && !isMoving && noObstacleAhead)
        {
            PlayLeftMoveAnimation();
            spriteAnimator.StopPlayback();
            HandleVerticalAnimations();

        }
        else if(horizontalMovementInput > 0 && !isMoving && noObstacleAhead)
        {
            PlayRightMoveAnimation();
            spriteAnimator.StopPlayback();
            HandleVerticalAnimations();
        }

        if(verticalMovementInput > 0 && !isMoving && noObstacleAhead)
        {
            PlayUpMoveAnimation();
            spriteAnimator.StopPlayback();
            HandleHorizontalAnimations();
        }
        else if(verticalMovementInput < 0 && !isMoving && noObstacleAhead)
        {
            PlayDownMoveAnimation();
            spriteAnimator.StopPlayback();
            HandleHorizontalAnimations();
        }
        

        
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {

            if(Mathf.Abs(horizontalMovementInput) == 1)
            {
                // Проверка на препятсвия впереди, если там ничего нет, то можно идти.
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontalMovementInput, 0f, 0f), .1f, whatStopsMovement))
                {
                    noObstacleAhead = true;
                    movePoint.position += new Vector3(horizontalMovementInput, 0f, 0f);
                }
                else
                {
                    noObstacleAhead = false;
                }
            } 
            else if(Mathf.Abs(verticalMovementInput) == 1)
            {
                // Проверка на препятсвия впереди, если там ничего нет, то можно идти.
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, verticalMovementInput, 0f), .2f, whatStopsMovement))
                {
                    noObstacleAhead = true;
                    movePoint.position += new Vector3(0f, verticalMovementInput, 0f);
                }
                else
                {
                    noObstacleAhead = false;
                }
            }
        }
    }

    void PlayRightMoveAnimation()
    {
        spriteAnimator.StopPlayback();
        spriteAnimator.Play("Right");

    }
    void PlayLeftMoveAnimation()
    {
        spriteAnimator.StopPlayback();
        spriteAnimator.Play("Left");
    }
    void PlayUpMoveAnimation()
    {
        spriteAnimator.StopPlayback();
        spriteAnimator.Play("Up");
    }
    void PlayDownMoveAnimation()
    {
        spriteAnimator.StopPlayback();
        spriteAnimator.Play("Down");

    }

    public void Move()
    {
        isMoving = true;
    }
    public void Stop()
    {
        isMoving = false;
    }

    private void HandleHorizontalAnimations()
    {
        if(horizontalMovementInput < 0 && !isMoving)
        {
            PlayLeftMoveAnimation();
            spriteAnimator.StopPlayback();


        }
        else if(horizontalMovementInput > 0 && !isMoving)
        {
            PlayRightMoveAnimation();
            spriteAnimator.StopPlayback();
        }
    }

    private void HandleVerticalAnimations()
    {
        if(verticalMovementInput > 0 && !isMoving)
        {
            PlayUpMoveAnimation();
            spriteAnimator.StopPlayback();
        }
        else if(verticalMovementInput < 0 && !isMoving)
        {
            PlayDownMoveAnimation();
            spriteAnimator.StopPlayback();
        }
    }
}
