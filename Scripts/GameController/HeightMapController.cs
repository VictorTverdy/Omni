using cakeslice;
using Omni.CameraCtrl;
using Omni.Entities;
using Omni.Singletons;
using Omni.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Omni.GameState
{
    public class HeightMapController : MonoBehaviour
    {           
        public Material OutlineMat;

		public Transform m_parentRegisterPoint;
		public Transform m_endPositionTransform;
		public Transform m_startPositionTransform;

        public GameObject WellInfoDefaultPrefab;
        public GameObject WellInfoDrillingPrefab;
        public GameObject WellInfoWorkoverPrefab;
        public GameObject WellInfoAbandonedPrefab;

		public CameraHeightInputControl m_cameraHeightInputControl;

        [SerializeField]private bool useNewOutlineSystem = false;//not ready yet.

        public void Start()
        {
            GenerateRandWell();
          	//GenerateConfigurationWell();
            if(useNewOutlineSystem)m_parentRegisterPoint.transform.Rotate(new Vector3(0,0,1));//need this because it needs a little of rotation to be displayed well (all borders).
        }

        private void GenerateConfigurationWell()
        {
            int lenght = UserDataSettings.Instance.AssetList.Length;
            for (int i = 0; i < lenght; i++)
            {
                AssetList _assetList = UserDataSettings.Instance.AssetList[i];

                GameObject plane = new GameObject();
                plane.name = "PlaneHolder";
                plane.transform.parent = m_parentRegisterPoint;
                plane.transform.localPosition = new Vector3(_assetList.posX, _assetList.posY, _assetList.posZ);
                plane.transform.LookAt(m_parentRegisterPoint);
                plane.tag = "InteractivePad";

                float lastHeight = 0f;
                float lastPosition = 0f;
                float height = Random.Range(1, 3);

                lastHeight = height;
                List<float> sizes = new List<float>();
                List<Renderer> cubeRenderers = new List<Renderer>();

                GameObject cubeHolder = new GameObject();
                cubeHolder.name = "CubeHolder";
                cubeHolder.tag = "InteractiveField";
                cubeHolder.transform.parent = plane.transform;
                cubeHolder.transform.localPosition = Vector3.zero;
                cubeHolder.transform.localEulerAngles = new Vector3(-90,0,0);

                for (int k = 0; k <= 2; k++)
                {
                    float rndHeight = Random.Range(1, lastHeight);
                    sizes.Add(rndHeight);

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    Destroy(cube.GetComponent<Collider>());

                    cube.transform.parent = cubeHolder.transform;
                    cube.transform.localPosition = new Vector3(0, lastPosition + rndHeight, 0);                   
                    cube.transform.localScale = new Vector3(1, rndHeight, 1);

                    Renderer renderer = cube.GetComponent<Renderer>();
                    cubeRenderers.Add(renderer);
                    Color randColor = Color.red;

                    switch (k)
                    {
                        case 0:
                            randColor = Color.blue;
                            break;
                        case 1:
                            randColor = Color.green;
                            break;
                        case 2:
                            randColor = new Color(1, 0.5f, 0);
                            break;
                    }
                    renderer.material.color = randColor;
                    renderer.material.EnableKeyword("_EMISSION");
                    renderer.material.SetColor("_EmissionColor", randColor / 3);

                    lastPosition = lastPosition + rndHeight * 2;
                    lastHeight = height - rndHeight;
                }

                BoxCollider boxCol = cubeHolder.AddComponent<BoxCollider>();

                Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);    

                Transform[] allDescendants = cubeHolder.GetComponentsInChildren<Transform>();
                foreach (Transform desc in allDescendants)
                {
                    desc.localEulerAngles = Vector3.zero;
                       Renderer childRenderer = desc.GetComponent<Renderer>();
                    if (childRenderer != null)
                    {
                        bounds.Encapsulate(childRenderer.bounds);
                    }
                    boxCol.size = bounds.size;
                }

                boxCol.center = Vector3.zero;
                boxCol.size = new Vector3(2, 10, 2);
            }
        }

        private void GenerateRandWell()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    GameObject plane = new GameObject();
                    plane.name = "PlaneHolder";
                    plane.transform.parent = m_parentRegisterPoint;
                    plane.transform.localPosition = new Vector3(i * 10, 2.5F, j * 10);
                    plane.tag = "InteractivePad";
                    bool createCubes = Random.Range(0, 10) >= 7 ? true : false;

                    EnumWellType wellType = EnumWellType.None;

                    if (createCubes)
                    {
                        int count = 0;
                        int numberOfPoint = Random.Range(1, 2);

                        while (numberOfPoint >= count++)
                        {

                            float lastHeight = 0f;
                            float lastPosition = 0f;
                            float posX = Random.Range(-4, 4);
                            float posY = Random.Range(-4, 4);
                            float height = Random.Range(2, 10);

                            lastHeight = height;
                            List<float> sizes = new List<float>();
                            List<Renderer> cubeRenderers = new List<Renderer>();

                            GameObject cubeHolder = new GameObject();
                            cubeHolder.name = "CubeHolder";
							cubeHolder.tag = "InteractiveField";
                            cubeHolder.transform.parent = plane.transform;
                            cubeHolder.transform.localPosition = new Vector3(posX, 0, posY);

                            OutlineDisplay oD = cubeHolder.AddComponent<OutlineDisplay>();
                            oD.enabled = false;
                            if(useNewOutlineSystem) { 
							    oD.enabled = true;
                                oD.displayWhenMouseOver = true;
                                oD.lOutline = new List<Outline>();
                            }

                            bool isWellProducing = Random.Range(0, 10) >= 4 ? true : false;

                            if (isWellProducing)
                            {
                                wellType = EnumWellType.Default;
                                for (int k = 0; k <= 2; k++)
                                {
                                    float rndHeight = Random.Range(0, lastHeight);
                                    sizes.Add(rndHeight);

                                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                                    Destroy(cube.GetComponent<Collider>());

                                    cube.transform.parent = cubeHolder.transform;
                                    cube.transform.localPosition = new Vector3(0, lastPosition + rndHeight, 0);

                                    cube.transform.localScale = new Vector3(2, rndHeight, 2);

                                    Renderer renderer = cube.GetComponent<Renderer>();
                                    cubeRenderers.Add(renderer);
                                    Color randColor = Color.red;
                                     
                                    switch (k)
                                    {
                                        case 0:
                                            randColor = Color.blue;
                                            break;
                                        case 1:
                                            randColor = Color.green;
                                            break;
                                        case 2:
                                            randColor = new Color(1,0.5f,0);
                                            break;
                                    }
                                    renderer.material.color = randColor;
                                    renderer.material.EnableKeyword("_EMISSION");
                                    renderer.material.SetColor("_EmissionColor", randColor / 3);

                                    lastPosition = lastPosition + rndHeight * 2;
                                    lastHeight = height - rndHeight;

                                    if(useNewOutlineSystem) {
                                        Outline ou = cube.AddComponent<Outline>();
                                        ou.enabled = false;
                                        oD.lOutline.Add(ou);
                                    }
                                }
                            }
                            else
                            {
                                bool isAbandoned = Random.Range(0, 10) > 6 ? true : false;

                                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
								Destroy(cube.GetComponent<Collider>());
                                sizes.Add(height);
                                cube.transform.parent = cubeHolder.transform;
                                cube.transform.localPosition = new Vector3(0, height, 0);

                                cube.transform.localScale = new Vector3(2, height, 2);

                                Renderer renderer = cube.GetComponent<Renderer>();
                                cubeRenderers.Add(renderer);
                                Color randColor;

                                if (isAbandoned)
                                {
                                    randColor = Color.grey;
                                    wellType = EnumWellType.Abandoned;
                                }else
                                {
                                    bool isWorkover = Random.Range(0, 10) > 6 ? true : false;
                                    if(isWorkover) {
                                        randColor = new Color(128/255f,0,128/255f,1);
                                        wellType = EnumWellType.WorkOver;
                                    }else {
                                        randColor = Color.yellow;
                                        wellType = EnumWellType.Drilling;
                                    }
                                }
                                
                                renderer.material.color = randColor;
                                renderer.material.EnableKeyword("_EMISSION");
                                renderer.material.SetColor("_EmissionColor", randColor / 3);
                                
                                if(useNewOutlineSystem) {
                                    Outline ou = cube.AddComponent<Outline>();
                                    ou.enabled = false;
                                    oD.lOutline.Add(ou);
                                }
                            }

                            BoxCollider boxCol = cubeHolder.AddComponent<BoxCollider>();                         

							Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
							boxCol.center = cubeHolder.transform.position;
							boxCol.size = bounds.size;

							Transform[] allDescendants = cubeHolder.GetComponentsInChildren<Transform>();
							foreach (Transform desc in allDescendants)
							{
								Renderer childRenderer = desc.GetComponent<Renderer>();
								if (childRenderer != null)
								{
								   bounds.Encapsulate(childRenderer.bounds);
								}
								boxCol.center = bounds.center - cubeHolder.transform.position;
								boxCol.size = bounds.size;
							}

							boxCol.center = new Vector3 (0, boxCol.center.y, 0);
							boxCol.size = new Vector3 (2, boxCol.size.y, 2);

                            
                            //Well UI
                            WellBarController wellBar = cubeHolder.AddComponent<WellBarController>();
                            WellInfo wellInfo = cubeHolder.AddComponent<WellInfo>();
                            GameObject wellPanel = null;//jiji
                            switch (wellType)
                            {
                                case EnumWellType.None:
                                    break;
                                case EnumWellType.Default:
                                    wellPanel = Instantiate(WellInfoDefaultPrefab);
                                    wellBar.wellType = EnumWellType.Default;
                                    break;
                                case EnumWellType.Drilling:
                                    wellPanel = Instantiate(WellInfoDrillingPrefab);
                                    wellBar.wellType = EnumWellType.Drilling;
                                    break;
                                case EnumWellType.WorkOver:
                                    wellPanel = Instantiate(WellInfoWorkoverPrefab);
                                    wellBar.wellType = EnumWellType.WorkOver;
                                    break;
                                case EnumWellType.Abandoned:
                                    wellPanel = Instantiate(WellInfoAbandonedPrefab);
                                    wellBar.wellType = EnumWellType.Abandoned;
                                    break;
                                default:
                                    break;
                            }                                                        

                            if (isWellProducing)
                            {
                                wellInfo.gas = Mathf.RoundToInt(sizes[2] * 1000).ToString();
                                wellInfo.water = Mathf.RoundToInt(sizes[1] * 1000).ToString();
                                wellInfo.oil = Mathf.RoundToInt(sizes[0] * 1000).ToString();
                            }
                            else
                            {
                                wellInfo.gas = "";
                                wellInfo.water = "";
                                wellInfo.oil = "";
								wellInfo.depth = (height * 234.3).ToString();
                            }

                            wellPanel.transform.parent = cubeHolder.transform;
                            float offset = 10;
                            wellPanel.transform.localPosition = new Vector3(0, boxCol.size.y + offset, 0);
                            wellPanel.transform.localScale = Vector3.one * 0.1f;
                            wellPanel.gameObject.SetActive(false);  
                            
                            //Outline
                            if(!useNewOutlineSystem) {
                                GameObject cubeOutline = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                                cubeOutline.name = "cubeOutline";
                                cubeOutline.transform.parent = cubeHolder.transform;

                                float CubeSize = 0;
                                foreach (float size in sizes)
                                {
                                    CubeSize += size;
                                }

                                cubeOutline.transform.localPosition = new Vector3(0, CubeSize, 0);
                                cubeOutline.transform.localScale = new Vector3(2, CubeSize, 2);
                                Renderer OutlineRenderer = cubeOutline.GetComponent<Renderer>();
                                OutlineRenderer.material = OutlineMat;                       
                                cubeOutline.SetActive(false);

                                wellBar.MainPanel = wellPanel;
                                wellBar.BarOutline = cubeOutline;
                                wellBar.BarRenderer = cubeRenderers;
                                wellBar.highlightColor = new Color(0.25f, 0.25f, 0.25f);
							    wellBar.CameraTransform = m_cameraHeightInputControl.gameObject.transform;
                            }
                        }
                    }
                }
            }
        }

    }
}