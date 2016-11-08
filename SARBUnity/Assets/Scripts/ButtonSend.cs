using UnityEngine;
using System.Collections;


public class ButtonSend : MonoBehaviour {

	public void sendStuff()
	{
        // Create some diversity in sending
        int rnd = Random.Range(1, 5);
		NetworkClient.instance.writeSocket(ChooseString(rnd));
	}

    private string ChooseString(int choice)
    {
        if (choice == 1)
            return "Hello world, I am a Client";

        else if (choice == 2)
            return "I am cool";

        else if (choice == 3)
            return "server works fine now";

        else if (choice == 4)
            return "HeightMap";

        else
            return "something else";
    }
}
