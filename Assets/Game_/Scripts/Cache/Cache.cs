using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Character> dictCharacter = new Dictionary<Collider, Character>();
   
    public static Character GenCharacter(Collider collider)
    {
        if (!dictCharacter.ContainsKey(collider))
        {
            Character cha = collider.GetComponent<Character>();

            dictCharacter.Add(collider, cha);
        }

        return dictCharacter[collider];
    }

}
