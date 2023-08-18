using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerShooting : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    private WeaponManager weaponManager;

    private PlayerWeapon currentWeapon;

    [SerializeField] private LayerMask mask;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        weaponManager = GetComponent<WeaponManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (currentWeapon.shootRate <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.shootRate);
            } else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }

        
    }

    private void Shoot()
    {
        Debug.Log("Shoot !!!!");
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.Range, mask))
        {
            if (hit.collider.tag == PLAYER_TAG)
            {
                ShootServerRpc(hit.transform.name, currentWeapon.Damage);
            }
        }
    }

    [ServerRpc]
    private void ShootServerRpc(string hittedName, int damage)
    {
        var player = GameManager.instance.GetPlayer(hittedName);
        player.TakeDamage(damage);
    }
}
