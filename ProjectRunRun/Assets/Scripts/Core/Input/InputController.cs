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

// �Լ��� Ű ���¸� ���
public struct KeyCallbackInfo 
{
    public delegate void CallbackFunc();

    private CallbackFunc dCallback;
    
    // ��ü �ݹ� �Լ��� �з��س�����.
    private Dictionary<KEY_STATE,CallbackFunc> callbackFunction;
    private Dictionary<string, GameObject> registeredObject;
  

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

    public Dictionary<string, GameObject> RegisteredObject
    {
        get { return registeredObject; }
    }

    public KeyCallbackInfo(KEY_STATE _keyState, CallbackFunc _callback)
    {
        dCallback = null;

        callbackFunction = new Dictionary<KEY_STATE, CallbackFunc>();
        callbackFunction.Add(_keyState, _callback);
        registeredObject = new Dictionary<string, GameObject>();
    }
}


public class InputController : MonoSingle<InputController>,
    Input.ICheatKeysActions, Input.IInteractionActions
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

    // � �׼ǽÿ� ����Ǿ���ϴ� �Լ�����...
    public void AddCallbackFunc(KeyCallbackInfo.CallbackFunc _callBackFunc, string _actionName, KEY_STATE _keyState, GameObject _registeredObject)
    {
        KeyCallbackInfo keyCallback = new KeyCallbackInfo(_keyState, _callBackFunc);
        //keyCallback.Callback += _callBackFunc;

        // Ÿ�� ������ ����� ... .
        // _callBackFunc.Target

        if (!keyCallback.RegisteredObject.ContainsKey(_registeredObject.name))
        {
            keyCallback.RegisteredObject.Add(_registeredObject.name, _registeredObject);
        }

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

    public void DeleteCallbackFunction(GameObject _registeredObject)
    {
        foreach (var item in allKeyCallbacks)
        {
            List<KeyCallbackInfo> callBackList = item.Value;

            foreach (var i in callBackList)
            {
                i.RegisteredObject.TryGetValue(_registeredObject.name, out _registeredObject);
            }
        }

    }

    private bool bCanInput; // Ű �Է��� ���� �� ���� ��Ȳ�� ����� ��

    private void Awake()
    {
        inputSystem = new Input();
        //inputSystem.Interaction.SetCallbacks(instance : this);

        mouse = new Mouse();
        mouse = Mouse.current;

        this.allKeyCallbacks = new Dictionary<string, List<KeyCallbackInfo>>();
    }

    private void OnEnable()
    {
        //inputSystem.Interaction.Enable();
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
    void Input.ICheatKeysActions.OnCheat(InputAction.CallbackContext context)
    {
        // �ش� Callback Action�� ��ϵ� ��� �Լ����� ȣ���ϰ� ��.
        string actionName = context.action.bindings.ToString();

        // �ش� Action�� ��ϵ� callback �Լ��� ���ٸ� return;
        //if (!allKeyCallbacks.ContainsKey(actionName))
        //{
        //    return;
        //}

        //CallRegisteredFunction(context, actionName);
    }

    void Input.IInteractionActions.OnClick(InputAction.CallbackContext context)
    {
        //string actionName = context.action.GetBindingDisplayString();

    }
    // �ش� ������Ʈ�� ��ȣ�ۿ� �ߴ��� üũ..
    // ���콺 �������� �ش� ������Ʈ�� ��ϵ� �Լ��� ȣ���Ų��. 
    // �ڷ�ƾ���� ? ����. ���콺 Ŭ���� �浹�� �����ϴ� �ڷ�ƾ ����. 

    private bool CheckMouseRay2D(ref RaycastHit2D _hitResult)
    {
        bool result = false;

        Vector2 mousePosition = mouse.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Ray2D ray2D = new(mousePosition, Vector2.zero);
        RaycastHit2D HitResult = Physics2D.Raycast(ray2D.origin, ray2D.direction);

        // ��ȣ�ۿ��� ������ ��ü��� ������ ����
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        // �� �ڽ��� Ŭ���Ǿ��� ���� true�� return ��
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
