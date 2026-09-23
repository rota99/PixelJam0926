using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] GameObject interactWarning;
    public GameObject message;
    
    private InputSystem_Actions playerControls;
    private bool isPlayerInRange = false;

    void Awake()
    {
        playerControls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        playerControls.Enable();
        // Colleghiamo il nostro metodo all'evento "performed" (tasto premuto)
        playerControls.Player.Interact.performed += OnInteract;
    }

    void OnDisable()
    {
        playerControls.Disable();
        // Scolleghiamo il metodo quando l'oggetto si spegne per evitare errori di memoria
        playerControls.Player.Interact.performed -= OnInteract;
    }

    // Questo metodo scatta da solo appena premi il tasto assegnato a "Interact"
    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Il sistema a eventi funziona!"); // Test in console

        if (isPlayerInRange)
        {
            message.SetActive(true);
            interactWarning.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactWarning.SetActive(true);
        }  
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactWarning.SetActive(false);
            message.SetActive(false); 
        }
    }
}