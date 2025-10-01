using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShavingStandMode : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform camPosition;


    public void Init(PlayerMovement player, Transform cameraPosition)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        inputActions = player.InputActions;

        inputActions.Player.Disable();
        inputActions.Shaving.Enable();
        inputActions.Shaving.Grab.performed += HandleGrab;
        inputActions.Shaving.Exit.performed += ExitShavingMode;
        MoveCamera(cameraPosition);
    }

    private void MoveCamera(Transform target)
    {
        StartCoroutine(LerpObject(cam.transform, target, cam.transform));
    }

    private void ExitShavingMode(InputAction.CallbackContext context)
    {
        MoveCamera(camPosition);
        inputActions.Shaving.Disable();
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().Init();
    }

    private void HandleGrab(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IceBlock iceBlock))
            {
                iceBlock.Grab();
            }
        }
    }

    private IEnumerator LerpObject(Transform startTransform, Transform targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startTransform.position, targetPostion.position, currentTime / duration);
            obj.rotation = Quaternion.Lerp(startTransform.rotation, targetPostion.rotation, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        obj.position = targetPostion.position;
        obj.rotation = targetPostion.rotation;
    }

    private IEnumerator ResetCamera(Transform startTransform, Transform targetPostion, Transform obj)
    {
        float currentTime = 0f;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            obj.position = Vector3.Lerp(startTransform.position, targetPostion.position, currentTime / duration);
            obj.rotation = Quaternion.Lerp(startTransform.rotation, targetPostion.rotation, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        obj.position = targetPostion.position;
        obj.rotation = targetPostion.rotation;
    }

    void OnDisable()
    {
        inputActions.Shaving.Exit.performed -= ExitShavingMode;
    }

}
