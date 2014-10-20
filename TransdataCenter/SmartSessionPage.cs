using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransdataCenter
{
    public class SmartSessionPage : System.Web.UI.Page
    {
        private const string NAME = "name";
        /// <summary>
        /// session[用户姓名]
        /// </summary>
        public string Name
        {
            get
            {
                return (string)Session[NAME];
            }
            set
            {
                Session[NAME] = value;
            }
        }

        private const string DEP = "dep";
        /// <summary>
        /// session[用户单位]
        /// </summary>
        public string Dep
        {
            get
            {
                return (string)Session[DEP];
            }
            set
            {
                Session[DEP] = value;
            }
        }

        private const string IDENTITY = "id";
        /// <summary>
        /// session[用户depid]
        /// </summary>
        public string Identity
        {
            get
            {
                return (string)Session[IDENTITY];
            }
            set
            {
                Session[IDENTITY] = value;
            }
        }
        private const string FLAG = "flag";
        /// <summary>
        /// session[flag]
        /// 默认flag=0为子公司查看本公司情况权限 所有的flag=1则为集团公司为最大读取权限 flag=2为维修公司查看本公司情况权限
        /// </summary>
        public int Flag
        {
            get
            {
                return (int)Session[FLAG];
            }
            set
            {
                Session[FLAG] = value;
            }
        }
    }
}