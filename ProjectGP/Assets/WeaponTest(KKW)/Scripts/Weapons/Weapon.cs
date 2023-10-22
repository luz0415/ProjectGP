using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxBullet;     // �ִ� �Ѿ� ��
    protected float currentBullet;        // ���� ���� �Ѿ� ��
    public float fireDamp;      // ���� ���� �ð�
    protected float currentDamp;          // ���� ���꿡 ���� �����ð�
    public float reloadTime;    // �������� �ɸ��� �ð�
    protected bool isReload = false;      // ���������ΰ�?

    public GameObject bullet;   // �Ѿ� ������
    public Transform firePos;   // �ѱ� ��ġ
    public GameObject player;   // �÷��̾�
    public TestPlayer testPlayer;

    // �� SFX ����
    public AudioClip fireSFX;
    protected AudioSource source = null;

    // �ѱ� ȭ�� ����
    public ParticleSystem muzzleFlash;
    protected WFX_LightFlicker _light;

    public bool isArmed = false;

    // MachineArms(������ �ӵ� 20% ����)
    public void DecreaseReloadTime(float ratio)
    {
        reloadTime *= ratio;
    }

    // PlasteelMagazine(źâUP)
    public void IncreseMaxBullet(float ratio)
    {
        maxBullet = (int)(maxBullet * 1.2f);
    }

    // SuperDrink(���� 20%����)
    public void DecreaseFireDamp(float ratio)
    {
        ratio = 2 - ratio;
        fireDamp *= 0.8f;
    }
}
