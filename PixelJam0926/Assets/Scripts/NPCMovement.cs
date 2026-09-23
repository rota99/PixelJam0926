using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float waitTime = 2f; // Quanto tempo aspettare su ogni punto (in secondi)
    
    public List<Transform> points; 
    
    private Rigidbody2D rb;
    private int currentPointIndex = 0;
    
    private float waitTimer = 0f; // Tiene traccia del tempo trascorso fermi
    private int patrolDirection = 1; // 1 significa marcia avanti, -1 marcia indietro

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // Se non ci sono punti assegnati, fermati
        if (points == null || points.Count == 0) return;

        // SEZIONE PAUSA: Se il timer è maggiore di 0, stiamo aspettando
        if (waitTimer > 0)
        {
            waitTimer -= Time.fixedDeltaTime; // Sottrae il tempo trascorso
            return; // Interrompe il metodo qui, quindi l'NPC non si muove
        }

        // Troviamo la posizione del punto target attuale
        Vector2 targetPosition = points[currentPointIndex].position;

        // Ci muoviamo verso il bersaglio
        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // SEZIONE ARRIVO AL PUNTO
        if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            // 1. Inizia la pausa
            waitTimer = waitTime;

            // Se c'è solo un punto, non ha senso calcolare il prossimo
            if (points.Count <= 1) return;

            // 2. Gestione del "ping-pong" (inversione di marcia)
            if (currentPointIndex >= points.Count - 1)
            {
                // Siamo arrivati all'ultimo punto, invertiamo la marcia verso i punti precedenti
                patrolDirection = -1; 
            }
            else if (currentPointIndex <= 0)
            {
                // Siamo tornati al primo punto, invertiamo la marcia in avanti
                patrolDirection = 1; 
            }

            // 3. Imposta il prossimo punto usando la direzione (+1 o -1)
            currentPointIndex += patrolDirection;
        }
    }
}