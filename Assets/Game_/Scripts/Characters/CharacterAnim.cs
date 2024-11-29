using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    [SerializeField] Character character;

    string animName = "Idle";

    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            character.anim.ResetTrigger(this.animName);
            this.animName = animName;
            character.anim.SetTrigger(this.animName);
        }
    }

    public void ResetAnim()
    {
        ChangeAnim("Idle");
    }
}
