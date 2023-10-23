using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReinforcementItem : MonoBehaviour, IItem 
{
    private string itemName;

    private void Start()
    {
        itemName = GetComponent<ItemInShop>().itemName;
    }

    public void Use(GameObject target)
    {
        switch (itemName)
        {
            case "�ӽ� ����":
                MachineArms(target);
                break;

            case "��ƼŰ ��":
                StickyWheel(target);
                break;

            case "��ü���� ��������":
                BioreactiveDefibrillator(target);
                break;

            case "�̽��� �Ѿ�":
                MithrilBullet(target);
                break;

            case "�޽� ������":
                PulseAmplifier(target);
                break;

            case "�κ�ƽ ����":
                RoboticEye(target);
                break;

            case "�ö�ƿ źâ":
                PlasteelMagazine(target);
                break;

            case "���ڽ� ���":
                StealthModule(target);
                break;

            case "���� ���� �ùķ��̼�":
                VirtualCombatSimulation(target);
                break;

            case "���� �帵ũ":
                SuperDrink(target);
                break;

            case "��ȭ���� ����":
                BiochemicalWeapon(target);
                break;

        }
        // public int �� ����� reinforcement�� ����
        // swtich case�� �Լ� ����������
    }

    private void MachineArms(GameObject target)
    {
        // ���� �ӵ� 20% ����
        WeaponManager.instance.DecreaseAllReloadTime(0.8f);
    }

    private void StickyWheel(GameObject target)
    {
        // �̵� �ӵ� 20% ����
        GameManager.instance.player.walkSpeed *= 1.2f;
    }

    private void BioreactiveDefibrillator(GameObject target)
    {
        // ��Ȱ 1ȸ
        PlayerHP playerHP = target.GetComponent<PlayerHP>();
        if(playerHP != null)
        {
            playerHP.canRevive = true;
        }
    }

    private void MithrilBullet(GameObject target)
    {
        // ���� 1ȸ
        WeaponManager.instance.IncreasePenetrate();
    }

    private void PulseAmplifier(GameObject target)
    {
        // ȸ�Ǳ� ��Ÿ�� 20% ����
        PlayerDodge playerDodge = target.GetComponent<PlayerDodge>();
        if (playerDodge != null)
        {
            playerDodge.dashCooldown *= 0.8f;
        }
    }

    private void RoboticEye(GameObject target)
    {
        // �� �ӵ� 10% ����
    }

    private void PlasteelMagazine(GameObject target)
    {
        // ���� źâ 20% ����
        WeaponManager.instance.DecreaseAllReloadTime(0.8f);
    }

    private void StealthModule(GameObject target)
    {
        // ȸ�Ǳ� ���� ���� ��� �ν� ����
        target.GetComponent<PlayerDodge>().hasStealthModule = true;
    }

    private void VirtualCombatSimulation(GameObject target)
    {
        // ���� ���� �� 0.5�� ���� �÷��̾ ������.
        GameManager.instance.hasVirtualCombatSimulation = true;
    }

    private void SuperDrink(GameObject target)
    {
        // �ִ�ü�� -1, ���ݼӵ� 20% ����.
        PlayerHP playerHP = target.GetComponent<PlayerHP>();
        if (playerHP != null)
        {
            playerHP.DecreaseStartHP(1);
        }
        WeaponManager.instance.DecreaseFireDamp(0.8f);
    }

    private void BiochemicalWeapon(GameObject target)
    {
        // �����ι� �����ι�.
        PlayerHP playerHP = target.GetComponent<PlayerHP>();
        if (playerHP != null)
        {
            playerHP.damageDouble = true;
        }
        WeaponManager.instance.IncreaseDamage();
    }
}
