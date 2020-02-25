using UnityEngine;

namespace phoenix_point.mod.infiltrator_toolkit
{
    public class Indicator
    {
        public readonly string Name;
        public readonly Color Colour;
        public Sprite Icon
        {
            get;
            private set;
        }

        public Indicator(string Name, Color Colour)
        {
            this.Name = Name;
            this.Colour = Colour;
        }

        public void CreateSprite(Sprite sourceSprite)
        {
            if (this.Icon == null)
            {
                Rect rect = new Rect(641f, (float) sourceSprite.texture.height - 131f, 122f, -86f);
                this.Icon = Sprite.Create(sourceSprite.texture, rect, new Vector2(rect.xMin, rect.yMin));
            }
        }
    }
}
