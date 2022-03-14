using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRankLineController : MonoBehaviour
{
    [SerializeField]
    Text txtRank, txtName, txtPoint;

    public void Init(int rank, string name, string point)
    {
        this.txtRank.text = rank + "";
        this.txtName.text = name;
        this.txtPoint.text = point;
    }
}
