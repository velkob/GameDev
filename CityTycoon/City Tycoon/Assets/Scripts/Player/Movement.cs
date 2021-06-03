using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    int MAX_TILES = 6;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private GameObject turnTile;

    private Vector3[] destination;

    private int tilesToMove;
    private int tilesRolled;

    public bool isMoving;

    void Update()
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, destination[tilesRolled - tilesToMove], 30f * Time.deltaTime);
            
            if (Vector3.Distance(transform.localPosition, destination[tilesRolled - tilesToMove]) < 0.01f)
            {
                transform.localPosition = destination[tilesRolled - tilesToMove];

                if (CheckForTurn())
                {
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);
                    EvaluateTilesToMove();
                    tilesToMove++;
                }

                tilesToMove--;

                if (tilesToMove == 0)
                {
                    isMoving = false;
                }
            }
        }
    }

    public void StartMoving()
    {
        tilesToMove = Random.Range(MAX_TILES, MAX_TILES);
        EvaluateTilesToMove();
        isMoving = true;
    }

    private void EvaluateTilesToMove()
    {
        direction = transform.right;
        destination = new Vector3[tilesToMove];
        tilesRolled = tilesToMove;

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
}
