using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : Singleton<MoneyManager>
{

    public int money = 0;
    [SerializeField]
    TextMeshProUGUI moneyAmmountText;

    private void Start()
    {
        UpdateMoneyVisual();
    }
    private void UpdateMoneyVisual()
    {
        moneyAmmountText.text = money.ToString();
    }

    public void addMoneyAmmount (int moneyAmmount)
    {
        money = money + moneyAmmount;
        UpdateMoneyVisual();
    }
    public void setMoneyAmmount(int newAmmount)
    {
        money = newAmmount;
        UpdateMoneyVisual();
    }



}
