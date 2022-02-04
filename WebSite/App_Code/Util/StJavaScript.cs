using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Web.UI;
/// <summary>
/// StephenJavaScript의 요약 설명입니다.
/// </summary>

namespace FirstOrder.Util
{
    public class StJavaScript
    {
        private Page m_SourceDocument;
        private StringBuilder sb;
        private bool m_PageEnd = false;
        private bool m_IsStartupScript = false;

        public StJavaScript(Page TargetPage)
        {
            sb = new StringBuilder();

            m_SourceDocument = TargetPage;
        }

        public StJavaScript(Page TargetPage, bool PageEnd)
        {
            sb = new StringBuilder();

            m_SourceDocument = TargetPage;
            m_PageEnd = PageEnd;
        }

        public StJavaScript(Page TargetPage, bool PageEnd, bool IsStartUpScript)
        {
            sb = new StringBuilder();

            m_SourceDocument = TargetPage;
            m_PageEnd = PageEnd;
            m_IsStartupScript = IsStartUpScript;
        }

        public Page SourceDocument
        {
            get { return m_SourceDocument; }
        }

        public bool IsAsynchronous
        {
            get { return ScriptManager.GetCurrent(m_SourceDocument).IsInAsyncPostBack; }
            //get { return m_SourceDocument.IsAsync; }
        }

        private void GenerateJavaScriptTag(string sourceCode)
        {
            if (sb.Length > 0)
            {
                sb.Remove(0, sb.Length);
            }

            sb.AppendLine("<script charset=\"UTF-8\" type=\"text/javascript\" language=\"javascript\">");
            sb.AppendLine("//<![CDATA[");
            sb.AppendLine(@sourceCode);
            sb.AppendLine("//]]>");
            sb.AppendLine("</script>");

            SendJavascript();
        }

        private void GenerateJavaScriptTag(string[] sourceCode)
        {
            if (sb.Length > 0)
            {
                sb.Remove(0, sb.Length);
            }

            sb.AppendLine("<script charset=\"UTF-8\" type=\"text/javascript\" language=\"javascript\">");
            sb.AppendLine("//<![CDATA[");
            for (int i = 0; i < sourceCode.Length; i++)
            {
                sb.AppendLine(@sourceCode[i]);
            }
            sb.AppendLine("//]]>");
            sb.AppendLine("</script>");

            SendJavascript();
        }

        public void ShowAlertMessage(string message)
        {
            GenerateJavaScriptTag("alert(\"" + @message + "\");");
        }

        public void ShowAlertMessage(string message, string javascriptStr)
        {
            string[] arrMessage = { "alert(\"" + @message + "\");", @javascriptStr };

            GenerateJavaScriptTag(arrMessage);
        }

        public void WriteJavascript(string javascriptStr)
        {
            GenerateJavaScriptTag(@javascriptStr);
        }

        public void WriteJavascript(string[] javascriptStr)
        {
            GenerateJavaScriptTag(@javascriptStr);
        }

        private void SendJavascript()
        {
            if (m_PageEnd)
            {
                m_SourceDocument.Response.Write(sb.ToString());
                m_SourceDocument.Response.End();
            }
            else
            {
                if (IsAsynchronous == false)
                {
                    if (m_IsStartupScript)
                    {
                        SourceDocument.ClientScript.RegisterStartupScript(SourceDocument.GetType(), "JSAlertMessageBox", sb.ToString(), false);
                    }
                    else
                    {
                        SourceDocument.ClientScript.RegisterClientScriptBlock(SourceDocument.GetType(), "JSAlertMessageBox", sb.ToString(), false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(SourceDocument, SourceDocument.GetType(), "JSAlertMessageBox", sb.ToString(), false);
                }
            }
        }
    }
}