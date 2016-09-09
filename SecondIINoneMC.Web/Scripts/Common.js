/*******************************************************************************
**          Change History
*******************************************************************************
**    Date:         Author:             Description:
**    --------      --------            ---------------------------------------
**    When          WHO                 what/why    
02/08/2013    Juba Jackson        Created the Common.js folder with common javascript functions 
*******************************************************************************/


/************************************************************************  
Vars
**************************************************************************/
var _existingPictureWidth = '';
var _existingPictureHeight = '';
var _pictureWidth = '';
var _pictureHeight = '';

/************************************************************************ 
Enums
**************************************************************************/

/************************************************************************ 
Functions
**************************************************************************/
function isBrowser() {
    var isBrowser = true;

    switch (navigator.userAgent) {
        case "Android":
            isBrowser = false;
            break;
        case "webOS":
            isBrowser = false;
            break;
        case "iPhone":
            isBrowser = false;
            break;
        case "iPad":
            isBrowser = false;
            break;
        case "iPod":
            isBrowser = false;
            break;
        case "BlackBerry":
            isBrowser = false;
            break;
    }

    return isBrowser;
}

function getImageSize(imgSrc) {
    var newImg = new Image();
    newImg.src = imgSrc;
    var height = newImg.height;
    var width = newImg.width;
    p = $(newImg).ready(function () {
        return { width: newImg.width, height: newImg.height };
    });
    _existingPictureWidth = p[0]['width'];
    _existingPictureHeight = p[0]['height'];
}

function getResizedImageHeightAndWidth(oldWidth, oldHeight, maxSideSize) {
    if (oldWidth >= oldHeight) {
        largestSide = oldWidth;
    }
    else {
        largestSide = oldHeight;
    }

    if (largestSide > maxSideSize) {
        var dblCoef = maxSideSize / largestSide;

        _pictureWidth = (dblCoef * oldWidth);
        _pictureHeight = (dblCoef * oldHeight);
    }
    else {
        width = oldWidth;
        height = oldWidth;
    }
}

function displayVideo(videoId, videoDivName) {
    var videoString = ''

    videoString = videoString + '<param name="movie" value="http://www.youtube.com/v/' + videoId + '?fs=1&amphl=en_US"></param>'
    videoString = videoString + '<param name="allowFullScreen" value="true"></param>'
    videoString = videoString + '<param name="allowscriptaccess" value="always"></param>'
    videoString = videoString + '<embed src="http://www.youtube.com/v/' + videoId + '?fs=1&amphl=en_US" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" width="480" height="480"></embed>'
    videoString = videoString + '</object><p>&nbsp</p></div>'


    videoString = videoString + '<div><object width="480" height="480">'
    videoString = videoString + '<param name="movie" value="http://www.youtube.com/v/' + videoId + '?fs=1&amphl=en_US"></param>'
    videoString = videoString + '<param name="allowFullScreen" value="true"></param>'
    videoString = videoString + '<param name="allowscriptaccess" value="always"></param>'
    videoString = videoString + '<embed src="http://www.youtube.com/v/' + videoId + '?fs=1&amphl=en_US" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" width="480" height="480"></embed>'
    videoString = videoString + '</object><p>&nbsp</p></div>'

    var videoDiv = document.getElementById(videoDivName);

    $(videoDivName).show();
    $(videoDivName).html(videoString);

    globalBackgroundPopup = '#backgroundVideoPopup'
    globalPopup = '#videoPopup'
    globalPopupClose = '#videoPopupClose'

    //centering with css
    centerPopup()
    //load popup
    loadJQueryPopup()
}

function JSONDate(dateStr) {
    var m, day;
    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    return (m + '/' + day + '/' + d.getFullYear())
}

function JSONDateWithTime(dateStr) {
    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    var m, day;
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    var formattedDate = m + "/" + day + "/" + d.getFullYear();
    var hours = (d.getHours() < 10) ? "0" + d.getHours() : d.getHours();
    var minutes = (d.getMinutes() < 10) ? "0" + d.getMinutes() : d.getMinutes();
    var formattedTime = hours + ":" + minutes + ":" + d.getSeconds();
    formattedDate = formattedDate + " " + formattedTime;
    return formattedDate;
}
function showHTMLContent(elementName, elementHTML) {
    $('#' + elementName).show();
    $('#' + elementName).empty();
    $('#' + elementName).html(elementHTML);
}

function isNullOrUndefinedOrEmpty(object) {
    return (object === undefined || object === null || object === "" || object.length <= 0);
}

function addError(errorMessage) {
    $('#divError ul').append($('<li>').append(errorMessage));
}

function showErrors() {
    hideWaitDialog();
    $('#divError').show();
}

function hideErrors() {
    $('#divError ul').empty();
    $('#divError').hide();
}

function addMessage(message) {
    $('#divMessage ul').append($('<li>').append(message));
}

function showMessages() {
    $('#divMessage').show();
}

function hideMessages() {
    $('#divMessage ul').empty();
    $('#divMessage').hide();
}

function convertFacebookDate(facebookDate) {
    var time_formats = [
		[60, 'Just Now'],
		[90, '1 Minute'], // 60*1.5
		[3600, 'Minutes', 60], // 60*60, 60
		[5400, '1 Hour'], // 60*60*1.5
		[86400, 'Hours', 3600], // 60*60*24, 60*60
		[129600, '1 Day'], // 60*60*24*1.5
		[604800, 'Days', 86400], // 60*60*24*7, 60*60*24
		[907200, '1 Week'], // 60*60*24*7*1.5
		[2628000, 'Weeks', 604800], // 60*60*24*(365/12), 60*60*24*7
		[3942000, '1 Month'], // 60*60*24*(365/12)*1.5
		[31536000, 'Months', 2628000], // 60*60*24*365, 60*60*24*(365/12)
		[47304000, '1 Year'], // 60*60*24*365*1.5
		[3153600000, 'Years', 31536000], // 60*60*24*365*100, 60*60*24*365
		[4730400000, '1 Century'], // 60*60*24*365*100*1.5
	];

    var time = ('' + facebookDate).replace(/-/g, "/").replace(/[TZ]/g, " ").replace("+0000", " "),
		dt = new Date,
		seconds = ((dt - new Date(time) + (dt.getTimezoneOffset() * 60000)) / 1000),
		token = ' Ago',
		i = 0,
		format;

    if (seconds != NaN || seconds < 0) {
        seconds = Math.abs(seconds);
        token = '';
    }

    while (format = time_formats[i++]) {
        if (seconds < format[0]) {
            if (format.length == 2) {
                return format[1] + (i > 1 ? token : ''); // Conditional so we don't return Just Now Ago
            } else {
                return Math.round(seconds / format[2]) + ' ' + format[1] + (i > 1 ? token : '');
            }
        }
    }

    // overflow for centuries
    if (seconds > 4730400000)
        return Math.round(seconds / 4730400000) + ' Centuries' + token;

    return facebookDate;
};

function getBaseURL() {
    var url = location.href;  // entire url including querystring - also: window.location.href;
    var baseURL = url.substring(0, url.indexOf('/', 14));


    if (baseURL.indexOf('http://localhost') != -1) {
        // Base Url for localhost
        var url = location.href.toString().replace(location.pathname, '');  // window.location.href;
        var url = url.toString().replace('#', '');  // window.location.href;

        return url + "/";
    }
    else {
        // Root Url for domain name
        return baseURL + "/";
    }

}

function capitalizeWords(stringToCapitalize) {
    var tmpStr;
    var tmpChar;
    var preString;
    var postString;
    var strlen;

    tmpStr = stringToCapitalize.toLowerCase();
    stringLen = tmpStr.length;

    if (stringLen > 0) {
        for (i = 0; i < stringLen; i++) {
            if (i == 0) {
                tmpChar = tmpStr.substring(0, 1).toUpperCase();
                postString = tmpStr.substring(1, stringLen);
                tmpStr = tmpChar + postString;
            }
            else {
                tmpChar = tmpStr.substring(i, i + 1);
                if ((tmpChar == " " || tmpChar == "-") && i < (stringLen - 1)) {
                    tmpChar = tmpStr.substring(i + 1, i + 2).toUpperCase();
                    preString = tmpStr.substring(0, i + 1);
                    postString = tmpStr.substring(i + 2, stringLen);
                    tmpStr = preString + tmpChar + postString;
                }
            }
        }
    }

    return tmpStr;
}

function isZipCodeValid(zipCode) {
    var isZipValid = true;

    if (!RegularExpressions.ZipCode.test(zipCode)) {
        isZipValid = false;
    }

    return isZipValid;
}

function isEmailValid(email) {
    var isEmailAddressValid = true;

    if (email.length > 0) {
        if (!RegularExpressions.Email.test(email)) {
            isEmailAddressValid = false;
        }
    }

    return isEmailAddressValid;
}

function isPhoneNumberValid(phoneNumber) {
    var isPhoneValid = true;

    if (phoneNumber.length > 0) {
        if (!RegularExpressions.PhoneNumber.test(phoneNumber)) {
            isPhoneValid = false;
        }
    }

    return isPhoneValid;
}

function isWebAddressValid(webAddress) {
    var isWebSiteAddressValid = true;

    if (webAddress.length > 0) {
        if (!RegularExpressions.URL.test(webAddress)) {
            isWebSiteAddressValid = false;
        }
    }

    return isWebSiteAddressValid;
}

/*	*******************************************************
Layout Popup menu interaction functions 
*******************************************************		*/
function displayHelp(helpType) {
    $('#divMessage ul').empty();

    switch (helpType) {
        case 'password':
            addMessage('<span style="font-weight:bold;">To Reset Your Password;</span>');
            addMessage('1. - In the Change Password box at the top left of the page.');
            addMessage('2. - Enter your current password.');
            addMessage('3. - Enter your new password.');
            addMessage('4. - Confirm your new password by entering it twice.');
            addMessage('5. - Push the "Change Password" button.');
            addMessage('6. - Your new password is saved.');
            break;
        case 'addresses':
            addMessage('<span style="font-weight:bold;">To Add a Address;</span>');
            addMessage('1. - In the Manage Addresses box at the bottom right of the page.');
            addMessage('2. - Click the “Add” button at the bottom of the form.');
            addMessage('3. - The Address form will be set up to add a new address.');
            addMessage('4. - Fill out the New Address Form.');
            addMessage('5. - Click the “Save Address Button”.<br /><br />');

            addMessage('<span style="font-weight:bold;">To Edit a Address;</span>');
            addMessage('1. - In the Manage Addresses box at the bottom right of the page.');
            addMessage('2. - Select the Address you would like to edit from the dropdown list.');
            addMessage('3. - The Address form will pre populated with the address information you selected.');
            addMessage('4. - Change or add information on the form.');
            addMessage('5. - Click the “Save Address Button”.');
            break;
        case 'users':
            addMessage('<span style="font-weight:bold;">To Add a User;</span>');
            addMessage('1. - Click the Add User button at the bottom of the form.');
            addMessage('2. - The Add a New User form appears.');
            addMessage('3. - Fill out the Add User form data.');
            addMessage('4. - Select the roles a user will be given.<br /><span style="color: red; font-size:x-small; margin-left: 20px;">(Click the ctrl key to select multiple items.)</span>');
            addMessage('5. - Every user must be in the User role.');
            addMessage('6. - Click the “Save User” button when done filling out the form.<br /><br />');

            addMessage('<span style="font-weight:bold;">To Edit a User;</span>');
            addMessage('1. - Enter the name, username, or email address of the user you would like to edit in the user search screen.');
            addMessage('2. - The pre-populated edit user form appears.');
            addMessage('3. - Change or add any information in the Edit User form.');
            addMessage('4. - Select the roles a user will be given.<br /><span style="color: red; font-size:x-small; margin-left: 20px;">(Click the ctrl key to select multiple items.)</span>');
            addMessage('5. - Every user must be in the User role.');
            addMessage('6. - Click the “Save User” button when done filling out the form.');
            break;
        case 'register':
            addMessage('<span style="color:red;">NOTE: "SECOND II NONE MC MEMBERS ONLY!" If you are not a member of Second II None MC do not register on this website.</span>');
            addMessage('<span style="font-weight:bold;">To Register a User;</span>');
            addMessage('1. - Select your charter from the dropdown list.');
            addMessage('2. - Select your position from the dropdown list.');
            addMessage('3. - Enter your username. (Must be an email address)');
            addMessage('4. - Enter your riding name.');
            addMessage('5. - Enter your password.');
            addMessage('6. - Confirm your password.');
            addMessage('7. - Enter your birthday.');
            addMessage('8. - Click the “Register” button to complete the registration process.');
            break;
        case 'forgotPassword':
            addMessage('1. - Click the “Forgot Password” link on the Log In Page right above the password textbox.');
            addMessage('2. - You will be redirected to the forgot password page.');
            addMessage('3. - Enter the email address you used to register on the website.');
            addMessage('4. - Click the “Reset Password” button.');
            addMessage('5. - Your password will be reset and you new password will be sent to your email address.');
            addMessage('6. - Change your password to something you can remember after you log back in using the “My Account” section of the website.');
            break;
        default:
            addMessage('There is no help documentation for this section of the website.');
            break;
    }

    showMessages();
}

function showWaitDialog(message) {
    $("#loading-div-background").css({ opacity: 0.8 });
    $("#lblWaitMessage").text(message);
    $("#loading-div-background").show();
}

function hideWaitDialog() {
    $("#lblWaitMessage").val('');
    $("#loading-div-background").hide();
}

function getLocalTime(utcTime) {
    var localTime = new Date();

    return new Date(utcTime - (localTime.getTimezoneOffset() * 60000));
}

function getFormattedUtcDateTime(localTime) {
    var result = "";
    var utcDate = new Date();
    var newTime = localTime.getTime();

    newTime += (localTime.getTimezoneOffset() * 60000);

    utcDate.setTime(newTime);

    result = utcDate.format('MM/dd/yyyy HH:mm:ss');

    return result;
}

function getFormattedDateTime(dateTime, localize) {
    var minDate = new Date();
    var result;

    minDate.setFullYear(1, 0, 1);
    minDate.setHours(0, 0, 0, 0);

    if (dateTime <= minDate) {
        result = '';
    }
    else {
        if (localize) {
            result = getLocalTime(dateTime).format('MM/dd/yyyy HH:mm:ss');
        }
        else {
            result = dateTime.format('MM/dd/yyyy HH:mm:ss');
        }
    }

    return result;
}

function getFormattedDateTimeAMPM(dateTime) {
    return dateTime.format('MM/dd/yyyy hh:mm tt');
}

function getFormattedTimeAMPM(dateTime) {
    if (dateTime.format("hh:mm tt").toString().indexOf("0") == 0)
        return dateTime.format("h:mm tt");
    else
        return dateTime.format("hh:mm tt");
}

function getFormattedTime(dateTime) {
    return dateTime.format('HH:mm');
}

function getFormattedDate(dateTime) {
    return dateTime.format('MM/dd/yyyy');
}


function getDateDifferenceInDays(fromDate, toDate) {
    var one_day = 1000 * 60 * 60 * 24
    return Math.ceil((fromDate.getTime() - toDate.getTime()) / (one_day));
}

function getWeekNumberWithinMonth(date) {

    var eventDate = new Date(date);

    var prefixes = ['First', 'Second', 'Third', 'Fourth', 'Fifth'];

    var weekNum = 0 | eventDate.getDate() / 7;
    weekNum = (eventDate.getDate() % 7 === 0) ? weekNum - 1 : weekNum;

    return prefixes[weekNum];

}

function formatCurrency(nStr, prefix) {
    return String.localeFormat("{0:c}", parseFloat(nStr));
}

function formatCurrencyWith3Decimal(nStr, prefix) {
    var value = to3Decimal(nStr)

    return String.localeFormat("{0:c3}", value);
}

function to3Decimal(val) {
    val = parseFloat(val);

    val = Math.ceil(val * 1000) / 1000;

    return val;
}

/************************************************************************ 
FUNCTION FOR INITIALIZING A DROP DOWN BOX ELEMENT
**************************************************************************/
function initializeDropDown(element, text, value, isDefaultSelected, isSelected) {
    element.options.length = 0;

    addDropDownOption(element, text, value, isDefaultSelected, isSelected);
}

function addDropDownOption(element, text, value, isDefaultSelected, isSelected) {
    var optSelect = new Option(text, value, isDefaultSelected, isSelected);
    element.options[element.options.length] = optSelect;
}
