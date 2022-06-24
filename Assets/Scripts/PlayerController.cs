using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Аниматор для контроля анимаций
    public Animator spriteAnimator;
    // Показатель горизонтального инпута
    float horizontalMovementInput;
    // Показатель вертикального инпута
    float verticalMovementInput;
    // С какой скоростью будет передвигаться персонаж.
    public float moveSpeed = 5f;

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

        Debug.Log(verticalMovementInput);
        Debug.Log(horizontalMovementInput);
        if(horizontalMovementInput<0)
        {
            PlayLeftMoveAnimation();
        }
        if(horizontalMovementInput>0)
        {
            PlayRightMoveAnimation();
        }
        if(verticalMovementInput>0)
        {
            PlayUpMoveAnimation();
        }
        if(verticalMovementInput<0)
        {
            PlayDownMoveAnimation();
        }

        
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {

            if(Mathf.Abs(horizontalMovementInput) == 1)
            {
                // Проверка на препятсвия впереди, если там ничего нет, то можно идти.
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontalMovementInput, 0f, 0f), .1f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(horizontalMovementInput, 0f, 0f);
                }
            } else if(Mathf.Abs(verticalMovementInput) == 1)
            {
                // Проверка на препятсвия впереди, если там ничего нет, то можно идти.
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, verticalMovementInput, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, verticalMovementInput, 0f);
                }
            }
        }
    }

    void PlayRightMoveAnimation()
    {
        spriteAnimator.Play("Right");
    }
    void PlayLeftMoveAnimation()
    {
        spriteAnimator.Play("Left");
    }
    void PlayUpMoveAnimation()
    {
        spriteAnimator.Play("Up");
    }
    void PlayDownMoveAnimation()
    {
        spriteAnimator.Play("Down");
    }
}
