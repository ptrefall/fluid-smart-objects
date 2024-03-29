﻿using System;
using System.Collections.Generic;
using FluidHTN;
using FluidHTN.Contexts;
using FluidHTN.Debug;
using FluidHTN.Factory;

namespace Fluid
{
    public enum AIWorldState
    {
        Location
    }

    public class AIContext : BaseContext
    {
        private byte[] _worldState = new byte[Enum.GetValues(typeof(AIWorldState)).Length];

        public override IFactory Factory { get; set; } = new DefaultFactory();
        public override List<string> MTRDebug { get; set; }
        public override List<string> LastMTRDebug { get; set; }
        public override bool DebugMTR { get; } = false;
        public override Queue<IBaseDecompositionLogEntry> DecompositionLog { get; set; }
        public override bool LogDecomposition { get; } = true;
        public override byte[] WorldState => _worldState;

        public Player Player { get; }
        public World World { get; }

        public AIContext(Player player, World world)
        {
            Player = player;
            World = world;
        }

        public override void Init()
        {
            base.Init();

            // Custom init of state
        }

        public bool HasState(AIWorldState state, bool value)
        {
            return HasState((int) state, (byte) (value ? 1 : 0));
        }

        public bool HasState(AIWorldState state)
        {
            return HasState((int) state, 1);
        }

        public void SetState(AIWorldState state, byte value, EffectType type)
        {
            SetState((int) state, value, true, type);
        }

        public byte GetState(AIWorldState state)
        {
            return GetState((int) state);
        }
    }
}
