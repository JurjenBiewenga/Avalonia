// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Platform;
using Avalonia.Threading;
using Avalonia.UnitTests;
using Xunit;

namespace Avalonia.Base.UnitTests
{
    public class AvaloniaObjectTests_Threading
    {
        [Fact]
        public void StyledProperty_GetValue_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => target.GetValue(Class1.StyledProperty));
            }
        }

        [Fact]
        public void StyledProperty_SetValue_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => target.SetValue(Class1.StyledProperty, "foo"));
            }
        }

        [Fact]
        public void Setting_StyledProperty_Binding_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => 
                    target.Bind(
                        Class1.StyledProperty,
                        new BehaviorSubject<string>("foo")));
            }
        }

        [Fact]
        public void StyledProperty_ClearValue_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => target.ClearValue(Class1.StyledProperty));
            }
        }

        [Fact]
        public void StyledProperty_IsSet_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => target.IsSet(Class1.StyledProperty));
            }
        }

        [Fact]
        public void DirectProperty_GetValue_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => target.GetValue(Class1.DirectProperty));
            }
        }

        [Fact]
        public void DirectProperty_SetValue_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => target.SetValue(Class1.DirectProperty, "foo"));
            }
        }

        [Fact]
        public void Setting_DirectProperty_Binding_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() =>
                    target.Bind(
                        Class1.DirectProperty,
                        new BehaviorSubject<string>("foo")));
            }
        }

        [Fact]
        public void DirectProperty_ClearValue_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => target.ClearValue(Class1.DirectProperty));
            }
        }

        [Fact]
        public void DirectProperty_IsSet_Should_Throw()
        {
            using (UnitTestApplication.Start(new TestServices(threadingInterface: new ThreadingInterface())))
            {
                var target = new Class1();

                Assert.Throws<InvalidOperationException>(() => target.IsSet(Class1.DirectProperty));
            }
        }

        private class Class1 : AvaloniaObject
        {
            public static readonly StyledProperty<string> StyledProperty =
                AvaloniaProperty.Register<Class1, string>("Foo", "foodefault");

            public static readonly DirectProperty<Class1, string> DirectProperty =
                AvaloniaProperty.RegisterDirect<Class1, string>("Qux", _ => null, (o, v) => { });
        }

        private class ThreadingInterface : IPlatformThreadingInterface
        {
            public ThreadingInterface(bool isLoopThread = false)
            {
                CurrentThreadIsLoopThread = isLoopThread;
            }

            public bool CurrentThreadIsLoopThread { get; set; }

            public event Action<DispatcherPriority?> Signaled;

            public void RunLoop(CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }

            public void Signal(DispatcherPriority prio)
            {
                throw new NotImplementedException();
            }

            public IDisposable StartTimer(TimeSpan interval, Action tick)
            {
                throw new NotImplementedException();
            }
        }
    }
}
