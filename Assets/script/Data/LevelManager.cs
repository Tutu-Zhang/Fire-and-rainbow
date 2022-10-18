using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int level;
    private void Awake()
    {
        Instance = this;
        level = -1;
    }
}
