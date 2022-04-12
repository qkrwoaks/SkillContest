using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterData", fileName = "ScriptableObject/MonsterData" ,order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    [SerializeField] private string monsterName;
    public string MonsterName { get { return monsterName; }}
    [SerializeField] private float hp;
    public float Hp { get { return hp; }} 
    [SerializeField] private float power;
    public float Power { get { return power; }} 
    [SerializeField] private float speed;
    public float Speed { get { return speed; }} 
    [SerializeField] private int score;
    public int Score { get { return score; }}
}
