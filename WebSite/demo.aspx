
<HTML>
<HEAD>
    <meta charset="UTF-8">
    <TITLE> 인쇄 데모 </TITLE>

    <SCRIPT LANGUAGE="JavaScript">
	<!--
		function Installed()
		{
			try
			{
				return (new ActiveXObject('IEPageSetupX.IEPageSetup'));
			}
			catch (e)
			{
				return false;
			}
		}

		function printMe()
		{
			if (Installed())
			{
				IEPageSetupX.header = "";
				IEPageSetupX.footer = "";
				IEPageSetupX.PrintBackground = true;
				IEPageSetupX.ShrinkToFit = false;
				IEPageSetupX.Preview();
			}
			else
				alert("컨트롤을 설치하지 않았네요.. 정상적으로 인쇄되지 않을 수 있습니다.");
		}

	//-->
    </SCRIPT>
</HEAD>

<BODY OnLoad="setTimeout('printMe();', 500);" OnUnload="if (Installed()) IEPageSetupX.RollBack();">
    <OBJECT id=IEPageSetupX classid="clsid:41C5BC45-1BE8-42C5-AD9F-495D6C8D7586" codebase="./IEPageSetupX.cab#version=1,4,0,3" style="width:0;height:0">
        <param name="copyright" value="http://isulnara.com">
        <div style="position:absolute;top:276;left:320;width:300;height:68;border:solid 1 #99B3A0;background:#D8D7C4;overflow:hidden;z-index:1;visibility:visible;"><FONT style='font-family: "굴림", "Verdana"; font-size: 9pt; font-style: normal;'><BR>&nbsp;&nbsp;인쇄 여백제어 컨트롤이 설치되지 않았습니다.&nbsp;&nbsp;<BR>&nbsp;&nbsp;<a href="./IEPageSetupX.exe"><font color=red>이곳</font></a>을 클릭하여 수동으로 설치하시기 바랍니다.&nbsp;&nbsp;</FONT></div>
    </OBJECT>

    <span style="font-family:굴림;font-size:10pt;color:black;filter:glow(color=#006000,strength:2);height:0px;font-weight:bold;line-height=180%">
        들꽃에게
        <br />

        <br />
        서 정 윤
        <br />

        <br />

        <br />
        어디에서 피어
        <br />
        언제 지든지
        <br />
        너는 들꽃이다.
        <br />

        <br />
        내가 너에게 보내는 그리움은
        <br />
        오히려 너를 시들게 할 뿐,
        <br />
        너는 그저 논두렁 길가에
        <br />
        피었다 지면 그만이다.
        <br />

        <br />
        인간이 살아, 살면서 맺는
        <br />
        숱한 인연의 매듭들을
        <br />
        이제는 풀면서 살아야겠다.
        <br />
        들꽃처럼 소리 소문없이
        <br />
        보이지 않는 곳에서
        <br />
        피었다 지면 그만이다.
        <br />

        <br />
        한 하늘 아래
        <br />
        너와 나는 살아있다.
        <br />
        그것만으로도 아직은 살 수 있고
        <br />
        나에게 허여된 시간을
        <br />
        그래도 열심히 살아야 한다.
        <br />
        그냥 피었다 지면
        <br />
        그만일 들꽃이지만
        <br />
        홀씨를 날릴 강한 바람을
        <br />
        아직은 기다려야 한다.
    </span>
</BODY>
</HTML>
