using Sitecore.Data.Items;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Names;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Presentation;
using System;

namespace Sitecore.Support.Mvc.Pipelines.Response.GetRenderer
{
    public class GetViewRenderer : Sitecore.Mvc.Pipelines.Response.GetRenderer.GetViewRenderer
    {
        public override void Process(GetRendererArgs args)
        {
            if (args.Result != null)
            {
                return;
            }
            args.Result = base.GetRenderer(args.Rendering, args);
        }
        protected override string GetViewPath(Rendering rendering, GetRendererArgs args)
        {
            return base.GetViewPathFromRenderingType(rendering, args) ?? this.GetViewPathFromRenderingItemNew(rendering);
        }

        protected string GetViewPathFromRenderingItemNew(Rendering rendering)
        {
            RenderingItem renderingItem = rendering.RenderingItem;
            bool flag = renderingItem == null || renderingItem.InnerItem.TemplateID != TemplateIds.ViewRendering;
            string result;
            if (flag)
            {
                result = null;
            }
            else
            {
                string text = renderingItem.InnerItem["path"];
                text = this.RemoveTraillingSpaces(text);
                bool flag2 = text.IsWhiteSpaceOrNull();
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    result = text;
                }
            }
            return result;
        }

        private string RemoveTraillingSpaces(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                bool flag = text[i] == ' ';
                if (!flag)
                {
                    break;
                }
                text = text.Remove(0, 1);
                i--;
            }
            for (int j = text.Length - 1; j > 0; j--)
            {
                bool flag2 = text[j] == ' ';
                if (!flag2)
                {
                    break;
                }
                text = text.Remove(j, 1);
            }
            return text;
        }
    }
}
