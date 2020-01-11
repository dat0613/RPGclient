using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using UnityEngine.UI;

public static class MinNetDebug
{
    private static GameObject Content = null;
    private static LogText logTextPrefab = null;
    private static ScrollRect scrollRect = null;
    private static System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(ScrollDown));

    public static void FindContent()
    {
        scrollRect = GameObject.Find("ScrollView").GetComponent<ScrollRect>();
        Content = GameObject.Find("Content");
        logTextPrefab = ((GameObject)Resources.Load("TextView")).GetComponent<LogText>();

        if(scrollRect == null || Content == null || logTextPrefab == null)
        {
            Debug.Log("ASDF");
        }
    }

    private static void ScrollDown(System.Object state)
    {
        scrollRect.normalizedPosition = Vector2.zero;
    }

    public static void Log(string str)
    {
        var log = GameObject.Instantiate(logTextPrefab, Content.transform);
        
        log.SetText(str);

        ScrollDown(null);

        // timer.Change(10, System.Threading.Timeout.Infinite);
    }
}