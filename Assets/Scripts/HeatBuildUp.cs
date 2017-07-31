using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeatBuildUp : MonoBehaviour
{
    [SerializeField] int coolPoint = 275;
    [SerializeField] int overHeatPoint = 675;
    float temperature = 120;
    public Text outPut;

    public GameObject light;

    void Start()
    {
        StartCoroutine(HeatClimb());
    }

    public void Vent()
    {
        StopCoroutine(HeatClimb());
        StartCoroutine(CoolDown());
    }

    IEnumerator HeatClimb()
    {
        string text = "";

        while (temperature <= overHeatPoint)
        {
            temperature += 1f;
            text = "Temperature \n" + temperature.ToString() + "\t\t° C";

            outPut.text = text;

            yield return new WaitForSeconds(0.09f);
        }
        text = "Temperature \n" + temperature.ToString() + "\t\t°C \nOverHeated";
        outPut.text = text;
        light.GetComponent<Animation>().Play();
        //light.GetComponent<AudioSource>().Play();
    }

    IEnumerator CoolDown()
    {
        light.GetComponent<Animation>().Stop();
        //light.GetComponent<AudioSource>().Stop();
        string text = "";
        while (temperature >= coolPoint)
        {
            temperature -= 3f;

            text = "Temperature \n" + temperature.ToString() + "\t\t°C";
            outPut.text = text;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);

        StartCoroutine(HeatClimb());
    }
}
