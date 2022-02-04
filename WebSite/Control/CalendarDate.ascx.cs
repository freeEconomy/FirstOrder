using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_CalendarDate : System.Web.UI.UserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	public void DefaultDate(string setValue)
	{
		this.txtDate.Text = setValue;
	}
}
