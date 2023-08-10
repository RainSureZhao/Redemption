using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentToDisable;

    private Camera sceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        if(!IsLocalPlayer)
        {
            DisableComponents();
           
        } else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
        SetPlayerName();
    }

    private void SetPlayerName()
    {
        transform.name = "Player" + GetComponent<NetworkObject>().NetworkObjectId;
    }
    
    private void DisableComponents()
    {
        foreach (var component in componentToDisable)
        {
            component.enabled = false;
        }
    }
    private void OnDisable()
    {
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
