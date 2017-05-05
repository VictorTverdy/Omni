using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUIController : MonoBehaviour {

    public GameObject NotificationItem;
	public GameObject VerticalPanel;
    public string[] notificationText;

    private float timer;

    public float SpawnTime;

    void Start () {
        randomNotification();
    }

    void Update()
    {
        //Generate Random Notifications every "SpawnTime" seconds
        timer += Time.deltaTime;
        if (timer >= SpawnTime)
        {
            timer = 0;
            randomNotification();
        }

    }

    public void Hide()
    {
        Destroy(gameObject);
    }

    void randomNotification()
    {
        int max = notificationText.Length - 1;
        int rand = Mathf.RoundToInt(Random.Range(0, max));
        GameObject notItem = Instantiate(NotificationItem);
        Text notItemText = notItem.transform.FindChild("NotificationText").GetComponent<Text>();
        notItemText.text = notificationText[rand];
		notItem.transform.SetParent(VerticalPanel.transform, false);
    }
}
