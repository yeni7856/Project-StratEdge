using UnityEngine;

namespace MyStartEdge
{
    public class BuildManager : MonoBehaviour
    {
        #region Singleton
        public static BuildManager instance;
        public static BuildManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Variables
        //Ÿ�Ͽ� ��ġ�� ĳ��������(������, ��������)
        //private TurretBlueprint turretToBuild;

        //������ ĳ���Ͱ� �ִ���, �������� �������� ��ġ ���Ѵ�
        //public bool CannotBuild => turretToBuild == null;


        //������ ĳ���� ��ġ ����� ������ �ִ���
/*        public bool HasBuildMoney
        {
            get
            {
                if (turretToBuild == null)
                    return false;

                return PlayerStats.HasMoney(turretToBuild.cost);
            }
        }*/

        //Ÿ�� UI
        //public TileUI tileUI;
        //���õ� Ÿ��
        private Tile selectTile;
        #endregion

       /* public TurretBlueprint GetTurretToBuild()
        {
            return turretToBuild;
        }
*/
        //�Ű������� ���� �ͷ� �������� ��ġ�� �ͷ��� ����        
/*        public void SetTurretToBuild(TurretBlueprint turret)
        {
            turretToBuild = turret;

            //���õ� Ÿ�� �����ϱ�
            DeselectTile();
        }*/

        //�Ű������� ������ Ÿ�� ������ ���´�
        public void SelectTile(Tile tile)
        {
            //���� Ÿ���� �����ϸ� HideUI
            if (selectTile == tile)
            {
                DeselectTile();
                return;
            }

            //������ Ÿ�� �����ϱ�
            selectTile = tile;
            //����� ĳ���� �Ӽ��� �ʱ�ȭ
            //turretToBuild = null;

            //Debug.Log("Ÿ�� UI �����ֱ�");
            //tileUI.ShowTileUI(tile);
        }

        //���� ����
        public void DeselectTile()
        {
            //Debug.Log("Ÿ�� UI ���߱�");
            //tileUI.HideTileUI();

            //������ Ÿ�� �ʱ�ȭ �ϱ�
            selectTile = null;
        }

    }
}