using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour {

    public enum ViewModes { DEFAULT };
    public static ViewModes viewMode { get; set; }

    private Camera camera;
    private Transform character;
    private MouseLook mouseLook;
    
    // Use this for initialization
    void Start ()
    {
        viewMode = ViewModes.DEFAULT;
        camera = GetComponent<Camera>();
        character = transform.parent;
        mouseLook = new MouseLook();
    }

    // Update is called once per frame
    void Update()
    {
        mouseLook.LookRotation(character, camera.transform);
    }

    void Save()
    {
        ViewMode data = new ViewMode();
        data.viewMode = viewMode;
        GameIO.saveData(FileName.VIEW_MODE,data);
    }

    void Load()
    {
        ViewMode data = GameIO.loadData<ViewMode>(FileName.VIEW_MODE);
        viewMode = data.viewMode;
    }
}
