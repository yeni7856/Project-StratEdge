using UnityEngine;
using UnityEngine.EventSystems;

namespace MyStartEdge
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� ĳ���� ���ӿ�����Ʈ ��ü
        private GameObject turret;

        //���� ���õ� ĳ���� blueprint(prefab, cost, ....)
        //public TurretBlueprint blueprint;

        //����Ŵ��� ��ü
        //private BuildManager buildManager;

        //������ �ν��Ͻ�
        private Renderer rend;

        //���콺�� ���� ������ Ÿ�� �÷���
        public Color notEnoughColor;

        //���콺�� ���� ������ Ÿ�� ���͸���
        public Material hoverMaterial;

        //��Ÿ���� �⺻ Material
        private Material startMaterial;

        //ĳ���� ��ġ ����Ʈ ������
        public GameObject buildEffectPrefab;

        //ĳ���� �Ǹ� ����Ʈ ������
        public GameObject sellEffectPrefab;

        //ĳ���� ���׷��̵� ����
        public bool IsUpgrade { get; private set; }
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            //buildManager = BuildManager.Instance;

            //rend = this.transform.GetComponent<Renderer>();
            rend = this.GetComponent<Renderer>();
            rend.enabled = false;
            //startColor = rend.material.color;
            startMaterial = rend.material;
            IsUpgrade = false;
        }

        private void OnMouseEnter()
        {
            //���콺 �����Ͱ� UI���� ������
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            //ĳ���͸� ��ġ ���� ������
           /* if (buildManager.CannotBuild)
            {
                return;
            }*/

            rend.enabled = true;
            //rend.material.color = hoverColor;
            rend.material = hoverMaterial;

            //������ ĳ���� ��ġ ����� ������ �ִ��� �ܰ�Ȯ��
/*            if (buildManager.HasBuildMoney == false)
            {
                rend.material.color = notEnoughColor;
            }*/
        }

        private void OnMouseDown()
        {
            //���콺 �����Ͱ� UI���� ������
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (turret != null)
            {
                //Debug.Log("Ÿ�Ͽ� �ͷ��� ��ġ�Ǿ� ������ Ÿ�� UI �����ֱ�");
                //buildManager.SelectTile(this);        //Ÿ�Ͽ� ĳ���� ��ġ �Ǿ������� Ÿ��ui
                return;
            }

            /*if (buildManager.CannotBuild)
            {
                Debug.Log("�ͷ��� ��ġ���� ���߽��ϴ�"); //�ͷ��� �������� ���� ����
                return;
            }*/

            BuildTurret();
        }

        private void BuildTurret()
        {
            //��ġ�� ĳ������ �Ӽ��� �������� (ĳ���� ��ġ ������, ĳ���ͺ��, ���׷��̵� ������, ���׷��̵� ���.....)
            //blueprint = buildManager.GetTurretToBuild();

            //���� �����Ѵ� 100, 250
            //Debug.Log($"�ͷ� �Ǽ����: {blueprint.cost}");
           /* if (PlayerStats.UseMoney(blueprint.cost))
            {
                //ĳ���� ��ġ ����Ʈ
                GameObject effectGo = Instantiate(buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effectGo, 2f);

                //ĳ���� ��ġ
                turret = Instantiate(blueprint.turretPrefab, GetBuildPosition(), Quaternion.identity);
                //Debug.Log($"�Ǽ��ϰ� ������: {PlayerStats.Money}");
            }*/
        }

        //ĳ���� ���׷��̵�
        public void UpgradeTurret()
        {
            //��ġ�� ĳ������ �Ӽ��� üũ
/*            if (blueprint == null)
            {
                Debug.Log("���׷��̵忡 ���� �߽��ϴ�");
                return;
            }*/

            //Debug.Log("�ͷ� ���׷��̵�");
            /*if (PlayerStats.UseMoney(blueprint.upgradeCost))
            {
                //ĳ���� ��ġ ����Ʈ
                GameObject effectGo = Instantiate(buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effectGo, 2f);

                //���� �ͷ� ų
                Destroy(turret);

                //���׷��̵� ����
                IsUpgrade = true;

                //ĳ���� ��ġ
                turret = Instantiate(blueprint.turretUpgradePrefab, GetBuildPosition(), Quaternion.identity);
            }*/
        }

        public void SellTurret()
        {
            //�ǸŰ��� ����
            int sellMoney = blueprint.GetSellCost();

            //�ͷ��� ų
            Destroy(turret);

            //�Ǹ� ����Ʈ
            GameObject effectGo = Instantiate(sellEffectPrefab, GetBuildPosition(), Quaternion.identity);
            Destroy(effectGo, 2f);

            //�Ǽ� ���� �Ӽ� �ʱ�ȭ            
            turret = null;
            blueprint = null;
            IsUpgrade = false;

            //�ǸŰ��� ����
            PlayerStats.AddMoney(sellMoney);
        }

        //�ͷ� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            return this.transform.position + blueprint.offset;
        }

        private void OnMouseExit()
        {
            rend.enabled = false;
            //rend.material.color = startColor;
            rend.material = startMaterial;
        }
    }
}