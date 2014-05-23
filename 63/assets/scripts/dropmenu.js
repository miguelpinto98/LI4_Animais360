var tabdropdown={
disappeardelay: 275, //set delay in miliseconds before menu disappears onmouseout
disablemenuclick: false, //when user clicks on a menu item with a drop down menu, disable menu item's link?
//downsymbol: '<img src="../imagens/dropindicate.gif" style="display:none;border: 0; padding-left: 5px" />', //HTML "symbol" to use to indicate this is a dropdown menu item. Enter ('') to disable.

//No need to edit beyond here////////////////////////
dropmenuobj: null, ie: document.all, firefox: document.getElementById&&!document.all, previousmenuitem:null,

getposOffset:function(what, offsettype){
var totaloffset=(offsettype=="left")? what.offsetLeft : what.offsetTop;
var parentEl=what.offsetParent;
while (parentEl!=null){
totaloffset=(offsettype=="left")? totaloffset+parentEl.offsetLeft : totaloffset+parentEl.offsetTop;
parentEl=parentEl.offsetParent;
}
return totaloffset;
},

showhide:function(obj, e, obj2){ //obj refers to drop down menu, obj2 refers to tab menu item mouse is currently over
if (this.ie || this.firefox)
this.dropmenuobj.style.left=this.dropmenuobj.style.top="-500px"
if (e.type=="click" && obj.visibility==hidden || e.type=="mouseover"){
if (obj2.parentNode.className.indexOf("default")==-1) //if tab isn't a default selected one
obj2.parentNode.className="selected"
obj.visibility="visible"
}
else if (e.type=="click")
obj.visibility="hidden"
},

iecompattest:function(){
return (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body
},

clearbrowseredge:function(obj, whichedge){
var edgeoffset=0
if (whichedge=="rightedge"){
var windowedge=this.ie && !window.opera? this.iecompattest().scrollLeft+this.iecompattest().clientWidth-15 : window.pageXOffset+window.innerWidth-15
this.dropmenuobj.contentmeasure=this.dropmenuobj.offsetWidth
if (windowedge-this.dropmenuobj.x < this.dropmenuobj.contentmeasure)  //move menu to the left?
edgeoffset=this.dropmenuobj.contentmeasure-obj.offsetWidth
}
else{
var topedge=this.ie && !window.opera? this.iecompattest().scrollTop : window.pageYOffset
var windowedge=this.ie && !window.opera? this.iecompattest().scrollTop+this.iecompattest().clientHeight-15 : window.pageYOffset+window.innerHeight-18
this.dropmenuobj.contentmeasure=this.dropmenuobj.offsetHeight
if (windowedge-this.dropmenuobj.y < this.dropmenuobj.contentmeasure){ //move up?
edgeoffset=this.dropmenuobj.contentmeasure+obj.offsetHeight
if ((this.dropmenuobj.y-topedge)<this.dropmenuobj.contentmeasure) //up no good either?
edgeoffset=this.dropmenuobj.y+obj.offsetHeight-topedge
}
this.dropmenuobj.style.borderTopWidth=(edgeoffset==0)? 0 : "1px" //Add 1px top border to menu if dropping up
}
return edgeoffset
},

dropit:function(obj, e, dropmenuID){
if (this.dropmenuobj!=null){ //hide previous menu
this.dropmenuobj.style.visibility="hidden" //hide menu
if (this.previousmenuitem!=null && this.previousmenuitem!=obj){
if (this.previousmenuitem.parentNode.className.indexOf("default")==-1) //If the tab isn't a default selected one
this.previousmenuitem.parentNode.className=""
tabdropdown.togglehiddenobj(this.previousmenuitem, 'visible')
}
}
this.clearhidemenu()
if (this.ie||this.firefox){
obj.onmouseout=function(){tabdropdown.delayhidemenu(obj)}
obj.onclick=function(){return !tabdropdown.disablemenuclick} //disable main menu item link onclick?
this.dropmenuobj=document.getElementById(dropmenuID)
this.dropmenuobj.onmouseover=function(){tabdropdown.clearhidemenu()}
this.dropmenuobj.onmouseout=function(e){tabdropdown.dynamichide(e, obj)}
this.dropmenuobj.onclick=function(){tabdropdown.delayhidemenu(obj)}
this.showhide(this.dropmenuobj.style, e, obj)
this.dropmenuobj.x=this.getposOffset(obj, "left")
this.dropmenuobj.y=this.getposOffset(obj, "top")
this.dropmenuobj.style.left=this.dropmenuobj.x-this.clearbrowseredge(obj, "rightedge")+"px"
this.dropmenuobj.style.top=this.dropmenuobj.y-this.clearbrowseredge(obj, "bottomedge")+obj.offsetHeight+3+"px"
this.previousmenuitem=obj //remember main menu item mouse moved out from (and into current menu item)
tabdropdown.togglehiddenobj(obj, "hidden") //Hide (form) object drop down menu overlaps, if any
}
},

togglehiddenobj:function(obj2, visible){
var revvalue=obj2.getAttribute("rev")
if (typeof revvalue=="string" && revvalue.length>0 && document.getElementById(revvalue)!=null)
document.getElementById(revvalue).style.visibility=visible
},

contains_firefox:function(a, b) {
while (b.parentNode)
if ((b = b.parentNode) == a)
return true;
return false;
},

dynamichide:function(e, obj2){ //obj2 refers to tab menu item mouse is currently over
var evtobj=window.event? window.event : e
if (this.ie&&!this.dropmenuobj.contains(evtobj.toElement))
this.delayhidemenu(obj2)
else if (this.firefox&&e.currentTarget!= evtobj.relatedTarget&& !this.contains_firefox(evtobj.currentTarget, evtobj.relatedTarget))
this.delayhidemenu(obj2)
},

delayhidemenu:function(obj2){
this.delayhide=setTimeout(function(){tabdropdown.dropmenuobj.style.visibility='hidden'; if (obj2.parentNode.className.indexOf('default')==-1) obj2.parentNode.className=''; tabdropdown.togglehiddenobj(obj2, 'visible')},this.disappeardelay) //hide menu
},

clearhidemenu:function(){
if (this.delayhide!="undefined")
clearTimeout(this.delayhide)
},

initializetabmenu:function(menuid, opt_selectedmenuitem){
var menuitems=document.getElementById(menuid).getElementsByTagName("a")
for (var i=0; i<menuitems.length; i++){
if (parseInt(opt_selectedmenuitem)==i)
menuitems[i].parentNode.className+=" selected default"
if (menuitems[i].getAttribute("rel")){
var relvalue=menuitems[i].getAttribute("rel")
menuitems[i].onmouseover=function(e){
var event=typeof e!="undefined"? e : window.event
tabdropdown.dropit(this, event, this.getAttribute("rel"))
}
/*
if (tabdropdown.downsymbol!="")
menuitems[i].innerHTML+=tabdropdown.downsymbol //Add symbol HTML to menu item's.
*/
}
}
}

}