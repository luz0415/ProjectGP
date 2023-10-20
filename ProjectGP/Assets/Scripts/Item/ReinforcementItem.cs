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
            case "머신 암즈":
                MachineArms(target);
                break;

            case "스티키 휠":
                StickyWheel(target);
                break;

            case "생체반응 제세동기":
                BioreactiveDefibrillator(target);
                break;

            case "미스릴 총알":
                MithrilBullet(target);
                break;

            case "펄스 증폭기":
                PulseAmplifier(target);
                break;

            case "로보틱 아이":
                RoboticEye(target);
                break;

            case "플라스틸 탄창":
                PlasteelMagazine(target);
                break;

            case "스텔스 모듈":
                StealthModule(target);
                break;

            case "가상 전투 시뮬레이션":
                VirtualCombatSimulation(target);
                break;

            case "슈퍼 드링크":
                SuperDrink(target);
                break;

            case "생화학적 무기":
                BiochemicalWeapon(target);
                break;

        }
        // public int 로 사용할 reinforcement를 고르고
        // swtich case로 함수 ㄱㄱㄱㄱㄱ
    }

    private void MachineArms(GameObject target)
    {
        // 장전 속도 20% 증가
    }

    private void StickyWheel(GameObject target)
    {
        // 이동 속도 20% 증가
    }

    private void BioreactiveDefibrillator(GameObject target)
    {
        // 부활 1회
    }

    private void MithrilBullet(GameObject target)
    {
       // 관통 1회
    }

    private void PulseAmplifier(GameObject target)
    {
        // 회피기 쿨타임 20% 감소
    }

    private void RoboticEye(GameObject target)
    {
        // 적 속도 10% 감소
    }

    private void PlasteelMagazine(GameObject target)
    {
        // 현재 무기 탄창 20% 증가
    }

    private void StealthModule(GameObject target)
    {
        // 회피기 사용시 적이 잠시 인식 못함
    }

    private void VirtualCombatSimulation(GameObject target)
    {
        // 전투 시작 후 0.5초 동안 플레이어만 움직임.
    }

    private void SuperDrink(GameObject target)
    {
        // 최대체력 -1, 공격속도 20% 증가.
    }

    private void BiochemicalWeapon(GameObject target)
    {
        // 적도두배 나도두배.
    }
}
