using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{

    public delegate void AspectChanged(Vector3 topRightCorner, Vector3 downLeftCorner);

    public static event AspectChanged OnChange;
   private Camera _camera;
    private int _lastScreenWidth;
    private int _lastScreenHeight;
    private Vector3 cameraPos;
    private Vector3 topRightCorner;
    private Vector3 downLeftCorner;
    public Vector3 TopRightCorner => topRightCorner;
    public Vector3 DownLeftCorner => downLeftCorner;
    private bool waitForUpdate;


    private void Awake()
    {
        StartCoroutine(WaitForScene());



    }
    IEnumerator WaitForScene()
    {
        GameManager.OnStartGame += SetupBoundaries;
        while (Camera.main==null)
        {
            waitForUpdate = true;
            yield return null;
        }
        
        _camera = Camera.main;
        cameraPos = _camera.transform.position;
        CacheScreenSize();
        SetupBoundaries();
        if (waitForUpdate)
        {
            waitForUpdate = false;
            OnChange?.Invoke(topRightCorner, downLeftCorner);
        }
       
   
    }
    

    private void OnDisable()
    {
        GameManager.OnStartGame -= SetupBoundaries;
      
    }

    private void Start()
    {
        if (!waitForUpdate)
        {
            OnChange?.Invoke(topRightCorner, downLeftCorner);
        }
    }

    private void Update()
    {
        if (waitForUpdate) return;
        if (!ScreenSizeChanged() && cameraPos == _camera.transform.position) return;
        CacheScreenSize();
        SetupBoundaries();
        OnChange?.Invoke(topRightCorner, downLeftCorner);
    }
    

    private void SetupBoundaries()
    {
        float z = -_camera.transform.position.z; // plano do gameplay

        downLeftCorner = _camera.ScreenToWorldPoint(new Vector3(0, 0, z));
        topRightCorner = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, z));
       
    }
    
    private bool ScreenSizeChanged()
    {
        return Screen.width != _lastScreenWidth ||
               Screen.height != _lastScreenHeight;
    }
    private void CacheScreenSize()
    {
        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;
    }


}

