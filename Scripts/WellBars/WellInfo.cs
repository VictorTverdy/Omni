using UnityEngine;
using UnityEngine.UI;
public class WellInfo : MonoBehaviour {

    public string wellName;
    public string gas, oil, water, depth;
    public Text gasNum, oilNum, wellDepth, waterNum, wellNam;

    void Start () {
        //this is now in WellInfoPanelDisplay.cs -> setText();
        /*string WellNumber = string.Format("{0:0000}", 1000 * Random.value);

        if (gasNum)gasNum.text = string.Format("{0:0,0}", gas);
        if(oilNum) oilNum.text = string.Format("{0:0,0}", oil);
        if (waterNum) waterNum.text = string.Format("{0:0,0}", water);
        if (wellNam) wellNam.text = string.Format("WELL HB{0}", WellNumber);
		if(wellDepth) wellDepth.text = decimal.Parse(depth).ToString("N") + " mts";
        */
    }

}
