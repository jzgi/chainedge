﻿using ChainFX;

namespace ChainEdge;

/// <summary>
/// An output job that takes event content and operates on device driver.
/// </summary>
public abstract class Job
{
    public const short
        STU_VOID = 0,
        STU_HALF = 50,
        STU_COMPLETED = 100;

    short progress;

    public short Progress => progress;

    public JObj Data { get; set; }

    /// <summary>
    /// The hosting driver of this job.
    /// </summary>
    public Driver Driver { get; internal set; }

    public abstract void OnInit();

    public abstract void Run();
}