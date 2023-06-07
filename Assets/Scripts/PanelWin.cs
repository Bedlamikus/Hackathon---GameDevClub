using TMPro;
using UnityEngine;

public class PanelWin : MonoBehaviour
{
    [SerializeField] private TMP_Text coins;
    public int CountCoins;

    public void SetRandomCoins()
    {
        int i = Random.Range(0, (FindObjectOfType<GameManager>().currentCycle + 1) * 100);
        coins.text = "+" + i.ToString();
        CountCoins = i;
    }
}
