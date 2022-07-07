using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
   public void GameStart()
    {
        SceneController.Inst.IsSceneEnd = true;
    }

    public void GameExit()
    {
        GameManager.Inst.IsGameOver = true;
    }
}
