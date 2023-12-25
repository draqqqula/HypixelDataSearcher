j::
Loop 30
{
    Send, {Esc}
    Sleep, 200
    Send, {t}
    Sleep, 100
    Loop % (A_Index * 2 + 1)
    {
        Send, {Tab}
        Sleep, 15
    }
    Loop 30
    {
        Send, +{Left}
        Sleep, 15
    }
    Send, ^c
    Sleep, 100
    Send, {Esc}
    Sleep, 100
    Send, {Esc}
    Sleep, 100
    Send, !{Tab}
    Sleep, 100
    Send, {Enter}
    Sleep, 100
    Send, ^v
    Sleep, 100
    Send, !{Tab}
    Sleep, 1000
}
return