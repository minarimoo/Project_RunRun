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
    [Tooltip("��ư ���� ���� Ȯ���ϱ� ���� �ӽ÷� �����س��� ����")]
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

        InputController.Inst.AddCallbackFunc(StartInteraction, "Click", KEY_STATE.Started);
        InputController.Inst.AddCallbackFunc(EndInteraction, "Click", KEY_STATE.Canceled);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void StartInteraction()
    {
        // ��ư�� �������� �ʴ� ��Ȳ�� ��� return;
        if (!canInteraction)
        {
            return;
        }

        RaycastHit2D hitResult = new RaycastHit2D();

        // ���� ��ư�� ���� ���
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
}
