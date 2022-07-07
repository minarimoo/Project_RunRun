using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTimer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("게임 전체 시간")]
    private float lifeTime;

    private Slider timeSlider;
    private float currentTime = 0f;

    private void Awake()
    {
        timeSlider = GetComponent<Slider>();
        timeSlider.maxValue = lifeTime;
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= lifeTime)
        {
            currentTime += Time.deltaTime;
            timeSlider.value = currentTime;
        }

        else
        {
            Debug.Log("Time Over");
            GameManager.Inst.IsGameOver = true;
        }
    }
}
