using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 진행도 체크
public class GameManager : MonoSingle<GameManager>
{
    [SerializeField]
    private bool isGameOver;    

    public GameObject SceneController;

    private void Awake()
    {
        IsGameOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    //Getter, Setter
    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

}
