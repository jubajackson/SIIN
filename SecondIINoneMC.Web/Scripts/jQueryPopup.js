/***************************/
//@Author: Adrian "yEnS" Mato Gondelle
//@website: www.yensdesign.com
//@email: yensamg@gmail.com
//@license: Feel free to use it, but keep this credits please!					
/***************************/

//SETTING UP OUR POPUP
//0 means disabled; 1 means enabled;
var popupStatus = 0;
var globalBackgroundPopup = '';
var globalPopup = '';
var globalPopupClose = '';

jQuery.fn.center = function () {
    this.css("position","absolute");
    this.css("top", (($(window).height() - this.outerHeight()) / 2) + $(window).scrollTop() + "px");
    this.css("left", (($(window).width() - this.outerWidth()) / 2) + $(window).scrollLeft() + "px");
    return this;
}

//loading popup with jQuery magic!
function loadJQueryPopup(){
	//loads popup only if it is disabled
	if(popupStatus==0){
		$(globalBackgroundPopup).css({
			"opacity": "0.7"
		});
		$(globalBackgroundPopup).fadeIn("slow");
		$(globalPopup).fadeIn("slow");
		popupStatus = 1;
	}
}

//disabling popup with jQuery magic!
function disableJQueryPopup(){
	//disables popup only if it is enabled
	if(popupStatus==1){
		$(globalBackgroundPopup).fadeOut("slow");
		$(globalPopup).fadeOut("slow");
		popupStatus = 0;
	}
}

//centering popup
function centerPopup(){
	//request data for centering
	var windowWidth = document.documentElement.clientWidth;
	var windowHeight = document.documentElement.clientHeight;
	var popupHeight = $(globalPopup).height();
	var popupWidth = $(globalPopup).width();

	//centering
	$(globalPopup).center()
    
	//only need force for IE6
	$(globalBackgroundPopup).css({
		"height": windowHeight
	});
	
}


//CONTROLLING EVENTS IN jQuery
$(document).ready(function(){
	
	//Press Escape event!
	$(document).keypress(function(e){
		if(e.keyCode==27 && popupStatus==1){
			disableJQueryPopup();
		}
	});

});