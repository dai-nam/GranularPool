using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    [SerializeField] Camera uiCamera;


    class GameViewCoordinates
    {
        internal static Vector3 gameCameraPosition = new Vector3(20, 115, 0);
        internal static Vector2 gameCameraRectXY = new Vector2(0, 0);
        internal static Vector2 gameCameraRectWH = new Vector2(1, 1);
    }

    class UiViewCoordinates
    {
        internal static Vector3 uiCameraPosition = new Vector3(5050, 20, -500);
        internal static Vector2 uiCameraRectXY = new Vector2(0, 0);
        internal static Vector2 uiCameraRectWH = new Vector2(1, 1);
    }

    class SplitViewCoordinates
    {
        internal static Vector3 gameCameraPosition = new Vector3(-20, 215, 0);
        internal static Vector2 gameCameraRectXY = new Vector2(0, 0);
        internal static Vector2 gameCameraRectWH = new Vector2(0.5f, 1);

        internal static Vector3 uiCameraPosition = new Vector3(5170, 20, -840);
        internal static Vector2 uiCameraRectXY = new Vector2(0.5f, 0);
        internal static Vector2 uiCameraRectWH = new Vector2(0.5f, 1);
    }


    public void SetGameViewCamera()
    {
        gameCamera.gameObject.SetActive(true);
        uiCamera.gameObject.SetActive(false);
        gameCamera.transform.position = GameViewCoordinates.gameCameraPosition;
        gameCamera.rect = new Rect(GameViewCoordinates.gameCameraRectXY, GameViewCoordinates.gameCameraRectWH);
    }


    public void SetUiViewCamera()
    {
        gameCamera.gameObject.SetActive(false);
        uiCamera.gameObject.SetActive(true);
        uiCamera.transform.position = UiViewCoordinates.uiCameraPosition;
        uiCamera.rect = new Rect(UiViewCoordinates.uiCameraRectXY, UiViewCoordinates.uiCameraRectWH);
    }

    public void SetSplitViewCamera()
    {
        gameCamera.gameObject.SetActive(true);
        uiCamera.gameObject.SetActive(true);
        gameCamera.transform.position = SplitViewCoordinates.gameCameraPosition;
        gameCamera.rect = new Rect(SplitViewCoordinates.gameCameraRectXY, SplitViewCoordinates.gameCameraRectWH);
        uiCamera.transform.position = SplitViewCoordinates.uiCameraPosition;
        uiCamera.rect = new Rect(SplitViewCoordinates.uiCameraRectXY, SplitViewCoordinates.uiCameraRectWH);
    }

}
