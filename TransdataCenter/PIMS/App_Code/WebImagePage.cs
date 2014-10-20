using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

    public abstract class WebImagePage : System.Web.UI.Page
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            Image im = GetImage();
            if (im == null) return;
            Response.Clear();
            Response.ContentType = "Image/Png";
            Response.BufferOutput = true;
            using (MemoryStream ms = new MemoryStream())
            {
                im.Save(ms, ImageFormat.Png);
                ms.Flush();
                Response.BinaryWrite(ms.GetBuffer());
            }
            //im.Save(Response.OutputStream, ImageFormat.Png);
            Response.End();
        }

        protected virtual string cacheName()
        {
            return Request.Url.ToString();
        }

        protected virtual DateTime cacheClearTime()
        {
            return DateTime.Now.AddDays(1);
        }

        protected virtual Image GetImage()
        {
            if (cacheName() != null && cacheName().Trim() != "")
            {
                object ChartObj = HttpContext.Current.Cache.Get(cacheName());
                if (ChartObj == null)
                {
                    ChartObj = MakeImage();
                    HttpContext.Current.Cache.Insert(cacheName(), ChartObj, null, cacheClearTime(), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null); //添加Cache缓存数据提高速度
                }
                return (Image)ChartObj;
            }
            else return MakeImage();
        }

        protected abstract Image MakeImage();
    }