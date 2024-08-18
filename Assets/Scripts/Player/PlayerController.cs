using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayerController : MonoBehaviour
{
    //Referencia a C�mara
    private Transform cameraMain;

    private Rigidbody mRb;
    private AudioSource mAudioSource;

    private Vector2 mDirection;
    private Vector2 mDeltaLook;

    [Header("Velocidad de Movimiento")]
    [SerializeField] private float movementSpeed;

    [Header("Velocidad de Rotaci�n")]
    [SerializeField] private float turnSpeed;

    //--------------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia al componente RigidBody
        mRb = GetComponent<Rigidbody>();
    }

    //--------------------------------------------------------------------

    void Start()
    {
        //Obtenemos referencia a la Camara Principal (Vista de jugador)
        cameraMain = transform.Find("CameraHandler").Find("Main Camera");

        //Bloqueamos el Cursor para que este no sea visible
        Cursor.lockState = CursorLockMode.Locked;
    }

    //--------------------------------------------------------------------------------
    #region InputActions
    private void OnMove(InputValue value)
    {
        //Obtenemos Direccion de movimiento (en un Vector2, con X e Y)
        mDirection = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        //Obtenemos el Vector2 generado por la rotaci�n del Raton
        mDeltaLook = value.Get<Vector2>();
    }

    #endregion


    //--------------------------------------------------------------------------------------------

    private void MoverseConFuerza()
    {

        //Aplicamos una fuerza al RB del Player para moverlo
        mRb.AddForce(
            (mDirection.y * transform.forward + mDirection.x * transform.right).normalized * movementSpeed,
            ForceMode.Force
            );

    }

    //--------------------------------------------------------------------------------------------

    private void ControlSpeed()
    {
        Vector3 flatVelocity = new Vector3(mRb.velocity.x, 0, mRb.velocity.z);

        //Limitamos la velocidad dentro del limite
        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 velocidadLimitada = flatVelocity.normalized * movementSpeed;
            mRb.velocity = new Vector3(velocidadLimitada.x, mRb.velocity.y, velocidadLimitada.z);
        }
    }

    //--------------------------------------------------------------------------------------------

    private void ControlarRotacion()
    {
        //Actualizamos constantemente la rotaci�n horizontal del Player en torno al Eje Y
        transform.Rotate(
            Vector3.up,
            turnSpeed * Time.deltaTime * mDeltaLook.x
        );

        ////Actualizamos constantemente la rotaci�n vertical del Player en torno al Eje X
        cameraMain.GetComponent<CameraMovement>().RotateUpDown(
            -turnSpeed * Time.deltaTime * mDeltaLook.y
        );
    }

    //----------------------------------------------------------------------

    void FixedUpdate()
    {
        MoverseConFuerza();
    }

    //----------------------------------------------------------------------

    void Update()
    {
        //Manejamos los estados para controlar el movimiento que se realizar�

        ControlSpeed();

        ControlarRotacion();

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            ScalesManager.Instance.LensScaleChanged(scrollInput);
        }
    }
}
