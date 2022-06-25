using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    // Аниматор для контроля анимаций
    public Animator spriteAnimator;
    // Показатель горизонтального инпута
    float horizontalMovementInput;
    // Показатель вертикального инпута
    float verticalMovementInput;
    // С какой скоростью будет передвигаться персонаж.
    public float moveSpeed = 5f;
    public bool nothingAhead;

    // Точка перемещения.
    public Transform movePoint;

    // Общедоступная маска слоя, которая будет называть, то что останавливает движение
    public LayerMask whatStopsMovement;

    private void Awake()
    {
        spriteAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        // Точка перемещения преобразована - больше нет родителя.
        movePoint.parent = null;
    }
    void Update()
    {
        GetAxisInput();

        HandleAnimationChange();

        CalculateMovePointPosition();
        MoveToPosition(movePoint.position);
        
       
    }
    void MoveToPosition(Vector3 movePosition)
    {
        this.transform.position = Vector3.MoveTowards(transform.position, movePosition, moveSpeed * Time.deltaTime);
    }
    void CalculateMovePointPosition()
    {
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {

            if (Mathf.Abs(horizontalMovementInput) == 1)
            {
                // Проверка на препятсвия впереди, если там ничего нет, то можно идти.
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontalMovementInput, 0f, 0f), .1f, whatStopsMovement))
                {
                    nothingAhead = true;
                    movePoint.position += new Vector3(horizontalMovementInput, 0f, 0f);
                }
                else
                {
                    nothingAhead = false;
                }
            }
            else if (Mathf.Abs(verticalMovementInput) == 1)
            {
                // Проверка на препятсвия впереди, если там ничего нет, то можно идти.
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, verticalMovementInput, 0f), .2f, whatStopsMovement))
                {
                    nothingAhead = true;
                    movePoint.position += new Vector3(0f, verticalMovementInput, 0f);
                }
                else
                {
                    nothingAhead = false;
                }
            }
        }
    }
    void GetAxisInput()
    {
        horizontalMovementInput = Input.GetAxisRaw("Horizontal");
        verticalMovementInput = Input.GetAxisRaw("Vertical");
    }
    void HandleAnimationChange()
    {
        if (horizontalMovementInput < 0 && !isMoving && nothingAhead)
        {
            PlayLeftMoveAnimation();
            spriteAnimator.StopPlayback();
            HandleVerticalAnimations();

        }
        else if (horizontalMovementInput > 0 && !isMoving && nothingAhead)
        {
            PlayRightMoveAnimation();
            spriteAnimator.StopPlayback();
            HandleVerticalAnimations();
        }

        if (verticalMovementInput > 0 && !isMoving && nothingAhead)
        {
            PlayUpMoveAnimation();
            spriteAnimator.StopPlayback();
            HandleHorizontalAnimations();
        }
        else if (verticalMovementInput < 0 && !isMoving && nothingAhead)
        {
            PlayDownMoveAnimation();
            spriteAnimator.StopPlayback();
            HandleHorizontalAnimations();
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
    public void Move()
    {
        isMoving = true;
    }
    public void Stop()
    {
        isMoving = false;
    }

   
}
