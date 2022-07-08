using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DebugColorType
{
    Red, Blue, Green
};

public class Puzzle: AInteractiveProp
{
    [SerializeField]
    [Tooltip("버튼 눌린 것을 확인하기 위해 임시로 지정해놓은 색깔")]
    private DebugColorType debugColorType;


    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        canInteraction = true;
        isInteraction = false;
        debugColorType = DebugColorType.Red;
    }

    protected override void Start()
    {
        base.Start();

        InputController.Inst.AddCallbackFunc(StartInteraction, "Click", KEY_STATE.Started, gameObject);
        InputController.Inst.AddCallbackFunc(EndInteraction, "Click", KEY_STATE.Canceled, gameObject);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void StartInteraction()
    {
        // 버튼이 동작하지 않는 상황일 경우 return;
        if (!canInteraction)
        {
            return;
        }

        RaycastHit2D hitResult = new ();

        // 현재 버튼이 눌린 경우
        if (CheckMouseRay(ref hitResult))
        {
            canInteraction = false;
            isInteraction = true;

            SetDebugColor();
        }
    }

    public void EndInteraction()
    {
        canInteraction = true;
        isInteraction = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void SetDebugColor()
    {
        switch (debugColorType)
        {
            case DebugColorType.Red:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;

            case DebugColorType.Blue:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;

            case DebugColorType.Green:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;

            default:
                break;
        }
    }

    // 객체가 소멸될때 등록했던 콜백함수를 삭제해준다.
    // 이거 컨트롤러가 알아서 하게끔? 하려다가 매프레임 업데이트에서 체크하는게 더 구릴거같아서 소멸될때 함수호출
    private void OnDestroy()
    {
        
    }
}
