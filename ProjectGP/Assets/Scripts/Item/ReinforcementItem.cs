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
    }

    private void StickyWheel(GameObject target)
    {
        // �̵� �ӵ� 20% ����
    }

    private void BioreactiveDefibrillator(GameObject target)
    {
        // ��Ȱ 1ȸ
    }

    private void MithrilBullet(GameObject target)
    {
       // ���� 1ȸ
    }

    private void PulseAmplifier(GameObject target)
    {
        // ȸ�Ǳ� ��Ÿ�� 20% ����
    }

    private void RoboticEye(GameObject target)
    {
        // �� �ӵ� 10% ����
    }

    private void PlasteelMagazine(GameObject target)
    {
        // ���� ���� źâ 20% ����
    }

    private void StealthModule(GameObject target)
    {
        // ȸ�Ǳ� ���� ���� ��� �ν� ����
    }

    private void VirtualCombatSimulation(GameObject target)
    {
        // ���� ���� �� 0.5�� ���� �÷��̾ ������.
    }

    private void SuperDrink(GameObject target)
    {
        // �ִ�ü�� -1, ���ݼӵ� 20% ����.
    }

    private void BiochemicalWeapon(GameObject target)
    {
        // �����ι� �����ι�.
    }
}
