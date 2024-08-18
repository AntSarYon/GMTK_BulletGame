using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    [Header("Velocidad de movimiento")]
    public float moveSpeed = 5f;

    [Header("Transform de Camara")]
    public Transform cameraTransform;

    [Header("Sensibilidad de �camara")]
    public float sensitivity = 2f;

    [Header("Ajustes de Rotaci�n - X")]
    public float minXRotation = -50f;
    public float maxXRotation = 50f;

    [Header("Ajustes de Rotaci�n - Y")]
    //public float minYRotation = -10f;
    //public float maxYRotation = 10f;

    private float mouseX, mouseY;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Update()
    {
        // Movimiento en el eje X (izquierda y derecha)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        // Movimiento en el eje Z (izquierda y derecha)
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 CompleteSpeed = new Vector3(moveX, 0f, moveZ);
        CompleteSpeed = CompleteSpeed.normalized;

        //Traslado en el Eje X
        transform.Translate(CompleteSpeed * moveSpeed * Time.deltaTime);

        // Rotaci�n de la c�mara seg�n el movimiento del mouse
        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Limitar la rotaci�n en el eje X
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotaci�n del objeto en el eje Y, con l�mite
        yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, minYRotation, maxYRotation);

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            ScalesManager.Instance.LensScaleChanged(scrollInput);
        }
    }
}
