using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster
    }

    public enum Layer
    {
        Monster=6,
        Ground=3
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum Scene
    {
        UnKnown,
        Login,
        Lobby,
        Game

    }
    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        Click
    }

    public enum CmeraMode
    {
        QuarterView,
        ThirdPersonView
    }

    public enum EffectType
    {
        Default,
    }
}
