using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 나중에 Callback 함수로 바꿀때 사용할 것
public enum KEY_STATE
{
    Started,
    Peformed,
    Canceled,
    None
}

// 함수와 키 상태를 등록
public struct KeyCallbackInfo 
{
    public delegate void CallbackFunc();

    private CallbackFunc dCallback;
    
    // 전체 콜백 함수를 분류해놓은것.
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

    // 어떤 액션시에 수행되어야하는 함수인지...
    public void AddCallbackFunc(KeyCallbackInfo.CallbackFunc _callBackFunc, string _actionName, KEY_STATE _keyState, GameObject _registeredObject)
    {
        KeyCallbackInfo keyCallback = new KeyCallbackInfo(_keyState, _callBackFunc);
        //keyCallback.Callback += _callBackFunc;

        // 타겟 저장방식 고민좀 ... .
        // _callBackFunc.Target

        if (!keyCallback.RegisteredObject.ContainsKey(_registeredObject.name))
        {
            keyCallback.RegisteredObject.Add(_registeredObject.name, _registeredObject);
        }

        // 이미 key값이 존재하는 경우 (이미 등록된 함수가 존재할 경우)
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

    private bool bCanInput; // 키 입력을 받을 수 없는 상황도 고려할 것

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
        // 해당 Callback Action에 등록된 모든 함수들을 호출하게 함.
        string actionName = context.action.bindings.ToString();

        // 해당 Action에 등록된 callback 함수가 없다면 return;
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
    // 해당 오브젝트와 상호작용 했는지 체크..
    // 마우스 눌렸을때 해당 오브젝트면 등록된 함수를 호출시킨다. 
    // 코루틴으로 ? 제작. 마우스 클릭시 충돌을 검출하는 코루틴 실행. 

    private bool CheckMouseRay2D(ref RaycastHit2D _hitResult)
    {
        bool result = false;

        Vector2 mousePosition = mouse.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Ray2D ray2D = new(mousePosition, Vector2.zero);
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
