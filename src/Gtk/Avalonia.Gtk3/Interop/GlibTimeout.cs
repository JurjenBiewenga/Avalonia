﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace Avalonia.Gtk3.Interop
{
    static class GlibTimeout
    {
        static bool Handler(IntPtr data)
        {
            var handle = GCHandle.FromIntPtr(data);
            var cb = (Func<bool>) handle.Target;
            if (!cb())
            {
                handle.Free();
                return false;
            }
            return true;
        }

        private static readonly GCHandle PinnedHandle;
        private static readonly Native.D.timeout_callback PinnedHandler;
        static GlibTimeout()
        {
            PinnedHandler = Handler;
            
        }


        public static void Add(int priority, uint interval, Func<bool> callback)
        {
            var handle = GCHandle.Alloc(callback);
            //Native.GTimeoutAdd(interval, PinnedHandler, GCHandle.ToIntPtr(handle));
            Native.GTimeoutAddFull(priority, interval, PinnedHandler, GCHandle.ToIntPtr(handle), IntPtr.Zero);
        }

        class Timer : IDisposable
        {
            public bool Stopped;
            public void Dispose()
            {

                Stopped = true;
            }
        }

        public static IDisposable StarTimer(uint interval, Action tick)
        {
            if (interval == 0)
                throw new ArgumentException("Don't know how to create a timer with zero or negative interval");
            var timer = new Timer ();
            GlibTimeout.Add(GlibPriority.FromDispatcherPriority(DispatcherPriority.Background), interval,
                () =>
                {
                    if (timer.Stopped)
                        return false;
                    tick();
                    return !timer.Stopped;
                });

            return timer;
        }
    }
}
