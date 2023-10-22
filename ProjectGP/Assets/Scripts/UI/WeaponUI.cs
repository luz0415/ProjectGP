using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Sprite[] weaponImages; // 0~4 ���� 0:����, 1:������, 2:����, 3:�Ǽ�

    public TextMeshProUGUI playerAmmoText;
    public Image curWeaponImage;

    //weaponmanager�� ���� ����� ��ũ��Ʈ ��������
    public WeaponManager weaponManager;

    private void LateUpdate()
    {
        if (weaponManager.weaponIndex == 0) // ���� �Ѿ� ǥ��
            playerAmmoText.text = weaponManager.weapons[0].currentBullet + " / " + weaponManager.weapons[0].maxBullet;
        if (weaponManager.weaponIndex == 1) // ������ �Ѿ� ǥ��
            playerAmmoText.text = weaponManager.weapons[1].currentBullet + " / " + weaponManager.weapons[1].maxBullet;
        if (weaponManager.weaponIndex == 2) // ���� �Ѿ� ǥ��
            playerAmmoText.text = weaponManager.weapons[2].currentBullet + " / " + weaponManager.weapons[2].maxBullet;
        if (weaponManager.weaponIndex == 3) // ��ź �߻�� �Ѿ� ǥ��
            playerAmmoText.text = weaponManager.weapons[3].currentBullet + " / " + weaponManager.weapons[3].maxBullet;

        curWeaponImage.sprite = weaponImages[weaponManager.weaponIndex]; // ���� �̹��� ǥ��
    }
}
