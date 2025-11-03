using Unity.Cinemachine;
using UnityEngine;

public class TestSave : MonoBehaviour, IDataPersistence
{
    private int deathCount = 0;

    public void LoadData(GameData data)
    {
        this.deathCount = data.deathCount;
    }

    public void SaveData(GameData data)
    {
        data.deathCount = this.deathCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            deathCount++;
            Debug.Log($"Death Count: {deathCount}");
        }
    }
}
