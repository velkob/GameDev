using UnityEngine;

public class Movement : MonoBehaviour
{
    const int MAX_TILES = 6;
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private GameObject turnTile;

    private Vector3[] destination;

    private int tilesToMove;

    public bool isMoving;

    private void Start()
    {
        // direction = Vector3.forward;
        //destination = new Vector3[MAX_TILES];
    }
    void Update()
    {
        if (isMoving)
        {
            int tilesRolled = tilesToMove;
            while (tilesToMove > 0)
            {
                isMoving = true;
                Move(destination[tilesRolled - tilesToMove]);
                tilesToMove--;
            }

        }
    }

    public void StartMoving()
    {
        tilesToMove = Random.Range(MAX_TILES, MAX_TILES);
        EvaluateTilesToMove();
        isMoving = true;
    }

    private void Move(Vector3 destination)
    {
        while (isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, destination, Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, destination) < 0.001f)
            {
                transform.localPosition = destination;
                isMoving = false;
            }
            if (transform.localPosition == destination && CheckForTurn())
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);
                tilesToMove++;
                EvaluateTilesToMove();
            }
        }
    }

    private void EvaluateTilesToMove()
    {
        direction = 0.1f * transform.right;
        destination = new Vector3[tilesToMove];
        for (int i = 0; i < tilesToMove; i++)
        {
            destination[i] = transform.localPosition + (i + 1) * direction;
        }
    }

    private bool CheckForTurn()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit turnTileRayCast, 10);
        return turnTileRayCast.collider != null && turnTileRayCast.collider.CompareTag("TurnTile");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * 10);
    }
}
