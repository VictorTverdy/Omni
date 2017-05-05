using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Omni.UI
{
    public class OpenReportsUrls : MonoBehaviour
    {

        public void ListenerOpenReport0()
        {
            Application.OpenURL("http://www.sgs-me.com/~/media/Global/Documents/Flyers%20and%20Leaflets/SGS-OGC-FluidPro-Leaflet-A4-web-LR.pdf");
        }

        public void ListenerOpenReport1()
        {
            Application.OpenURL("http://www.sgs-me.com/~/media/Global/Documents/Brochures/SGS-OGC-RDK-Brochure-A4-web-LR.pdf");
        }

        public void ListenerOpenReport2()
        {
            Application.OpenURL("https://www.youtube.com/watch?v=5diKdBZ8EOI");

        }

        public void ListenerOpenReport3()
        {
            Application.OpenURL("https://docs.unity3d.com/ScriptReference/Application.OpenURL");
        }
    }

}
