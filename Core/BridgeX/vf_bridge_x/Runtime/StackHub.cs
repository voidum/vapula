﻿using System;
using System.Collections.Generic;

namespace Vapula.Runtime
{
    /// <summary>
    /// hub for stack
    /// </summary>
    internal class StackHub
    {
        private static StackHub _Instance
            = null;
        private static readonly object _SyncCtor
            = new object();

        public static StackHub Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncCtor)
                    {
                        _Instance = new StackHub();
                    }
                }
                return _Instance;
            }
        }

        private readonly object _SyncRoot = new object();

        private StackHub()
        {
        }

        private List<Stack> _Stacks = new List<Stack>();

        public Stack this[IntPtr handle]
        {
            get
            {
                foreach (Stack stack in _Stacks) 
                {
                    if (stack.Handle == handle)
                        return stack;
                }
                return null; 
            }
        }

        public void Link(Stack stack)
        {
            IntPtr handle = stack.Handle;
            lock (_SyncRoot) 
            {
                if (this[handle] == null)
                    _Stacks.Add(stack);
            }
        }

        public void Kick(Stack stack)
        {
            IntPtr handle = stack.Handle;
            lock (_SyncRoot)
            {
                if(_Stacks.Contains(stack))
                    _Stacks.Remove(stack);
            }
        }
    }
}
