using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManaging
{
    public enum ESources
    {
        KEY,
        MENU,
        LEVEL,
        BUTTON
    } 
    public enum ESoundTypes
    {
        HOVER,
        HOVER2,
        HOVER3,
        PRESS,
        KEYMISS,
        KEYGOOD,
        KEYPERFECT
    }

    public enum EMusicTypes
    {
        MENUMUSIC,
        Abyss,
        Devil,
        Erosion,
        Moneybagg,
        WINGAMEMUSIC,
        LOSEGAMEMUSIC
    }

}

public enum ELineIndex
{
    ONE,
    TWO,
    THREE,
    FOUR
}
public enum ELineType
{
    MISS,
    GOOD,
    PERFECT
}

public enum EButtonType
{
    SHORT,
    LONG,
    NONE
}

public enum ELevelDifficulty
{
    EASY,
    NORMAL,
    HARD
}
