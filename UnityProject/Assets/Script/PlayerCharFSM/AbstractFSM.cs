using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class PlayerCharacter
{
    public enum MOVE_STATE
    {
        MS_IDLE,
        MS_WALK,
    }

    public enum ACTION_STATE
    {
        AS_IDLE,
        AS_ATTACK
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
