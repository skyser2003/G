using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class PlayerCharacter
{
    public enum STATE
    {
        IDLE,
        WALK,
        ATTACK
    }

    private abstract class AbstractFSM
    {
        protected PlayerCharacter pc;

        abstract public void OnBegin();
        abstract public void OnUpdate();
        abstract public void OnEnd();

        public void Init(PlayerCharacter pc)
        {
            this.pc = pc;
        }
    }
}
