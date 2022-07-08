using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AInteractiveProp : MonoBehaviour
{
    protected Sprite sprite;
    protected bool canInteraction;
    protected bool isInteraction;


    // Start is called before the first frame update

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (canInteraction)
        {
            Vector2 mousePosition = InputController.Inst.GetMousePosition();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Ray2D ray2D = new (mousePosition, Vector2.zero);
            RaycastHit2D HitResult = Physics2D.Raycast(ray2D.origin, ray2D.direction);

            // 나 자신이 클릭되었을 때만 true를 return 함
            if (HitResult.collider != null)
            {
                if (HitResult.collider.gameObject == gameObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }

            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;

            }
        }
    }

    protected bool CheckMouseRay(ref RaycastHit2D _hitResult)
    {
        bool result = false;

        Vector2 mousePosition = InputController.Inst.GetMousePosition();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Ray2D ray2D = new (mousePosition, Vector2.zero);
        RaycastHit2D HitResult = Physics2D.Raycast(ray2D.origin, ray2D.direction);

        // 상호작용이 가능한 물체라면 색깔을 변경
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        // 나 자신이 클릭되었을 때만 true를 return 함
        if (HitResult.collider != null)
        {
            if (HitResult.collider.gameObject == gameObject)
            {
                _hitResult = HitResult;
                result = true;
            }
        }

        return result;
    }
}
