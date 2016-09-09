﻿/************************************************************************  
                                VARS
**************************************************************************/


/************************************************************************ 
                           Page Element Enums
**************************************************************************/
var RegularExpressions =
{
    Email : /^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$/,
    LeadingBlanks : /^\s+/,
    TrailingBlanks: /\s+$/,
    Date : /(\d{1,2})(\/|-|.)(\d{1,2})(\/|-|.)(\d{2}|\d{4})$/,
    DateYear2Digits : /^\d{1,2}(\-|\/|\.)\d{1,2}\1\d{2}$/,
    DateYear4Digits : /^\d{1,2}(\-|\/|\.)\d{1,2}\1\d{4}$/,
    UserPassword: /(?=^.{8,15}$)(?=.*\d)(?=.*[a-zA-Z])(?!.*\s)[0-9a-zA-Z~`!@#$%^&*()]*$/ /*/(?=^.{8,15}$)(?=.*\d)(?=.*[a-zA-Z])/*/,
    PhoneNumber: /^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$/,
    URL: /^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$/,
    ZipCode: /^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$/,
    PhoneNumber: /^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$/,
    NumbersOnly: /^[0-9]+$/,
    SocialSecurityNumber: /^\d{3}-\d{2}-\d{4}$/
}



/************************************************************************ 
                               Functions 
**************************************************************************/
