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
        //타일에 설치할 캐릭터정보(프리팹, 가격정보)
        //private TurretBlueprint turretToBuild;

        //선택한 캐릭터가 있는지, 선택하지 안했으면 배치 못한다
        //public bool CannotBuild => turretToBuild == null;


        //선택한 캐릭터 배치 비용을 가지고 있는지
/*        public bool HasBuildMoney
        {
            get
            {
                if (turretToBuild == null)
                    return false;

                return PlayerStats.HasMoney(turretToBuild.cost);
            }
        }*/

        //타일 UI
        //public TileUI tileUI;
        //선택된 타일
        private Tile selectTile;
        #endregion

       /* public TurretBlueprint GetTurretToBuild()
        {
            return turretToBuild;
        }
*/
        //매개변수로 받은 터렛 프리팹을 설치할 터렛에 저장        
/*        public void SetTurretToBuild(TurretBlueprint turret)
        {
            turretToBuild = turret;

            //선택된 타일 해제하기
            DeselectTile();
        }*/

        //매개변수로 선택한 타일 정보를 얻어온다
        public void SelectTile(Tile tile)
        {
            //같은 타일을 선택하면 HideUI
            if (selectTile == tile)
            {
                DeselectTile();
                return;
            }

            //선택한 타일 저장하기
            selectTile = tile;
            //저장된 캐릭터 속성을 초기화
            //turretToBuild = null;

            //Debug.Log("타일 UI 보여주기");
            //tileUI.ShowTileUI(tile);
        }

        //선택 해제
        public void DeselectTile()
        {
            //Debug.Log("타일 UI 감추기");
            //tileUI.HideTileUI();

            //선택한 타일 초기화 하기
            selectTile = null;
        }

    }
}