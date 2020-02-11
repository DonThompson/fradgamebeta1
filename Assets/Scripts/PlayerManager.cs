using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private Unit PlayerUnit;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            PlayerUnit = GetComponent<Unit>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Unit GetPlayerUnit()
    {
        return PlayerUnit;
    }

    public void UpdatePlayerUnit(Unit updatedPlayer)
    {
        PlayerUnit = updatedPlayer;
    }
}
