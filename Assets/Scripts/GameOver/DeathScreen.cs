using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    EventInstance creatureAttack;

    private void Start()
    {
        creatureAttack = FMODUnity.RuntimeManager.CreateInstance("event:/CreatureAttack");
    }

    private void OnDestroy()
    {
        creatureAttack.release();
    }

    public void PlayGoreSFX()
    {
        creatureAttack.start();
    }

    public void SetGameOver()
    {
        GameOver.Instance.SetTo(true);
    }
}
