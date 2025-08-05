using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffect : MonoBehaviour
{

    [SerializeField] ParticleSystem fallTrail;
    [SerializeField] ParticleSystem burstRocks;
    [SerializeField] ParticleSystem lowHealth;
    [SerializeField] ParticleSystem hitL;
    [SerializeField] ParticleSystem hitR;
    [SerializeField] ParticleSystem dustJump;
    [SerializeField] ParticleSystem dustWalk;
    [SerializeField] ParticleSystem dustRun;
    [SerializeField] ParticleSystem dustSlide;

    public enum EffectType
    {
        FallTrail, BurstRocks, LowHealth, HitL, HitR, DustJump, DustWalk, DustRun, Slide
    }

    //public void DoEffect(EffectType effectType,bool enabled)
    //{
    //    switch (effectType)
    //    {
    //        case EffectType.FallTrail:
    //            if (enabled)
    //                fallTrail.Play();
    //            else
    //                fallTrail.Stop();
    //            break;


    //        case EffectType.BurstRocks:
    //            if (enabled)
    //                burstRocks.Play();
    //            else
    //                burstRocks.Stop();
    //            break;

    //        case EffectType.LowHealth:
    //            if (enabled)
    //                lowHealth.Play();
    //            else
    //                lowHealth.Stop();
    //            break;

    //        case EffectType.HitL:
    //            if (enabled)
    //                hitL.Play();
    //            else
    //                hitL.Stop();
    //            break;

    //        case EffectType.HitR:
    //            if (enabled)
    //                hitR.Play();
    //            else
    //                hitR.Stop();
    //            break;

    //        case EffectType.DustJump:
    //            if (enabled)
    //                dustJump.Play();
    //            else
    //                dustJump.Stop();
    //            break;

    //        case EffectType.DustWalk:
    //            if (enabled)
    //                dustWalk.Play();
    //            else
    //                dustWalk.Stop();
    //            break;

    //        case EffectType.DustRun:
    //            if (enabled)
    //                dustRun.Play();
    //            else
    //                dustRun.Stop();
    //            break;

    //        case EffectType.Slide:
    //            if (enabled)
    //                dustSlide.Play();
    //            else
    //                dustSlide.Stop();
    //            break;


    //        default:
    //            break;

    //    }

    //}



    public void DoEffect(EffectType effectType, bool enabled)
    {
        switch (effectType)
        {
            case EffectType.FallTrail:
                if (fallTrail == null) break;
                if (enabled) fallTrail.Play(); else fallTrail.Stop();
                break;

            case EffectType.BurstRocks:
                if (burstRocks == null) break;
                if (enabled) burstRocks.Play(); else burstRocks.Stop();
                break;

            case EffectType.LowHealth:
                if (lowHealth == null) break;
                if (enabled) lowHealth.Play(); else lowHealth.Stop();
                break;

            case EffectType.HitL:
                if (hitL == null) break;
                if (enabled) hitL.Play(); else hitL.Stop();
                break;

            case EffectType.HitR:
                if (hitR == null) break;
                if (enabled) hitR.Play(); else hitR.Stop();
                break;

            case EffectType.DustJump:
                if (dustJump == null) break;
                if (enabled) dustJump.Play(); else dustJump.Stop();
                break;

            case EffectType.DustWalk:
                if (dustWalk == null) break;
                if (enabled) dustWalk.Play(); else dustWalk.Stop();
                break;

            case EffectType.DustRun:
                if (dustRun == null) break;
                if (enabled) dustRun.Play(); else dustRun.Stop();
                break;

            case EffectType.Slide:
                if (dustSlide == null) break;
                if (enabled) dustSlide.Play(); else dustSlide.Stop();
                break;
        }
    }


}
