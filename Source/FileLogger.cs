﻿using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SkyGate
{
    /// <summary>
    /// The application logger based on file.
    /// </summary>
    public class FileLogger : ILoggerProvider, ILogger, ILoggerFactory
    {
        // opened writer on the log file
        readonly StreamWriter logWriter;

        internal FileLogger(string file)
        {
            // init the file-based logger
            var stream = new FileStream(file, FileMode.Append, FileAccess.Write);
            logWriter = new StreamWriter(stream, Encoding.UTF8, 4096, false)
            {
                AutoFlush = true
            };
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public int Level { get; internal set; } = 3;


        //
        // LOGGING

        // subworks are already there
        public ILogger CreateLogger(string name)
        {
            return this;
        }

        public IDisposable BeginScope<T>(T state)
        {
            return this;
        }

        public bool IsEnabled(LogLevel level)
        {
            return (int) level >= this.Level;
        }

        static readonly string[] LVL =
        {
            "TRC: ", "DBG: ", "INF: ", "WAR: ", "ERR: ", "CRL: ", "NON: "
        };

        void WritePart(char pre, int v)
        {
            if (pre > 0)
            {
                logWriter.Write(pre);
            }
            if (v < 10)
            {
                logWriter.Write('0');
            }
            logWriter.Write(v);
        }

        public void Log<T>(LogLevel level, EventId eid, T state, Exception except, Func<T, Exception, string> formatter)
        {
            if (!IsEnabled(level))
            {
                return;
            }

            logWriter.Write(LVL[(int) level]);

            var now = DateTime.Now;

            WritePart((char) 0, now.Day);
            WritePart(' ', now.Hour);
            WritePart(':', now.Minute);
            WritePart(':', now.Second);

            logWriter.Write(" ");

            if (eid.Id != 0)
            {
                logWriter.Write("{");
                logWriter.Write(eid.Id);
                logWriter.Write("} ");
            }

            // if (formatter != null) // custom format
            // {
            //     var msg = formatter(state, except);
            //     logWriter.WriteLine(msg);
            // }
            // else // fixed format
            // {
            logWriter.WriteLine(state.ToString());
            if (except != null)
            {
                logWriter.WriteLine(except.StackTrace);
            }
            // }
        }

        // end of a logger scope
        public void Dispose()
        {
        }
    }
}