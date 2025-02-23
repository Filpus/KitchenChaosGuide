using System;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }
    }
}
