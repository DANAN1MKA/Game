using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    [SerializeField] private int amountCenter;
    [SerializeField] private int amountTransition;
    [SerializeField] private GameObject start;
    private GameObject Center1;
    private GameObject Center2;
    private GameObject Transition;

    private Transform jointFromStartToCenter;
    private Transform jointFromCenterToTransition;

    void Awake()
    {
        //получаем расположение выхода из центра
        jointFromStartToCenter = GameObject.Find("Start-1/Exit").GetComponent<Transform>();

        //Загружаем случайный центральный префаб
        string str1 = "Center-" + Random.Range(1, amountCenter + 1);
        Center1 = (GameObject)Instantiate(Resources.Load(str1, typeof(GameObject)));
        Center1.transform.position = jointFromStartToCenter.position;

        //получаем координаты выхода из центрального префаба
        jointFromCenterToTransition = Center1.gameObject.transform.GetChild(0).GetComponent<Transform>();

        string str2 = "Center-" + Random.Range(1, amountCenter + 1);
        Center2 = (GameObject)Instantiate(Resources.Load(str2, typeof(GameObject)));
        Center2.transform.position = jointFromCenterToTransition.position;

        jointFromCenterToTransition = Center2.gameObject.transform.GetChild(0).GetComponent<Transform>();



        //загружаем случайный конечный префаб
        string str3 = "Transition-" + Random.Range(1, amountTransition + 1);
        Transition = (GameObject)Instantiate(Resources.Load(str3, typeof(GameObject)));
        Transition.transform.position = jointFromCenterToTransition.position;
    }
}
