using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float runSpeed = 5.0f;
    public float horizontalTurnSpeed = 180f;
    public float verticalTurnSpeed = 2.5f;
    public bool headVerticalInverted = false;
    private const float VERTICAL_LIMIT = 60f;
    public bool allowHeadCamera = true;
    public bool allowVerticalHeadCamera = false;
    public bool invertedRunTrigger = false;

    //private void OnGUI()
    //{
    //    Player player = Player.instance;
    //    if (!player)
    //    {
    //        return;
    //    }

    //    //new SteamVR_Input_ActionSet_default().MovementAction;
    //    SteamVR_Input_ActionSet_default touchpad = new SteamVR_Input_ActionSet_default();


    //    if (null != player.leftHand)
    //    {
    //        var touchPadVector = touchpad.MovementAction.GetAxis(SteamVR_Input_Sources.LeftHand);
    //        GUILayout.Label(string.Format("Left X: {0:F2}, {1:F2}", touchPadVector.x, touchPadVector.y));
    //    }

    //    if (null != player.rightHand)
    //    {
    //        var touchPadVector = touchpad.MovementAction.GetAxis(SteamVR_Input_Sources.RightHand);
    //        GUILayout.Label(string.Format("Right X: {0:F2}, {1:F2}", touchPadVector.x, touchPadVector.y));
    //    }
    //}

    float GetAngle(float input)
    {
        if (input < 0f)
        {
            return -Mathf.LerpAngle(0, VERTICAL_LIMIT, -input);
        }
        else if (input > 0f)
        {
            return Mathf.LerpAngle(0, VERTICAL_LIMIT, input);
        }
        return 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Player player = Player.instance;
        if (!player)
        {
            return;
        }

        //EVRButtonId touchPad = EVRButtonId.k_EButton_SteamVR_Touchpad;
        SteamVR_Input_ActionSet_default touchpad = new SteamVR_Input_ActionSet_default();
        SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
        if (null != player.leftHand)
        {
            Quaternion orientation = Camera.main.transform.rotation;
            var touchPadVector = touchpad.MovementAction.GetAxis(SteamVR_Input_Sources.LeftHand);
            Vector3 moveDirection = orientation * Vector3.forward * touchPadVector.y + orientation * Vector3.right * touchPadVector.x;
            Vector3 pos = player.transform.position;


            bool doRun = teleportAction.GetState(SteamVR_Input_Sources.LeftHand);
            if (invertedRunTrigger)
            {
                doRun = !doRun;
            }
            float adjustedMoveSpeed = moveSpeed;
            if (doRun)
            {
                adjustedMoveSpeed = runSpeed;
            }

            pos.x += moveDirection.x * adjustedMoveSpeed * Time.deltaTime;
            pos.z += moveDirection.z * adjustedMoveSpeed * Time.deltaTime;
            player.transform.position = pos;
        }

        if (allowHeadCamera)
        {
            if (null != player.rightHand)
            {
                Quaternion orientation = player.transform.rotation;
                var touchPadVector = touchpad.MovementAction.GetAxis(SteamVR_Input_Sources.RightHand);

                Vector3 euler = transform.rotation.eulerAngles;
                float angle = 0;
                if (allowVerticalHeadCamera)
                {
                    if (headVerticalInverted)
                    {
                        angle = GetAngle(touchPadVector.y);
                    }
                    else
                    {
                        angle = GetAngle(-touchPadVector.y);
                    }
                }
                euler.x = Mathf.LerpAngle(euler.x, angle, verticalTurnSpeed * Time.deltaTime);
                euler.y += touchPadVector.x * horizontalTurnSpeed * Time.deltaTime;
                player.transform.rotation = Quaternion.Euler(euler);
            }
        }
    }
}