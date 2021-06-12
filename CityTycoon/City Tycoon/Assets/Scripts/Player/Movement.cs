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

    private void TurnAround()
    {
        //To be implemented
    }

    private void StopMoving()
    {
        tilesToMove = 0;
        isMoving = false;
    }

    private void Move10Tiles()
    {
        tilesToMove = 10;
        EvaluateTilesToMove();
    }

    public void StartMoving()
    {
        tilesToMove = Random.Range(1, MAX_TILES);
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PowerUpSign"))
        {
            transform.localPosition = destination[tilesRolled - tilesToMove];
            Sign powerUpSign = collider.GetComponent<PowerUpInfo>().GetSign();
            switch (powerUpSign)
            {
                case Sign.Move10Tiles:
                    Move10Tiles();
                    break;
                case Sign.Stop:
                    StopMoving();
                    break;
                case Sign.TurnAround:
                    TurnAround();
                    break;
                default:
                    break;
            }
            Destroy(collider.gameObject);
        }
    }
}
