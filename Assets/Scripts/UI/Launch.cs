using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launch : MonoBehaviour
{
    Slider slider;
    Text info;
    
    GameMgr gameMgr;
    GlobalData globalData;

    private void Awake()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        info = GameObject.Find("Text").GetComponent<Text>();

        gameMgr = GameMgr.gameMgr;
        globalData = GameMgr.globalData;
    }

    private void Start()
    {
        StartCoroutine(SliderPractice());
    }

    IEnumerator SliderPractice()
    {
        int wfsCount = globalData.waitForSeconds.Count;
        string[] tips =
        {
            "获取资源地址...",
            "下载高清资源...",
            "资源解压缩，此过程不需要消耗流量...",
            "检查资源完整性...",
            "准备开始游戏..."
        };
        for (int i=0;i<5;i++)
        {
            TweenUtil.ChangeSliderValueTo(slider, slider.value + 0.2f, 0.5f);
            info.text = tips[i];
            yield return globalData.waitForSeconds[0];
        }
        SceneManager.LoadScene("Main");
        gameMgr.gameObject.AddComponent<NetWorkMgr>();
    }
}
