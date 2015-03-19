using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Clock
{

    public float time;
    private long t;
    private DateTime dt;
    private long tick;
    public string value;

    public Clock()
    {
        time = 0;
    }

    public void update(float deltaTime)
    {
        time += deltaTime;
    }

    public String getText()
    {
        t = (long)time;
        tick = t * 10000 * 1000;
        dt = new DateTime(tick);
        value = dt.ToString("mm:ss");
        return value;
    }
}

