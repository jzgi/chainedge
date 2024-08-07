﻿using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class NewOrderSpeechJob : Job
{
    public override void OnInit()
    {
    }

    public override void Run()
    {
        if (Driver is SpeechDriver drv)
        {
            drv.Speak("有新订单，请及时处理");
        }
    }

    public override string ToString()
    {
        return "新订单";
    }
}