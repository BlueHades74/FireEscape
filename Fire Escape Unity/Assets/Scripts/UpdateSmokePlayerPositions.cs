using UnityEngine;

public class UpdateSmokePlayerPositions : MonoBehaviour
{
    //Made by: Rafael Gonzalez Atiles
    //Last Edited by: Rafael Gonzalez Atiles

    [SerializeField]
    private Material smokeMaterial;

    [SerializeField]
    private GameObject player1;
    [SerializeField] 
    private GameObject player2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Send Player Positions
        smokeMaterial.SetVector("_Player1Position", player1.transform.position);
        smokeMaterial.SetVector("_Player2Position", player2.transform.position);
    }
}
