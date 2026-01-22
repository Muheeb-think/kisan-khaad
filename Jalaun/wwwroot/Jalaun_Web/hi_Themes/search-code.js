$(document).ready(function () {
$( ".srch-box" ).wrap( "<form action='article/hi/search-result'></form>" );
$(".hi-btn").click( function () {
    var cfrm = confirm("You are being redirected to English Version of Digital Fertilizer Demand and Distribution Management System District Jalaun Website.");
if(cfrm==true)
{
window.location(this.window.url+"/hi");
return true;
}
else if(cfrm==false)
{
return false;
}
//alert(crm);
});
});

$(document).ready(function() {
var comp = new RegExp(location.host);
$('a').each(function(){
if(comp.test($(this).attr('href'))){
// a link that contains the current host 
$(this).addClass('local');
}
else{
// a link that does not contain the current host
$(this).addClass('external');
}
});

$('a').filter(function() {
return this.hostname && this.hostname !== location.hostname;
})
.click(function () {
$(this).attr('target', '_blank');
var x=window.confirm('आपको उत्तर प्रदेश जल निगम विभाग की वेबसाइट से हस्तानांतरित किया जा रहा है और अब आप किसी बाहरी वेबसाइट का कंटेंट देखेंगे');
var val = false;
if (x)
val = true;
else
val = false;
return val;
});
});

$(function () {
var x;
setInterval(function () {
if (x == 0) {
$('.livehi').css('backgroundColor', 'rgb(175, 255, 0)');
x = 1;
} else {
if (x = 1) {
$('.livehi').css('backgroundColor', 'rgb(243, 255, 0)');
x = 0;
}
}
}, 500);
});

/////////// END HERE	


/* IMAGE CAPTION */

$(".grid-stack img").each(function() {
var imageCaption = $(this).attr("title");
if (imageCaption != '') {
var imgWidth = $(this).width();
var imgHeight = $(this).height();
var position = $(this).position();
var positionTop = (position.top + imgHeight - 26);
$("<span class='title-caption'><em>"+imageCaption+"</em></span>")
.css({"position":"absolute", "top":positionTop+"px", "left":"0", "width":imgWidth +"px"})
.insertAfter(this);
}
var num = imageCaption;
var mnum = num.indexOf("/");
var title = num.substr(mnum+1, num.length)
$(this).attr('title',title);
$(this).attr('alt',title);
});


$(".title-caption em").each(function() {
var num = $(this).html();
var mnum = num.indexOf("/");
var title = num.substr(mnum+1, num.length)
$(this).html(title)
});	

$(".grid-stack-item-content a").each(function(index) {
var numm = $(this).attr('data-caption')
var mnumm = numm.indexOf("/");
var titlef = numm.substr(mnumm+1, numm.length)
$(this).attr('data-caption',titlef);
$(this).on("click", function(){
//	
//alert($(this).attr('data-caption'));
// For the boolean value
//$(".fancybox-caption__body").hide()
//var num = $(this).html();
//var mnum = num.indexOf("/");
//var title = num.substr(mnum+1, num.length)
//$(this).html(title) 
});
});