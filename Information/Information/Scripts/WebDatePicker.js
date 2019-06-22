///////////////////////////////////////////////////
//WebDatePicker.js
//PowerAsp.NET Controls
//////////////////////////////////////////////////
var currentOpenCalender;
var currentIsOpen = 0;

function HideCalender()
{
   if(currentIsOpen == 1)
   {
      currentOpenCalender.style.display = "none";
      currentOpenCalender = null;
      document.onclick=null;
      defaultCoordinate.Remove();
   }
   currentIsOpen++;
   return true;
 }

function CalenderObject(ctrl,edit,initDate)
{
 this.currentDate = initDate;
 this.owner = ctrl;
 this.editCtrl = edit;
 //mapping control from DOM to variable.
 this.yearCtrl = ctrl.children[0].children[0].children[0].children[0].children[0].children[0].children[2];
 this.monthCtrl = ctrl.children[0].children[0].children[0].children[0].children[0].children[0].children[5];
 this.headerFirst = ctrl.children[0].children[0].children[0].children[0].children[0].children[1];
 this.date_row1 = ctrl.children[0].children[0].children[0].children[0].children[0].children[2];
 this.date_row2 = ctrl.children[0].children[0].children[0].children[0].children[0].children[3];
 this.date_row3 = ctrl.children[0].children[0].children[0].children[0].children[0].children[4];
 this.date_row4 = ctrl.children[0].children[0].children[0].children[0].children[0].children[5];
 this.date_row5 = ctrl.children[0].children[0].children[0].children[0].children[0].children[6]; 
 this.date_row6 = ctrl.children[0].children[0].children[0].children[0].children[0].children[7]; 
 this.dateCtrls = new Array(32);
 this.currentDay;


 this.CoordinateNow = function()
 {
      currentIsOpen = 1;	
      HideCalender();	
 }
 
 this.UpdateData = function()
 {
   var baseDate = new Date(this.currentDate.getYear(),this.currentDate.getMonth(),this.currentDate.getDate());
   
   var year = (this.currentDate.getYear()<1900?(1900+this.currentDate.getYear()):this.currentDate.getYear());
   
   var ysel = year;
   var msel = baseDate.getMonth()+1;
   var dsel = baseDate.getDate();
   if(msel<10)msel='0'+msel
   if(dsel<10)dsel='0'+dsel
   this.editCtrl.value = ysel+'-'+msel+'-'+dsel; 
 }
 
 this.getDayCount = function()
 {
  var destDate = new Date();
  var baseDate = new Date(this.currentDate.getYear(),this.currentDate.getMonth(),this.currentDate.getDay());
  baseDate.setDate(1);
  destDate.setDate(1);
  
   var year = (this.currentDate.getYear()<1900?(1900+this.currentDate.getYear()):this.currentDate.getYear());
   
  var ysel = year;
  i = baseDate.getMonth();
  destDate.setYear(ysel);
  destDate.setMonth(i+1); 
  v = destDate - baseDate;
  var finalDate = v / 60 / 60 / 24 / 1000;
  if(this.currentDate.getMonth()+1==12)
  {
  return 31;
  }
  return finalDate;
 }
 
 this.SelectedDay = function(ctrl,flag)
 {
   var dsel = parseInt(ctrl.innerText,10);
   if(ctrl.innerText == "") 
      return;   
   this.currentDate.setDate(dsel);
   this.UpdateData();
   if(this.currentDay != null)
      this.currentDay.style.backgroundColor ="#FFFFFF";
   this.dateCtrls[dsel].style.backgroundColor = "#FFFF66";   
   this.currentDay = this.dateCtrls[dsel];
   if(flag)
     this.ShowCalender();
 }


 this.RebuildCalender = function()
 {
   var baseDate = new Date(this.currentDate.getYear(),this.currentDate.getMonth(),1);
   var dayOfWeek = baseDate.getDay();
   var dayOfMonth = this.getDayCount();
   var i,cPos=1,v;
   //clean non date cell.
   for(i = 0; i < dayOfWeek; i++)
     this.date_row1.children[i].innerText ='';
   for(i = 0; i < 7; i++)
   {
     this.date_row5.children[i].innerText ='';
	 this.date_row6.children[i].innerText ='';	 
   }
   //fill date cell 1.
   for(i = dayOfWeek; i < 7; i++)
   {
     this.date_row1.children[i].innerText = cPos;
     this.dateCtrls[cPos] = this.date_row1.children[i];
     this.dateCtrls[cPos].style.backgroundColor="#FFFFFF";
     cPos++;   
   }
   v=0;
   for(i = 0; i < 7; i++)
   {
     this.date_row2.children[v].innerText = cPos;
     this.dateCtrls[cPos] = this.date_row2.children[i];
     this.dateCtrls[cPos].style.backgroundColor="#FFFFFF";
     cPos++;
     v++;
   }
   v=0;
   for(i = 0; i < 7; i++)
   {
     this.date_row3.children[v].innerText = cPos;
     this.dateCtrls[cPos] = this.date_row3.children[i];
     this.dateCtrls[cPos].style.backgroundColor="#FFFFFF";
     cPos++;
     v++;
   }
   
   v=0;
   for(i = 0; i < 7; i++)
   {
     if(cPos > dayOfMonth)
        return;
     this.date_row4.children[v].innerText = cPos;
     this.dateCtrls[cPos] = this.date_row4.children[i];
     this.dateCtrls[cPos].style.backgroundColor="#FFFFFF";
     cPos++;
     v++;
   }
   v=0;
   for(i = 0; i < 7; i++)
   {
     if(cPos > dayOfMonth)
       return;
     this.date_row5.children[v].innerText = cPos;
     this.dateCtrls[cPos] = this.date_row5.children[i];
     this.dateCtrls[cPos].style.backgroundColor="#FFFFFF";
     cPos++;
     v++;
   }   
   v=0;
   for(i = 0; i < 7; i++)
   {
     if(cPos > dayOfMonth)
        return;
     this.date_row6.children[v].innerText = cPos;
     this.dateCtrls[cPos] = this.date_row6.children[i];
     this.dateCtrls[cPos].style.backgroundColor="#FFFFFF";
     cPos++;
     v++;
   }   
 }

 this.ShowCalender = function()
 {
  if(this.owner.style.display == "none")
  {
      defaultCoordinate.Push(this);	
      this.owner.style.display = "block";
      document.onclick=HideCalender;
      currentOpenCalender = this.owner;			
      currentIsOpen = 0; 
  }    
  else
  {
      this.owner.style.display = "none";
      document.onclick=null;
      currentIsOpen = 0;
      defaultCoordinate.Remove();
  }			
 }

 this.AddYear = function()
 {
  var currentYear = parseInt(this.yearCtrl.innerText,10);
  this.yearCtrl.innerText = currentYear+1;  
  this.currentDate.setYear(currentYear+1);
  this.RebuildCalender();  
  this.UpdateData();
  currentIsOpen = 0;
  this.SelectedDay(this.dateCtrls[this.currentDate.getDate()],false);
 }

 this.DecYear = function()
 {
  var currentYear = parseInt(this.yearCtrl.innerText,10);
  this.yearCtrl.innerText = currentYear-1;  
  this.currentDate.setYear(currentYear-1);
  this.RebuildCalender();
  this.UpdateData();
  currentIsOpen = 0;
  this.SelectedDay(this.dateCtrls[this.currentDate.getDate()],false);
 }

 this.AddMonth = function()
 {
  var currentMonth = this.currentDate.getMonth();
  if(currentMonth >= 11) return;
  this.monthCtrl.innerText = currentMonth+2;
  
  this.currentDate.setMonth(currentMonth+1);
  
  this.RebuildCalender();
  this.currentDate.setDate(dsel);
  this.UpdateData();
  currentIsOpen = 0;
  this.SelectedDay(this.dateCtrls[this.currentDate.getDate()],false);
 }

 this.DecMonth = function()
 {
  var currentMonth = this.currentDate.getMonth();
  if(currentMonth == 0) return;
  this.monthCtrl.innerText = currentMonth;

  this.currentDate.setMonth(currentMonth-1);  
  
  this.RebuildCalender();
  this.currentDate.setDate(dsel);
  this.UpdateData();
  currentIsOpen = 0;
  this.SelectedDay(this.dateCtrls[this.currentDate.getDate()],false);
 }
 
this.ParseString = function(text)
{
  var year_sp = text.indexOf('-');
  var syear,smonth,sday;
  var iyear,imonth,iday;
  if(year_sp >= 0)
  {
   syear = text.substring(0,year_sp);   
   text = text.substring(year_sp+1,text.length);			
  }
  else
    return null; 
  var month_sp = text.indexOf('-');
  if(month_sp >= 0)
  {
    smonth = text.substring(0,month_sp);
    text = text.substring(month_sp+1,text.length);			
  }
  else
    return null; 
  sday = text;
  iyear = parseInt(syear,10);
  imonth = parseInt(smonth,10);
  iday = parseInt(sday,10);
  if( iyear < 1901)	
     return null;
  if(imonth < 0 || imonth > 12)
     return null;			
  return new Date(iyear,imonth-1,iday);	 
}  

this.UpdateFromEdit = function()
{
  var d = this.ParseString(this.editCtrl.value);
  if(d != null)
  {
  var year = (d.getYear()<1900?(1900+d.getYear()):d.getYear());
   this.currentDate = new Date(year,d.getMonth(),d.getDate());	
   		
   this.yearCtrl.innerText = year;
   this.monthCtrl.innerText = d.getMonth()+1;
   this.RebuildCalender();
   this.UpdateData();
   this.SelectedDay(this.dateCtrls[d.getDate()]);
   return;
  } 
  window.alert("日期格式录入错误！");
  this.UpdateData();  
}			
 
 //this.RebuildCalender(); 
 //this.UpdateData();
 var dsel = this.currentDate.getDate(); 
 this.UpdateFromEdit();
 this.SelectedDay(this.dateCtrls[dsel],false);						
}


function CoordinateObject()
{
	
 this.coordinateObjects = new Array();

 this.Push = function(srcObj)
 {  
  this.Pop();	
  var l = this.coordinateObjects.length;
  this.coordinateObjects[l] = srcObj;  
 }

 this.Pop = function()
 {
  //coordinate-able object must provides a function named CoordinateNow()  
  if(this.coordinateObjects.length > 0)
  {
   this.coordinateObjects[0].CoordinateNow();       	
   var tempArray = this.coordinateObjects.slice(1,this.coordinateObjects.length);
   this.coordinateObjects[0] = null;
   this.coordinateObjects = tempArray;
  }
 }
 
 this.Remove = function()
 {
  if(this.coordinateObjects.length > 0)	
  {
    var tempArray = this.coordinateObjects.slice(1,this.coordinateObjects.length);
    this.coordinateObjects[0] = null;
    this.coordinateObjects = tempArray;
  }   
 }
  
}

var defaultCoordinate = new CoordinateObject();
