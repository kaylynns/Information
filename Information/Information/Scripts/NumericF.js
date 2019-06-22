function FormatDecimalAsNumber(c,p)
{
    c.value=DecimalToNumber(c.value,p);
    c.value=NumberToDecimal(c.value,p);
    c.style.color=(c.value.match(/\x2D/)==null?c.getAttribute("positiveColor"):c.getAttribute("negativeColor"));
    var max=c.getAttribute('maxAmount');
    if(((parseFloat)(c.value))>((parseFloat)(max)))
    {
        c.value=max;
    }
    var min=c.getAttribute('minAmount');
    if(((parseFloat)(c.value))<((parseFloat)(min)))
    {
        c.value=min;
    }
    c.value=DecimalToNumber(c.value,p);
}

function FormatNumberAsDecimal(c)
{
    c.value=NumberToDecimal(c.value);
    c.style.color="black";
    c.select();
}

function NumberToDecimal(n)
{
    n=n.toString();
    n=n.replace(/[^\d\x2D\x2E]/g,'');
    return n;
}

function DecimalToNumber(n,p)
{
    n=n.toString();
    if(p==null)
    {
        p=2;
    }
    var sy=new Array('-','');
    var neg=(n.match(/\x2D/)!=null?true:false);
    n=n.replace(/[^\d\x2E]/g,'');
    var m=n.match(/(\d*)(\x2E*)(\d*)/);
    var f=m[3];
    if(f.length>p){f=f/Math.pow(10,(f.length-p));
        f=Math.round(f);
    while(f.toString().length<p)
    {
        f='0'+f};
    }else{while(f.toString().length<p){f+='0'};}var w=new Number(m[1]);if(f==Math.pow(10,p)){w+=1;f=f.toString().substr(1);}w=w.toString();var s=3;var l=w.length-s;while(l>0){w=w.substr(0,l)+'\x2C'+w.substr(l);l-=s;}if(p==0){m[2]='';f=''}else{m[2]='\x2E'}return (neg?sy[0]+w+m[2]+f+sy[1]:w+m[2]+f);}

function EnsureNumeric(evt)
{
    evt = (evt) ? evt : ((window.event) ? event : null);
    if (evt)
    {
        var charCode = (evt.charCode) ? evt.charCode :	((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
        var ch = String.fromCharCode(charCode);
    }
    var k=charCode;
    if(!((k>47&&k<58)||k==46||k==45||k==8||k==37||k==39))
    {
        if (window.event)
            {
                evt.returnValue = false;
            }else 
            {
                evt.preventDefault();
            }
    }
}
