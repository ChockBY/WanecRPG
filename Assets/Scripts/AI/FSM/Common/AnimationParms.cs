using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 动画参数类
/// </summary>
namespace AI.FSM
{
    [Serializable]
    public class AnimationParms 
    {
        public string Idle = "idle";
        public string Dead = "dead";
        public string Run = "run";
        public string Walk = "walk";
        public string Attack = "attack";
    }
}
