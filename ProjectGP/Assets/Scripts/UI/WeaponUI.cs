using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Sprite[] weaponImages; // 0~4 까지 0:귄총, 1:라이플, 2:샷건, 3:맨손

    public TextMeshProUGUI playerAmmoText;
    public Image curWeaponImage;

    //weaponmanager와 각각 무기들 스크립트 가져오기
    public WeaponManager weaponManager;

    private void LateUpdate()
    {
        if (weaponManager.weaponIndex == 0) // 권총 총알 표시
            playerAmmoText.text = weaponManager.weapons[0].currentBullet + " / " + weaponManager.weapons[0].maxBullet;
        if (weaponManager.weaponIndex == 1) // 라이플 총알 표시
            playerAmmoText.text = weaponManager.weapons[1].currentBullet + " / " + weaponManager.weapons[1].maxBullet;
        if (weaponManager.weaponIndex == 2) // 샷건 총알 표시
            playerAmmoText.text = weaponManager.weapons[2].currentBullet + " / " + weaponManager.weapons[2].maxBullet;
        if (weaponManager.weaponIndex == 3) // 유탄 발사기 총알 표시
            playerAmmoText.text = weaponManager.weapons[3].currentBullet + " / " + weaponManager.weapons[3].maxBullet;

        curWeaponImage.sprite = weaponImages[weaponManager.weaponIndex]; // 무기 이미지 표시
    }
}
