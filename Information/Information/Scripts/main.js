/* Common List Functions */
//highlight line
/******************************************/
/*功能：高亮线显示                        */
/*参数：无                                */
/*返回：无                                */
/******************************************/
function DoHL()
{
var e=window.event.srcElement;
while (e.tagName!="TR"){e=e.parentNode;}
if (e.className!='SL') e.className='HL';
}

/******************************************/
/*功能：低亮线显示                        */
/*参数：无                                */
/*返回：无                                */
/******************************************/
function DoLL()
{
var e=window.event.srcElement;
while (e.tagName!="TR"){e=e.parentNode;}
if (e.className!='SL')	e.className='';
}

/******************************************/
/*功能：选择线显示                        */
/*参数：无                                */
/*返回：无                                */
/******************************************/
function DoSL()
{
var TB=e=window.event.srcElement;
while (TB.tagName!="TABLE")
{TB=TB.parentNode;}
for (var i=0;i<TB.rows.length;i++){
	if(TB.rows[i].className=='SL')TB.rows[i].className='';}
while (e.tagName!="TR"){e=e.parentNode;}
e.className=(e.className=='SL')?'':'SL';
}

/******************************************/
/*功能：select line                       */
/*参数：                                  */
/*返回：                                  */
/******************************************/
function hL(E){
while (E.tagName!="TR")
{E=E.parentNode;}
E.className="SL";
}
/******************************************/
/*功能：diselect line                     */
/*参数：                                  */
/*返回：                                  */
/******************************************/
function dL(E){
while (E.tagName!="TR")
{E=E.parentNode;}
E.className="";
}
