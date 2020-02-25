using Harmony;
using PhoenixPoint.Tactical.AI;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Levels;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace phoenix_point.mod.infiltrator_toolkit
{
    public class Indicators
    {
        private static readonly IList<TacticalFactionVision> aiFactions;
        private static readonly Indicator locatedIndicator;
        private static readonly Indicator revealedIndicator;
        private static readonly Indicator alertedIndicator;

        static Indicators()
        {
            aiFactions = new List<TacticalFactionVision>(5);
            locatedIndicator = new Indicator("LocatedIndicator", Color.yellow);
            revealedIndicator = new Indicator("RevealedIndicator", Color.red);
            alertedIndicator = new Indicator("AlertedIndicator", Color.magenta);
        }

        internal static void DeclareFaction(TacticalFactionVision factionVision)
        {
            if (factionVision.Faction.IsControlledByAI)
            {
                aiFactions.Add(factionVision);
            }
        }

        internal static void InitIndicators(RectTransform transform, TacticalActorBase actor, Sprite smallIcon)
        {
            if (actor.IsFromViewerFaction)
            {
                CreateIndicator(transform, locatedIndicator, smallIcon);
                CreateIndicator(transform, revealedIndicator, smallIcon);
            }
            else
            {
                CreateIndicator(transform, alertedIndicator, smallIcon);
            }
        }

        internal static void UpdateIndicators(Transform transform, TacticalActorBase actor)
        {
            if (actor.IsFromViewerFaction)
            {
                FileLog.Log("UpdateIndicators(" + actor.GetDisplayName() + ") -> viewer");
                bool revealed = false;
                bool located = false;
                if (!actor.IsDead)
                {
                    revealed = aiFactions.Where(facVis => facVis.IsRevealed(actor)).Any();
                    if (!revealed)
                    {
                        located = aiFactions.Where(facVis => facVis.IsLocated(actor)).Any();
                    }
                    FileLog.Log("revealed: " + revealed+", located: "+ located);
                }
                UpdateIndicator(transform, locatedIndicator, located);
                UpdateIndicator(transform, revealedIndicator, revealed);
            }
            else
            {
                FileLog.Log("UpdateIndicators(" + actor.GetDisplayName() + ") -> AI");
                TacAIActor aiActor = actor.AIActor;
                if (aiActor != null)
                {
                    FileLog.Log("alerted: " + aiActor.IsAlerted);
                    UpdateIndicator(transform, alertedIndicator, aiActor.IsAlerted);
                }
            }
        }

        private static void CreateIndicator(Transform transform, Indicator indicator, Sprite smallIcon)
        {
            indicator.CreateSprite(smallIcon);
            GameObject gameObject = new GameObject(indicator.Name);
            RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
            rectTransform.SetParent(transform, false);
            Vector2 vector2 = new Vector2(0f, 1f);
            rectTransform.anchorMin = vector2;
            rectTransform.anchorMax = vector2;
            rectTransform.localScale = Vector2.one * 0.25f;
            Image image = gameObject.AddComponent<Image>();
            image.sprite = indicator.Icon;
            image.preserveAspect = true;
            gameObject.SetActive(false);
            image.color = indicator.Colour;
        }

        private static void UpdateIndicator(Transform transform, Indicator indicator, bool showIcon)
        {
            transform = transform.Find(indicator.Name);
            if (transform != null)
            {
                transform.gameObject.SetActive(showIcon);
            }
        }
    }
}
