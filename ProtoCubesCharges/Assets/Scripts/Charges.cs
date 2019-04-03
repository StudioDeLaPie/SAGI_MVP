using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charges : MonoBehaviour
{

    [SerializeField] private int chargesNegatives;
    [SerializeField] private int poidsSansCharges = 2;
    [SerializeField] private int maxCharges = 4;

    /// <summary>
    /// Nombre de charges négatives.
    /// </summary>
    public int CurrentCharge
    {
        get { return chargesNegatives; }
    }

    public int MaxCharges
    {
        get { return maxCharges; }
    }

    /// <summary>
    /// Poids actuel. (Poids sans charges - nbCharges)
    /// </summary>
    public int CurrentPoids
    {
        get { return poidsSansCharges - chargesNegatives; }
    }

    public void AjoutChargeNegative()
    {
        if (chargesNegatives < maxCharges)
            chargesNegatives++;
    }

    public void RetraitChargeNegative()
    {
        if (chargesNegatives > 0)
            chargesNegatives--;
    }
}
