using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private Button m_MoonyButton;
    [SerializeField]
    private Button m_SunnyButton;
    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private NewBehaviourScript script;

    public void ChooseMoony()
    {
        script.SetPlayerType(PlayerType.MOONY);
        sceneLoader.LoadMainScene();
    }

    public void Choose_Sunny()
    {
        script.SetPlayerType(PlayerType.SUNNY);
        sceneLoader.LoadMainScene();
    }
}
