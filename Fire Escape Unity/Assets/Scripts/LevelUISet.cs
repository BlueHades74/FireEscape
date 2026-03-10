using TMPro;
using UnityEngine;

public class LevelUISet : MonoBehaviour
{
    [SerializeField]
    private GameObject dataStorage;
    private LevelInfo data;

    [SerializeField]
    private bool setNumber;
    [SerializeField]
    private bool setDescription;
    [SerializeField]
    private bool setItems;

    [SerializeField]
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (setNumber || setDescription)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        data = dataStorage.GetComponent<LevelDataStorage>().LevelInfo;
    }

    // Update is called once per frame
    void Update()
    {
        data = dataStorage.GetComponent<LevelDataStorage>().LevelInfo;
        if (setNumber)
        {
            text.text = data.LevelNumber.ToString();
        }
        else if (setDescription)
        {
            text.text = data.LevelDescription.ToString();
        }
        else if (setItems)
        {
            transform.GetChild(0).gameObject.SetActive(data.AxePresent);
            transform.GetChild(1).gameObject.SetActive(data.BucketPresent);
            transform.GetChild(2).gameObject.SetActive(data.CrowbarPresent);
            transform.GetChild(3).gameObject.SetActive(data.ExtinguisherPresent);
            transform.GetChild(4).gameObject.SetActive(data.LadderPresent);
        }
    }
}
