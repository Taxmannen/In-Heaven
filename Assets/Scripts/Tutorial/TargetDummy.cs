using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made By: Vidar M
/// </summary>
public class TargetDummy : Character
{

    private int index;

    private void Start()
    {
        for (int i = 0; i < TutorialController.instance.shootDummyParent.childCount; i++)
        {
            if (transform == TutorialController.instance.shootDummyParent.GetChild(i))
            {
                index = i;
                //TutorialController.instance.CheckShootingGoal();
            }
        }
        if (index > 0)
        {
            gameObject.SetActive(false);

        }
    }

    internal override void Die()
    {
        base.Die();
        if (index < TutorialController.instance.shootDummyParent.childCount)
        {
            gameObject.SetActive(false);
            AudioController.instance.BossDestruction();
            if (index + 1 < TutorialController.instance.shootDummyParent.childCount)
            {
                TutorialController.instance.shootDummyParent.GetChild(index + 1).gameObject.SetActive(true);
            }
            TutorialController.instance.CheckShootingGoal();
        }


    }
    //private void HideGameObject()
    //{
    //    gameObject.GetComponent<MeshRenderer>().enabled = false;
    //    gameObject.GetComponent<BoxCollider>().enabled = false;
    //}
    //private void ShowGameObject()
    //{
    //    gameObject.GetComponent<MeshRenderer>().enabled = true;
    //    gameObject.GetComponent<BoxCollider>().enabled = true;
    //}
}
