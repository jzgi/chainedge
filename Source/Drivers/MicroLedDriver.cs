﻿using System.Windows;

namespace ChainEdge.Drivers
{
    public class MicroLedDriver : Driver
    {
        MediaPlayWindow sidewin;


        protected internal override void OnCreate(object state)
        {
        }

        public override void Bind()
        {
        }

        public override string Label => "微屏";

        public void show(string uri)
        {
        }
    }


    public class MediaPlayWindow : Window
    {
        // MediaElement webvw;
    }
}