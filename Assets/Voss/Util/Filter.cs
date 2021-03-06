﻿using System;
using System.Net;
using Constants;
using UnityEngine;

/// <summary>
/// A frame rate independent finite impulse response filter suite.
/// </summary>
public static class Filter
{
    public static float FIR(float previous, float current = 0, float smoothness = 0.9f, TimeBase timebase = TimeBase.DeltaTime, float fps = 60.0f)
    {
        var k = Mathf.Pow(smoothness, ReferenceTime(timebase, fps));
        return previous * k + (1.0f - k) * current;
    }    

    public static Vector2 FIR(Vector2 previous, Vector2 current, float smoothness = 0.9f, TimeBase timebase = TimeBase.DeltaTime, float fps = 60.0f)
    {
        var k = Mathf.Pow(smoothness, ReferenceTime(timebase, fps));
        return previous * k + (1.0f - k) * current;
    }    

    public static Vector3 FIR(Vector3 previous, Vector3 current, float smoothness = 0.9f, TimeBase timebase = TimeBase.DeltaTime, float fps = 60.0f)
    {
        var k = Mathf.Pow(smoothness, ReferenceTime(timebase, fps));
        return previous * k + (1.0f - k) * current;
    }    

    public static Vector3 FIR3(Vector3 previous, Vector3 current, float smoothness = 0.9f, TimeBase timebase = TimeBase.DeltaTime, float fps = 60.0f)
    {
        var k = Mathf.Pow(smoothness, ReferenceTime(timebase, fps));
        return previous * k + (1.0f - k) * current;
    }


    private static float ReferenceTime(TimeBase timebase, float fps)
    {
        switch (timebase)
        {
            case TimeBase.FixedDeltaTime:
                return Time.fixedDeltaTime * fps;
            
            case TimeBase.UnscaledDeltaTime:
                return Time.unscaledDeltaTime * fps;
            
            case TimeBase.UnscaledFixedDeltaTime:
                return Time.fixedUnscaledDeltaTime * fps;
         
            case TimeBase.DeltaTime:
                return Time.deltaTime * fps;
            
            default:
                return 1.0f;
        }
    }

    public enum TimeBase
    {
        None,
        DeltaTime,
        FixedDeltaTime,
        UnscaledDeltaTime,
        UnscaledFixedDeltaTime
    }    
}

/*
Written by Moritz Voss in 2018

This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
*/