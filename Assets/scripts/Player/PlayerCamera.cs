using FishNet.Object;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
   

    public override void OnStartClient()
    {
        if (!IsOwner)
            gameObject.SetActive(false);
    }

    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mousex;

        xRotation += mousey;

        xRotation = Mathf.Clamp(xRotation   , -90f, 90f);

        transform.rotation = Quaternion.Euler(-xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
