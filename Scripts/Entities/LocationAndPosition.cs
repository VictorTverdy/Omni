using System;
using UnityEngine;
using Omni.Utilities;

namespace Omni.Entities
{
	public class LocationAndPosition
	{		
        public bool m_isFromBack { get; set; }
		public Transform m_transformPosition { get; set; }
		public InsideFieldLocation m_location { get; set; }

		public LocationAndPosition (InsideFieldLocation location, Transform transformPosition = null)
		{
            m_isFromBack = false;
            m_location = location;
			m_transformPosition = transformPosition;
		}
	}
}
