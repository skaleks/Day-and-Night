using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly string HORIZONTAL = "Horizontal";
    private readonly string STAR = "Star";
    private readonly string GLARE = "Glare";
    private readonly string GROUND = "Ground";
    private readonly string DEATHZONE = "DeathZone";

    private bool onGround = true;
    private bool canMove = true;
    private Rigidbody moonyRB;
    private float horizontal;
    private float xLimit = 8f;
    private Vector3 startPosition = new Vector3(0, 1.72f, 1);
    private int countLife = 3;
    private PlayerType type;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _power;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private UIHandler _uiHandler;

    private void Start()
    {
        moonyRB = GetComponent<Rigidbody>();

        type = NewBehaviourScript.Instance.GetPlayerType();
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(STAR))
        {
            if (type.Equals(PlayerType.MOONY))
            {
                Destroy(other.gameObject);
                _uiHandler.AddPoints(10);
            }
            else
            {
                Fall();
            }
        }

        if (other.CompareTag(GLARE))
        {
            if (type.Equals(PlayerType.SUNNY))
            {
                Destroy(other.gameObject);
                _uiHandler.AddPoints(10);
            }
            else
            {
                Fall();
            }
        }
        if (other.CompareTag(DEATHZONE))
        {
            Fall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GROUND))
        {
            onGround = true;
        }
    }

    private void Move()
    {
        horizontal = Input.GetAxis(HORIZONTAL);

        transform.Translate(transform.right * horizontal * _speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow) && onGround)
        {
            moonyRB.AddForce(transform.up * _power, ForceMode.Impulse);

            onGround = false;
        }
    }

    private void Fall()
    {
        countLife--;
        transform.position = startPosition;
        _uiHandler.LifeMinus();

        if(countLife == 0)
        {
            canMove = false;
            _gameManager.isPlayerAlive = false;
            _uiHandler.ShowGameOver();
        }
    }
}
