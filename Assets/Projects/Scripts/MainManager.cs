using KirinUtil;
using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    // DllImport
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    // vars
    [Header("Script")]
    public ProcessManager processManager;

    [Header("Object")]
    public GameObject btnParentObj;
    public GameObject btnPrefab;
    public GameObject settingUI;
    public GameObject settingBgObj;
    public GameObject settingWindow;
    public Text settingInputText;

    // vars
    private List<string> titleList;
    private List<Process> processList;
    private Timer refreshTimer;
    private bool isClosingSettingWindow;

    //----------------------------------
    //  init/update
    //----------------------------------
    #region init/update
    void Start()
    {
        settingUI.SetActive(false);
        isClosingSettingWindow = false;
        Init();
    }

    private void Init()
    {
        titleList = new List<string>();
        processList = new List<Process>();
        refreshTimer = new Timer();
        refreshTimer.LimitTime = 1;

        GetUnityProcess();
        CreateKillBtn();
    }

    private void Update()
    {
        if (refreshTimer.Update())
        {
            Init();
        }
    }
    #endregion

    //----------------------------------
    //  Setting
    //----------------------------------
    #region Setting
    public void SettingBtnClick()
    {
        settingInputText.text = PlayerPrefs.GetString("unityHubPath", "");
        settingUI.SetActive(true);

        Util.media.FadeInUI(settingBgObj, 0.5f, 0);

        Util.media.FadeInUI(settingWindow, 0.3f, 0);
        Util.Scale(settingWindow, 0.2f);
        iTween.ScaleTo(settingWindow,
            iTween.Hash(
                "x", 1,
                "y", 1,
                "time", 0.4f,
                "islocal", true,
                "EaseType", iTween.EaseType.easeOutExpo
            )
        );
    }

    public void SettingCloseBtnClick()
    {
        if (isClosingSettingWindow) return;
        StartCoroutine(ClosedSettingWindowWait());
    }

    private IEnumerator ClosedSettingWindowWait()
    {
        isClosingSettingWindow = true;
        Util.media.FadeOutUI(settingBgObj, 0.3f, 0);
        Util.media.FadeOutUI(settingWindow, 0.2f, 0);

        yield return new WaitForSeconds(0.4f);

        settingUI.SetActive(false);
        isClosingSettingWindow = false;
    }

    public void SettingInputBtnClick()
    {
        var extensions = new[] {
            new ExtensionFilter("Exe Files", "exe" )
        };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);

        if (paths == null || paths.Length == 0 || paths[0] == "") return;
        settingInputText.text = paths[0];

        PlayerPrefs.SetString("unityHubPath", paths[0]);
    }

    public void SettingEraseBtnClick()
    {
        PlayerPrefs.SetString("unityHubPath", "");
        settingInputText.text = "";
    }
    #endregion

    //----------------------------------
    //  Kill Unity
    //----------------------------------
    #region Kill Unity
    // Create Unity Button
    private void CreateKillBtn()
    {
        Util.media.DeleteAllGameObject(btnParentObj, false);

        for (int i = 0; i < titleList.Count; i++)
        {
            GameObject btnObj = Util.media.CreateUIObj(btnPrefab, btnParentObj, "btn" + i, new Vector3(0, -i * 60, 0), Vector3.zero, Vector3.one);
            btnObj.GetComponentInChildren<Text>().text = titleList[i];
            int num = i;
            btnObj.GetComponent<Button>().onClick.AddListener(() => KillBtnClick(num));
        }
    }

    // process
    private void GetUnityProcess()
    {
        //IDictionary<IntPtr, string> test = OpenWindowGetter.GetOpenWindows();
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
            IntPtr handle = window.Key;
            string title = window.Value;

            if (title.IndexOf("Unity") != -1)
            {
                int processId;
                GetWindowThreadProcessId(handle, out processId);
                Process p = Process.GetProcessById(processId);

                if (p.ProcessName == "Unity")
                {
                    titleList.Add(title);
                    processList.Add(p);

                }
            }

        }
    }

    private void KillBtnClick(int btnNum)
    {
        KillProccess(btnNum);
        RunUnityHub();

        Init();
    }

    private void KillProccess(int listNum)
    {
        try
        {
            processList[listNum].CloseMainWindow();
            processList[listNum].Dispose();
        }
        catch (Exception ex)
        {
            print("ExitApp Error: " + ex.Message);
        }
    }
    #endregion

    #region Run UnityHub
    private void RunUnityHub()
    {
        string unityHubPath = PlayerPrefs.GetString("unityHubPath", "");
        if (unityHubPath == "") return;
        if (!File.Exists(unityHubPath)) return;

        processManager.RunApp(unityHubPath, false);
    }
    #endregion

}
