using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class ShavingStandMode : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    [SerializeField] private Camera cam;

    public void Init(PlayerMovement player, Transform cameraPosition)
    {
        inputActions = player.InputActions;
        inputActions.Shaving.Grab.performed += HandleGrab;
        MoveCamera(cameraPosition);
    }

    private void MoveCamera(Transform target)
    {
        StartCoroutine(LerpObject(cam.transform, target, cam.transform));
    }

    private void HandleGrab(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
        {
            Debug.Log(hitInfo.transform.gameObject.name);
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

    void OnDisable()
    {
        inputActions.Shaving.Grab.performed -= HandleGrab;
    }

}
