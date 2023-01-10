using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store a m_Level and experience. This is primarily used in the <see cref="LevelHandler"/> utilized as a list of levels for the <see cref="LevelHandler.experienceCurve"/>
/// </summary>
[System.Serializable]
public class LevelContainer
{
    #region Protected Variables

    [SerializeField] protected int level;
    [SerializeField] protected int exp;

    #endregion

    #region Public Properties
    /// <summary>
    /// The Experience points.
    /// </summary>
    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }
    /// <summary>
    /// The Level.
    /// </summary>
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    #endregion

    #region Public Constructors

    public LevelContainer(int level, int exp)
    {
        this.level = level;
        this.exp = exp;
    }
    public LevelContainer(int level, float exp)
    {
        this.level = level;
        this.exp = Mathf.RoundToInt(exp);
    }

    public LevelContainer()
    {

    }

    #endregion

}
