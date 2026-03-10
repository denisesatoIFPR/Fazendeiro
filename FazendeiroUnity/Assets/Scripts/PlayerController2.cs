using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 20f;
    public float xRange = 15f;
    public GameObject projectilePrefab;
    public InputActionAsset InputActions;
    private InputAction moveAction;
    private InputAction fireAction;

    private InputAction pauseActionPlayer;
    private InputAction pauseActionUI;

    public GameObject PauseDisplay;


    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Jump");    
        pauseActionPlayer = InputSystem.actions.FindAction("Player/Pause");
        pauseActionUI = InputSystem.actions.FindAction("UI/Pause");
    }
    // Update is called once per frame
    void Update()
    {
        // movimenta o player para esquerda e direita a partir da entrada do usu�rio
        float horizontalInput = moveAction.ReadValue<Vector2>().x;
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);
        // mant�m o player dentro dos limites do jogo (eixo x)
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.y);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.y);
        }
        if (fireAction.WasPressedThisFrame())
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);    
        }        

        DisplayPause();
    }

    private void DisplayPause()
    {
        if (pauseActionPlayer.WasPerformedThisFrame())
        {
            PauseDisplay.SetActive(true);
            InputActions.FindActionMap("Player").Disable();
            InputActions.FindActionMap("UI").Enable();
        } else if (pauseActionUI.WasPerformedThisFrame())
        {
            PauseDisplay.SetActive(false);
            InputActions.FindActionMap("Player").Enable();
            InputActions.FindActionMap("UI").Disable();
        }
    }
}
