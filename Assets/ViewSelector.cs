using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ViewSelector : MonoBehaviour
{
    public Views activeView;

    public delegate void OnSwitchToGameView();
    public OnSwitchToGameView SwitchToGameView;
    public delegate void OnSwitchToUiView();
    public OnSwitchToUiView SwitchToUiView;
    public delegate void OnSwitchToSplitView();
    public OnSwitchToSplitView SwitchToSplitView;

    private static ViewSelector _instance;
    public static ViewSelector Instance
    {
        get { return _instance; }
    }

    public enum Views
    {
        UiView, GameView, SplitView
    }

    private void Awake()
    {
        _instance = this;
        RegisterEvents();
        SwitchToSplitView?.Invoke();
    }

    private void RegisterEvents()
    {
        CameraController cc = FindObjectOfType<CameraController>();
        SwitchToGameView += cc.SetGameViewCamera;
        SwitchToUiView += cc.SetUiViewCamera;
        SwitchToSplitView += cc.SetSplitViewCamera;
    }

    private void Start()
    {
        activeView = Views.GameView;
        InvokeViewChange();
    }

    public void InvokeViewChange()
    {
        switch (activeView)
        {
            case Views.GameView:
                SwitchToGameView?.Invoke();
                break;
            case Views.UiView:
                SwitchToUiView?.Invoke();
                break;
            case Views.SplitView:
                SwitchToSplitView?.Invoke();
                break;
            default:
                break;
        }
    }
}
