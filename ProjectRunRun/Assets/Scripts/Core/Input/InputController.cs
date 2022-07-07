using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// ���߿� Callback �Լ��� �ٲܶ� ����� ��
public enum KEY_STATE
{
    Started,
    Peformed,
    Canceled,
    None
}

public struct KeyCallbackInfo 
{
    public delegate void CallbackFunc();
    private CallbackFunc dCallback;
    private Dictionary<KEY_STATE,CallbackFunc> callbackFunction;
  

    public CallbackFunc Callback
    {
        get { return dCallback; }
        set { dCallback = value; }
    }

    public Dictionary<KEY_STATE, CallbackFunc> CallbackFunction
    {
        get { return callbackFunction; }
        set { callbackFunction = value; }
    }

    public KeyCallbackInfo(KEY_STATE _keyState, CallbackFunc _callback)
    {
        dCallback = null;

        callbackFunction = new Dictionary<KEY_STATE, CallbackFunc>();
        callbackFunction.Add(_keyState, _callback);
    }
}


public class InputController : MonoSingle<InputController>,
    Input.IInteractionActions
{
    private Input inputSystem;
    private Mouse mouse;
    
    public Mouse GetMouse
    {
        get { return mouse; }
        private set { }
    }

    public Vector2 GetMousePosition()
    {
       return mouse.position.ReadValue();
    }
        

    //public delegate void CallbackFunc();
    //private static CallbackFunc dCallback;
    private Dictionary<string, List<KeyCallbackInfo>> allKeyCallbacks;

    public void AddCallbackFunc(KeyCallbackInfo.CallbackFunc _callBackFunc, string _actionName, KEY_STATE _keyState)
    {
        KeyCallbackInfo keyCallback = new KeyCallbackInfo(_keyState, _callBackFunc);
        keyCallback.Callback += _callBackFunc;


        // �̹� key���� �����ϴ� ��� (�̹� ��ϵ� �Լ��� ������ ���)
        if (allKeyCallbacks.ContainsKey(_actionName))
        {
            List<KeyCallbackInfo> callBackList = allKeyCallbacks[_actionName];
            callBackList.Add(keyCallback);
        }

        else
        {
            List<KeyCallbackInfo> callBackList = new List<KeyCallbackInfo>();
            callBackList.Add(keyCallback);
            allKeyCallbacks.Add(_actionName, callBackList);
        }
    }

    private bool bCanInput; // Ű �Է��� ���� �� ���� ��Ȳ�� ����� ��

    private void Awake()
    {
        inputSystem = new Input();
        inputSystem.Interaction.SetCallbacks(instance : this);

        mouse = new Mouse();
        mouse = Mouse.current;

        this.allKeyCallbacks = new Dictionary<string, List<KeyCallbackInfo>>();
    }

    private void OnEnable()
    {
        inputSystem.Interaction.Enable();
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    
    private void CallRegisteredFunction(InputAction.CallbackContext _context, string _keyName)
    {
        if (_context.started)
        {
            foreach (var item in allKeyCallbacks[_keyName])
            {
                if (item.CallbackFunction.ContainsKey(KEY_STATE.Started))
                    item.CallbackFunction[KEY_STATE.Started]();
            }

            return;
        }

        if (_context.performed)
        {
            foreach (var item in allKeyCallbacks[_keyName])
            {
                if (item.CallbackFunction.ContainsKey(KEY_STATE.Peformed))
                    item.CallbackFunction[KEY_STATE.Peformed]();
            }

            return;
        }

        if (_context.canceled)
        {
            foreach (var item in allKeyCallbacks[_keyName])
            {
                if (item.CallbackFunction.ContainsKey(KEY_STATE.Canceled))
                    item.CallbackFunction[KEY_STATE.Canceled]();
            }

            return;
        }
    }

    // Mapping Functions
    void Input.IInteractionActions.OnClick(InputAction.CallbackContext context)
    {
        // �ش� Callback Action�� ��ϵ� ��� �Լ����� ȣ���ϰ� ��.
        string actionName = context.action.name;

        // �ش� Action�� ��ϵ� callback �Լ��� ���ٸ� return;
        if (!allKeyCallbacks.ContainsKey(actionName))
        {
            return;
        }

        CallRegisteredFunction(context, actionName);
    }

}
