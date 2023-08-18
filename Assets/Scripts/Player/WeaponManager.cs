using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField] private PlayerWeapon primaryWeapon;

    [SerializeField] private PlayerWeapon secondaryWeapon;

    [SerializeField] private GameObject weaponHolder;

    private PlayerWeapon currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    public void EquipWeapon(PlayerWeapon weapon)
    {
        currentWeapon = weapon;
        while (weaponHolder.transform.childCount > 0)
        {
            DestroyImmediate(weaponHolder.transform.GetChild(0).gameObject);
        }
        var weaponObject = Instantiate(currentWeapon.graphics, weaponHolder.transform.position, weaponHolder.transform.rotation);
        weaponObject.transform.SetParent(weaponHolder.transform);
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void ToggleWeapon()
    {
        if (currentWeapon == primaryWeapon)
        {
            EquipWeapon(secondaryWeapon);
        }
        else
        {
            EquipWeapon(primaryWeapon);
        }
    }

    [ClientRpc]
    private void ToggleWeaponClientRpc()
    {
        ToggleWeapon();
    }

    [ServerRpc]
    private void ToggleWeaponServerRpc()
    {
        if (IsHost)
        {
            ToggleWeapon();
            return;
        }
        ToggleWeapon();
        ToggleWeaponClientRpc();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWeaponServerRpc();
        }
    }
}
