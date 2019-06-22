$(document).ready(function(){
	$(window).scroll(resscrEvt);
	$(window).resize(resscrEvt);
//	$("#cPic").click(closeWin);
	resscrEvt();
});
//显示灰色背景和操作窗�?
//vType:窗口加载的是html代码还是文件，参数可能为html或url
//url为代码或文件�?args 为传递的参数 格式为{arg1:"",arg2:""} w为窗口的�?h为窗口的�?
function showWin(vType,url,args,w,h){
	var bH=$(window).height()+ $(window).scrollTop();
	var bW=$(window).width()+$(window).scrollLeft();
	$("#msg").height(h);
	$("#msg").width(w);
	var objWH=objValue("msg");
	$("#fullBg").css({width:bW,height:bH,"display":"block"});
	var tbT=objWH.split("|")[0]+"px";
	var tbL=objWH.split("|")[1]+"px";
	$("#msg").css({top:tbT,left:tbL,display:"block"});
	$("#ctt").html("<div style='text-align:center'>正在加载，请稍后...</div>");
	if(vType=="url") $("#ctt").load(url,args);
	else  $("#ctt").html(url);
}
function objValue(obj){
	var st=document.documentElement.scrollTop;//滚动条距顶部的距�?
	var sl=document.documentElement.scrollLeft;//滚动条距左边的距�?
	var ch=document.documentElement.clientHeight;//屏幕的高�?
	var cw=document.documentElement.clientWidth;//屏幕的宽�?
	var objH=$("#"+obj).height();//浮动对象的高�?
	var objW=$("#"+obj).width();//浮动对象的宽�?
	var objT=Number(st)+(Number(ch)-Number(objH))/2;
	var objL=Number(sl)+(Number(cw)-Number(objW))/2;
	return objT+"|"+objL;
}
function resscrEvt(){
	var bjCss=$("#fullBg").css("display");
	if(bjCss=="block"){
	var bH2=$(window).height() + $(window).scrollTop();
	var bW2=$(window).width() +$(window).scrollLeft();
	$("#fullBg").css({width:bW2,height:bH2});
	var objV=objValue("msg");
	var tbT=objV.split("|")[0]+"px";
	var tbL=objV.split("|")[1]+"px";
	$("#msg").css({top:tbT,left:tbL});
	}
}

//关闭灰色背景和操作窗�?
function closeWin(lx,zt,lb){
	$("#fullBg").css("display","none");
	$("#msg").css("display","none");
	if(lx=='1')
	    window.location.href='main.aspx?zt='+zt+'&lb='+lb;	
	else
	    window.location.href='main2.aspx?zt='+zt+'&lb='+lb;	 
}