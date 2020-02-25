using PhoenixPoint.Tactical.AI;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Levels;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using PhoenixPoint.Common.View.ViewControllers;
using System;
using Harmony;

namespace phoenix_point.mod.infiltrator_toolkit
{
    public class Indicators
    {
        private static readonly IList<TacticalFactionVision> aiFactions = new List<TacticalFactionVision>(10);
        private static Color? defaultColor;
        private static Color locatedColor;
        private static Color revealedColor;
        private static Color alertedColor;

        internal static void DeclareFaction(TacticalFactionVision factionVision)
        {
            if (factionVision.Faction.IsControlledByAI)
            {
                aiFactions.Add(factionVision);
            }
        }

        internal static void UpdateIndicators(ActorClassIconElement classIcon, TacticalActorBase actor)
        {
            if (classIcon?.MainClassIconMask == null)
            {
                return;
            }
            if (defaultColor == null)
            {
                defaultColor = classIcon.MainClassIconMask.color;
                locatedColor = Mod.GetValue("LocatedColor", val => toColor(val), toColor("ffcc00"));
                revealedColor = Mod.GetValue("RevealedColor", val => toColor(val), toColor("e65c00"));
                alertedColor = Mod.GetValue("AlertedColor", val => toColor(val), toColor("cc0066"));
            }
            Color color = defaultColor.Value;
            if (actor.IsFromViewerFaction)
            {
                if (!actor.IsDead)
                {
                    if (aiFactions.Where(facVis => facVis.IsRevealed(actor)).Any())
                    {
                        color = revealedColor;
                    }
                    else if (aiFactions.Where(facVis => facVis.IsLocated(actor)).Any())
                    {
                        color = locatedColor;
                    }
                }
            }
            else
            {
                TacAIActor aiActor = actor.AIActor;
                if (aiActor != null && aiActor.IsAlerted)
                {
                    color = alertedColor;
                }
            }
            UpdateIndicator(classIcon, color);
        }

        private static Color toColor(string colorComponents)
        {
            if (colorComponents.Length < 6 || colorComponents.Length > 8)
            {
                throw new Exception("Cannot parse " + colorComponents + " to a color");
            }
            int argb = Convert.ToInt32(colorComponents, 16);
            float alpha = ((argb >> 24) & 0xFF) / 255f;
            float red = ((argb >> 16) & 0xFF) / 255f;
            float green = ((argb >> 8) & 0xFF) / 255f;
            float blue = (argb & 0xFF) / 255f;
            return new Color(red, green, blue, alpha > 0f ? alpha : 1f);
        }

        private static void UpdateIndicator(ActorClassIconElement classIcon, Color color)
        {
            classIcon.MainClassIcon.color = color;
            if (classIcon.SecondaryClassIcon != null)
            {
                classIcon.SecondaryClassIcon.color = color;
            }
        }
    }
}
