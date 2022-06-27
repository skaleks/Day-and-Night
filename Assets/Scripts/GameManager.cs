using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_star_prefab;
    [SerializeField]
    private GameObject m_glare_prefab;
    [SerializeField]
    private GameObject PlayerMoony;
    [SerializeField]
    private GameObject PlayerSunny;
    [SerializeField]
    private UIHandler uIHandler;
   

    private float xLimit = 8f;
    [SerializeField]
    private float speed;
    public bool isPlayerAlive = true;
    private bool canChangeEnvironment = true;
    private int time = 15;
    private string TimeOfDAy;
    private string Night = "Night";
    private string Day = "Day";

    private void PickASide()
    {
        var type = NewBehaviourScript.Instance.GetPlayerType();

        if (type.Equals(PlayerType.MOONY))
        {
            PlayerMoony.SetActive(true);
            TimeOfDAy = Night;
        }
        if(type.Equals(PlayerType.SUNNY))
        {
            PlayerSunny.SetActive(true);
            TimeOfDAy = Day;
        }
    }

    private void Awake()
    {
        PickASide();
    }
    void Start()
    {
        InvokeRepeating(nameof(SpawnInvoker), 0.5f, 1f);
    }

    private void Update()
    {
        if (canChangeEnvironment)
        {
            StartCoroutine(nameof(ChangeAfterTime));
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isPlayerAlive)
        {
            SceneLoader.Instance.Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoader.Instance.BackToMenu();
        }
    }

    private void SpawnInvoker()
    {
        var type = NewBehaviourScript.Instance.GetPlayerType();

        if (type.Equals(PlayerType.MOONY))
        {

            if (TimeOfDAy.Equals(Night))
            {
                StartCoroutine(SpawnStarsOrGlares(m_star_prefab, 2));
            }
            else
            {
                StartCoroutine(SpawnStarsOrGlares(m_glare_prefab, 0.1f));
            }
            
        }
        else
        {
            if (TimeOfDAy.Equals(Day))
            {
                StartCoroutine(SpawnStarsOrGlares(m_glare_prefab, 2));
            }
            else
            {
                StartCoroutine(SpawnStarsOrGlares(m_star_prefab, 0.1f));
            }
        }
        
    }

    public IEnumerator SpawnStarsOrGlares(GameObject obj, float seconds)
    {
        if (isPlayerAlive)
        {
            yield return new WaitForSeconds(seconds);

            Vector3 location = new Vector3(Random.Range(-xLimit, xLimit), 12f, 1f);

            Instantiate<GameObject>(obj, location, Quaternion.identity);
        }
    }

    private IEnumerator ChangeAfterTime()
    {
        canChangeEnvironment = false;

        if (isPlayerAlive)
        {
            uIHandler.ShowTimer();
        }

        yield return new WaitForSeconds(17);

        if (TimeOfDAy.Equals(Night))
        {
            TimeOfDAy = Day;
        }
        else
        {
            TimeOfDAy = Night;
        }

        uIHandler.ChangeEnvironment(TimeOfDAy);
        canChangeEnvironment = true;
    }
}
