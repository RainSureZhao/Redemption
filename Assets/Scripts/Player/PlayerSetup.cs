using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentToDisable;

    private Camera sceneCamera;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsLocalPlayer)
        {
            DisableComponents();

        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

        var name = "Player" + GetComponent<NetworkObject>().NetworkObjectId.ToString();
        var player = GetComponent<Player>();
        player.Setup();
        player.transform.name = name;
        GameManager.instance.RegisterPlayer(name, player);
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        DisableComponents();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void DisableComponents()
    {
        foreach (var component in componentToDisable)
        {
            component.enabled = false;
        }

        GameManager.instance.UnRegisterPlayer(transform.name);
    }
}
