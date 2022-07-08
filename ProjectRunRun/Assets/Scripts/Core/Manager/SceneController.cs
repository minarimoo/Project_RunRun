using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoSingle<SceneController>
{
    public enum SCENE_LIST
    {
        TitleScene,
        MainScene,
        EndingScene
    }

    public SCENE_LIST currentScene;
    public SCENE_LIST nextScene;

    private bool isSceneEnd;

    private void Awake()
    { 
        GameManager.Inst.sceneController = SceneController.Inst.gameObject;

        currentScene = SCENE_LIST.TitleScene;
        nextScene = SCENE_LIST.MainScene;
    }

    //void Start()
    //{
    //}

        // Update is called once per frame
        void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            isSceneEnd = true;
        }

        UpdateNextScene();

        CheckChangeScene();
    }

    void CheckChangeScene()
    {
        if (isSceneEnd)
        {
            isSceneEnd = false;
            currentScene = nextScene;

            SceneManager.LoadScene(nextScene.ToString());
        }
    }

    // Getter, Setter
    public bool IsSceneEnd
    {
        get { return isSceneEnd; }
        set { isSceneEnd = value; }
    }

    // 현재 강제로 다음 씬을 설정
    private void UpdateNextScene()
    {
        switch (currentScene)
        {
            case SCENE_LIST.TitleScene:
                nextScene = SCENE_LIST.MainScene;
                break;
            case SCENE_LIST.MainScene:
                nextScene = SCENE_LIST.EndingScene;
                break;
            case SCENE_LIST.EndingScene:
                nextScene = SCENE_LIST.TitleScene;
                break;
            default:
                break;
        }
    }
}
