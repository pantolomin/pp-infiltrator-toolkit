﻿using PhoenixPoint.Tactical.AI;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Levels;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using PhoenixPoint.Common.View.ViewControllers;
using System;
using PhoenixPoint.Common.Core;

namespace phoenix_point.mod.infiltrator_toolkit
{
    public class Indicators
    {
        private static readonly ISet<TacticalFactionVision> enemyFactions = new HashSet<TacticalFactionVision>(10);
        private static Color? defaultColor;
        private static Color locatedColor;
        private static Color revealedColor;
        private static Color alertedColor;

        public static object TacticalLevel { get; private set; }

        internal static void DeclareFaction(TacticalFactionVision factionVision)
        {
            TacticalFaction viewerFaction = factionVision.Faction.TacticalLevel.View.ViewerFaction;
            if (viewerFaction != factionVision.Faction)
            {
                if (viewerFaction.GetRelationTo(factionVision.Faction) == FactionRelation.Enemy)
                {
                    enemyFactions.Add(factionVision);
                }
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
                locatedColor = toColor(Mod.Config.locatedColor);
                revealedColor = toColor(Mod.Config.revealedColor);
                alertedColor = toColor(Mod.Config.alertedColor);
            }
            Color color = defaultColor.Value;
            if (enemyFactions.Contains(actor.TacticalFaction.Vision))
            {
                if (actor.AIActor?.IsAlerted ?? false)
                {
                    color = alertedColor;
                }
            } else if (!actor.IsDead)
            {
                if (enemyFactions.Where(facVis => facVis.IsRevealed(actor)).Any())
                {
                    color = revealedColor;
                }
                else if (enemyFactions.Where(facVis => facVis.IsLocated(actor)).Any())
                {
                    color = locatedColor;
                }
            }
            UpdateIndicator(classIcon, color);
        }

        private static void UpdateIndicator(ActorClassIconElement classIcon, Color color)
        {
            classIcon.MainClassIcon.color = color;
            if (classIcon.SecondaryClassIcon != null)
            {
                classIcon.SecondaryClassIcon.color = color;
            }
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
    }
}
