using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private PlayerType type;
    public static NewBehaviourScript Instance;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void SetPlayerType(PlayerType type)
    {
        this.type = type;
    }
    public PlayerType GetPlayerType()
    {
        return type;
    }
}
