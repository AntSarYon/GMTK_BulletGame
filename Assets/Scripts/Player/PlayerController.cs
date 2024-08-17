using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;
    public float sensitivity = 2f;
    public float minXRotation = -50f;
    public float maxXRotation = 50f;
    public float minYRotation = -10f;
    public float maxYRotation = 10f;

    private float mouseX, mouseY;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Update()
    {
        // Movimiento en el eje X (izquierda y derecha)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0f, 0f);

        // Rotación de la cámara según el movimiento del mouse
        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Limitar la rotación en el eje X
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación del objeto en el eje Y, con límite
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, minYRotation, maxYRotation);

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        ChangeGlass();
    }

    private void ChangeGlass()
    {
        switch (true)
        {
            case bool _ when Input.GetKeyDown(KeyCode.Z):
                ScalesManager.Instance.LensScaleChange(ProjectileScale.x1);
                break;

            case bool _ when Input.GetKeyDown(KeyCode.X):
                ScalesManager.Instance.LensScaleChange(ProjectileScale.x2);
                break;

            case bool _ when Input.GetKeyDown(KeyCode.C):
                ScalesManager.Instance.LensScaleChange(ProjectileScale.x3);
                break;
        }
    }
}
