﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스매니저는 게임의 흐름을 관리합니다.
/// </summary>
public class WPGameRoutineManager : MonoBehaviour {
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPGameRoutineManager instance = null;

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        Init();
    }

    IEnumerator MainRoutine()
    {
        float timeCounter = 0f;

        for(; ; )
        {
            for (timeCounter = 0f; timeCounter < 5f; timeCounter += Time.deltaTime * WPVariable.timeScale_WPDateTime)
            {
                yield return null;
                if (WPVariable.timeScale_WPDateTime <= 0) timeCounter = 0f;
            }

            if (timeCounter >= 5f)
            {
                WPDateTime.Now.AddHour(1);
                
                timeCounter = 0f;
                foreach(int newsID in WPGameDataManager.instance.GetData<WPData_Event>(WPEnum.GameData.eEvent)[WPDateTime.Now.Month - 1].GetNewsIDByCount(3))
                {
                    WPData_News news = WPGameDataManager.instance.GetData<WPData_News>(WPEnum.GameData.eNews)[newsID];
                    WPGameCommon._WPDebug(newsID + " // " + news.Name + " : " + news.Description);
                }
            }
            
        }
    }

    private void Init()
    {
        instance = this;

        WPDateTime.Now.OnTimeChanged += SaveTimeData;

        StartCoroutine(MainRoutine());
    }

    private void SaveTimeData(WPDateTime content)
    {
        //WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserDate, content.ToData());
        WPUserDataManager.instance.DateTime = content;
    }

}
