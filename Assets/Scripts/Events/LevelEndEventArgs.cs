using System;



public class LevelEndEventArgs : EventArgs
{
    public float Time { get; }
    public int Orbs { get; }
    public ScenesManager.Scenes LvlNumber { get; }

    public LevelEndEventArgs(float time, int orbs, ScenesManager.Scenes lvlNumber)
    {
        Time = time;
        Orbs = orbs;
        LvlNumber = lvlNumber;
    }
}
