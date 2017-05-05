using UnityEngine;
using System.Collections;

namespace Omni.BaseLibs
{
	public class GeneralUtilities {

		public static string GetSubString(string str, char cSeparator, int iIndex){

			string[] strElements = str.Split(cSeparator);
			string result = "";
			for(int j = iIndex; j < strElements.Length; j++){

				if(j > iIndex){

					result += '_';
				}
				
				result += strElements[j];
			}

			return result;
		}

		public  static string ReverseString(string strToReverse){

			string strResult = "";

			for(int i = strToReverse.Length; i-- >= 1;){

				strResult += strToReverse[i];
			}

			return strResult;
		}

		public static string ExtractFileName(string strPath, char cSeparator){

			strPath = ReverseString(strPath);

			string[] strPathElements = strPath.Split(cSeparator);
			

			return 0 < strPathElements.Length? ReverseString(strPathElements[0]): "";
			
		}

		public static string ExtractPath(string strPath, char cSeparator){

			strPath = ReverseString(strPath);

			string[] strPathElements = strPath.Split(cSeparator);
			string strResult = "";

			for(int i = 1; i < strPathElements.Length; i++){
				
				strResult += strPathElements[i] + (i + 1 < strPathElements.Length? "" + cSeparator: "");
			}

			strResult = ReverseString(strResult);
			return strResult;
		}

		public static string[] GetStrings(string strText, char cSeparator){

			return strText.Split(cSeparator);
		}

		public static Transform FindTransformChild(Transform tParent, string strName){

			
			for(int i = 0; i < tParent.childCount; i++){

				if(tParent.GetChild(i).name == strName){

					return tParent.GetChild(i);
				}
				else{

					Transform tResult = FindTransformChild(tParent.GetChild(i), strName);

					if(null != tResult){

						return tResult;
					}
				}
			}

			return null;
		}

		public static Transform FindParent(Transform tChild, string strParentName){

			if(null != tChild.parent){
				
				if(tChild.parent.name == strParentName){

					return tChild.parent;
				}
				else{

					Transform tParent = FindParent(tChild.parent, strParentName);

					if(null != tParent){

						return tParent;
					}				
				}
			}

			return null;
		}

		public static float SqrDistance(Vector3 vFrom, Vector3 vTo, Vector3 vAxisToCheckForReach){

			vFrom = new Vector3(vAxisToCheckForReach.x != 0? vFrom.x: 0,
			                                  vAxisToCheckForReach.y != 0? vFrom.y: 0,
			                                  vAxisToCheckForReach.z != 0? vFrom.z: 0
			                                  );

			vTo = new Vector3(vAxisToCheckForReach.x != 0? vTo.x: 0,
			                                  vAxisToCheckForReach.y != 0? vTo.y: 0,
			                                  vAxisToCheckForReach.z != 0? vTo.z: 0
			                                  );

			return (vTo - vFrom).sqrMagnitude;
		}

		public enum ComponentActivationOption{

			kDontModifyOthersOfBaseType,
			kActivateOthersOfBaseType,
			kDeactivateOthersOfBaseType
		};
		
		public 	static MonoBehaviour ActivateComponent(Transform tTransform, System.Type type, bool bEnabled, GeneralUtilities  .ComponentActivationOption kOtherOption){

			// first deactivate other components of the same base clase if desired
			if(GeneralUtilities.ComponentActivationOption.kDeactivateOthersOfBaseType == kOtherOption){

				Component[] componentsOfBaseType = tTransform.GetComponents(type.BaseType);

				for(int i = 0; i < componentsOfBaseType.Length; i++){
		
					((MonoBehaviour)componentsOfBaseType[i]).enabled = false;
				}			
			}
			
			// deactivate all camera scripts
			Component[] componentsOfType = tTransform.GetComponents(type);

			MonoBehaviour returnComponent = null;
			
			for(int i = 0; i < componentsOfType.Length; i++){

				((MonoBehaviour)componentsOfType[i]).enabled = bEnabled;			

				returnComponent = (MonoBehaviour)componentsOfType[i];
			}
			
			return returnComponent;
			
		}

		public static Vector3 GetBoundExtents(Collider otherCollider){

			string strColliderType = otherCollider.ToString();

			// first try to get the bounds based on the 3d model
			/*
			if(null != otherCollider.GetComponent(typeof(MeshFilter)) && null != ((MeshFilter)otherCollider.GetComponent(typeof(MeshFilter))).mesh){

				return ((MeshFilter)otherCollider.GetComponent(typeof(MeshFilter))).mesh.bounds.extents;
			}*/		

			Vector3 vExtents = otherCollider.bounds.extents;

			// check for each type
			if(typeof(BoxCollider).ToString() == strColliderType){

				vExtents =  ((BoxCollider)otherCollider).size / 2;
				vExtents.Scale(otherCollider.transform.lossyScale);
				return vExtents;
			}

			// check for each type
			if(typeof(SphereCollider).ToString() == strColliderType){

				float fRadious = ((SphereCollider)otherCollider).radius;
				vExtents = new Vector3(fRadious, fRadious, fRadious);
				vExtents.Scale(otherCollider.transform.lossyScale);
				return vExtents;
			}

			vExtents.Scale(otherCollider.transform.lossyScale);
			return vExtents;
		}
		

		public static bool CheckRayCast(ref RaycastHit hitInfo, Ray ray, LayerMask layers){

			// ray cast from
			return Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layers);
		}

		//----------------------- Swap -------------------------------------------
		//
		//  used to swap two values
		//------------------------------------------------------------------------
		public static void Swap<T>(T a, T b)	{
			
			T temp = a;
			a = b;
			b = temp;		
		}

		public static GameObject InstantiatePrefab(string strPrefabPath, Transform tParent){

			Object prefab = Resources.Load(strPrefabPath);

			GameObject instantiatedGameObject = null;
			
			if(null != prefab){

				instantiatedGameObject = (GameObject)Object.Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));

				if(null != tParent){

					instantiatedGameObject.transform.parent = tParent;

					instantiatedGameObject.transform.localPosition = Vector3.zero;
				}
			}
			else{

				Debug.LogError("Couldn't load prefab: " + strPrefabPath);
			}

			return instantiatedGameObject;
		}

		public static Component GetComponentFromPrefab(string strPrefabPath, Transform tParent, System.Type componentType){

			GameObject instantiatedGameObject = GeneralUtilities.InstantiatePrefab(strPrefabPath, tParent);

			if(null != instantiatedGameObject){

				return instantiatedGameObject.GetComponent(componentType);
			}

			return null;
		}

		public static string GetFormattedTime(float fTime, bool bHours, bool bMinutes, bool bSeconds, bool bMiliseconds){

			string strResult;

			int iMiliseconds = bMiliseconds? (int)((fTime - (int)fTime) * 1000) : 0;
			int iSeconds = bSeconds?  (int)fTime % 60: 0;		
			int iMinutes = bMinutes? (int)fTime / 60 : 0;
			int iHours = bHours? (int)fTime / (60 * 60) : 0;

			strResult = (bHours? (10 > iHours? "0": "" ) + iHours.ToString() + ":": "") +
							(bMinutes? (10 > iMinutes? "0": "" ) + iMinutes.ToString(): "") +
							(bSeconds? (10 > iSeconds? ":0": ":" ) + iSeconds.ToString(): "") +
							(bMiliseconds?  (100 > iMiliseconds? ".0": "." ) + iMiliseconds.ToString(): "");

			return strResult;
		
		}

		public static float GetSeconds(string strFormattedTime){

			int iMilliseconds = 0;
			System.Int32.TryParse (ExtractFileName (strFormattedTime, '.'), out iMilliseconds);
			
			string strRemaingFormattedTime = ExtractPath (strFormattedTime, '.');
			
			int iSeconds = 0;
			System.Int32.TryParse (ExtractFileName (strRemaingFormattedTime, ':'), out iSeconds);

			strRemaingFormattedTime = ExtractPath (strFormattedTime, ':');

			int iMinutes = 0;
			System.Int32.TryParse (ExtractFileName (strRemaingFormattedTime, ':'), out iMinutes);


			strRemaingFormattedTime = ExtractPath (strRemaingFormattedTime, ':');

			int iHours = 0;
			System.Int32.TryParse (strRemaingFormattedTime, out iHours);

			return iHours * 60 * 60 + iMinutes * 60 + iSeconds + iMilliseconds / 1000.0F;	
		}

		public static T RandomEnum<T>()
		{ 
			T[] values = (T[]) System.Enum.GetValues(typeof(T));
			return values[UnityEngine.Random.Range(0, values.Length)];
		}
	}
}