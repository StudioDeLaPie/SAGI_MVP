using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsLvl1 : MonoBehaviour
{
    public GameObject activeTxt;
    public GameObject deactiveTxt;
    public GameObject cubeTxt; //Le texte au dessus du cube, on le détruit pour plus qu'il gène
 
    private Target target;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Target>();
        activeTxt.SetActive(false);
        deactiveTxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target.IsActivated)
        {
            activeTxt.SetActive(true);
            deactiveTxt.SetActive(false);
        }
        else
        {
            activeTxt.SetActive(false);
            deactiveTxt.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Cube>() != null && other.transform.root.GetComponentInChildren<RectTransform>() != null && cubeTxt != null)
            Destroy(cubeTxt);
    }
}
