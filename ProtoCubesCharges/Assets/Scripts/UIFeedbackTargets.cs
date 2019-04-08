using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFeedbackTargets : MonoBehaviour
{
    public Sprite targetSprite;
    public Color activatedTarget;
    public Color deactivatedTarget;
    public RectTransform parentPanel;

    private Teleporter teleporter;
    private Dictionary<Target, Image> targets = new Dictionary<Target, Image>();

    void Start()
    {
        teleporter = GameObject.Find("Teleporter").GetComponent<Teleporter>();
        teleporter.OnTargetUpdate += TargetUpdate;
        foreach(Target target in teleporter.targets)
        {
            targets.Add(target, UI_AddTarget());
        }

    }

    private void TargetUpdate()
    {
        foreach (KeyValuePair<Target, Image> pair in targets)
        {
            pair.Value.color = pair.Key.IsActivated ? activatedTarget : deactivatedTarget;
        }
    }

    private Image UI_AddTarget()
    {
        GameObject newObj = new GameObject(); //Create the GameObject
        Image newImage = newObj.AddComponent<Image>(); //Add the Image Component script
        newImage.sprite = targetSprite; //Set the Sprite of the Image Component on the new GameObject
        newImage.color = deactivatedTarget;
        newObj.GetComponent<RectTransform>().SetParent(parentPanel.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        newObj.SetActive(true); //Activate the GameObject

        return newImage;
    }
}
