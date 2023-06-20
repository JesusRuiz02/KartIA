using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public List<GameObject> participants = new List<GameObject>();
    public List<int> progress = new List<int>();
    private GameObject player = default;
    public List<int> posiciones = new List<int>();
    private void Start()
    {
        participants.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        player = GameObject.FindGameObjectWithTag("Character");
        participants.Add(player);
        addParticipantsList();
        StartCoroutine(CycleScan());
    }

    private void OnDestroy()
    {
        Debug.Log("Se destruyo");
        StopCoroutine(CycleScan());
    }

    private IEnumerator CycleScan()
    {
        while (true)
        {
            CheckPosition();
            Reposition();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void addParticipantsList()
    {
        for (int i = 0; i < participants.Count; i++)
        {
            progress.Add(0);
        }
    }

    private void CheckPosition()
    {
        int index = 0;
        foreach (var VARIABLE in participants)
        {
            int progressIndex = VARIABLE.CompareTag("Player")  ? VARIABLE.GetComponent<CheckManager>().SAVEDINDEX : VARIABLE.GetComponent<PlayerCheckManager>().SAVEDINDEX;
            progress[index] = progressIndex;
            index++;
        }
        posiciones = progress.Select((value, idx) => new { Value = value, Index = idx })
            .OrderByDescending(x => x.Value)
            .Select(x => x.Index)
            .ToList();
    }

    private void Reposition()
    {
        int index = 0;
        foreach (var Players in participants)
        {
            int posicion = posiciones.IndexOf(index);
            if (Players.CompareTag("Player"))
            {
                Players.GetComponent<CheckManager>().cambiarLugar(posicion + 1);
            }
            else
            {
                Players.GetComponent<PlayerCheckManager>().cambiarLugar(posicion + 1);
            }
            index++;
        }
        
    }
    
    
}
