using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour {

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;

    public enum ViewModes { DEFAULT };
    public static ViewModes viewMode { get; set; }

    private Transform  charactor;
    private Quaternion m_CameraTargetRot;
    private Quaternion m_CharacterTargetRot;
    private Camera     m_Camera;
    private MouseLook m_MouseLook;
    private bool m_cursorIsLocked = true;



    // Use this for initialization
    void Start ()
    {
        viewMode = ViewModes.DEFAULT;

        charactor = transform.parent;
        m_CharacterTargetRot = charactor.localRotation;
        m_CameraTargetRot = transform.localRotation;
        m_Camera = GetComponent<Camera>();
        m_MouseLook = new MouseLook();
        m_MouseLook.Init(charactor, m_Camera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        InputListener.listenMouseXY();

        m_MouseLook.LookRotation(charactor, m_Camera.transform);
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
